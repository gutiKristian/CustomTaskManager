using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Utils;
using LiveCharts;
using System.Windows.Data;
using LiveCharts.Wpf;
using System;

namespace TaskManager.Models
{
    public class ProcessContext : NotifyPropertyChanged
    {
        // Private
        private string _cpuusageS;
        private string _ramusageS;

        private CustomProcess _customProcess;

        private readonly object _itemsLock = new object();

        // Public
        public Process CurrentProcess { get; }
        // Record - whether we are recording meassured values, SaveRecord - when both Record and SaveRecord are
        // true, we will generate output files, both variables serve as flag whether to start recording or save the data
        // in different thread
        public bool Record { get; set; }
        public bool SaveRecord { get; set; }
        
        public DateTime RecordStart { get; set; }
        
        // Properties for UI
        public string CpuUsageS
        {
            get => _cpuusageS;

            set => Set<string>(ref _cpuusageS, value);
        }
        
        public string RamUsageS
        {
            get => _ramusageS;

            set => Set<string>(ref _ramusageS, value);
        }

        public SeriesCollection CpuCollection { get; set; }
        public SeriesCollection RamCollection { get; set; }

        // Methods

        public ProcessContext(Process data)
        {
            CurrentProcess = data;
            _customProcess = new CustomProcess();
            Record = false;

            CpuCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<int> {0, 0, 0, 0, 0, 0, 0, 0, 0},
                    Title = "CPU",
                    LineSmoothness = 1,
                    LabelPoint = (ChartPoint p) =>  $"{p.Y}%",
                }
            };

            RamCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<float> {0, 0, 0, 0, 0, 0, 0, 0, 0},
                    Title = "RAM",
                    LineSmoothness = 1,
                    LabelPoint = (ChartPoint p) =>  $"{p.Y}MB",
                }
            };
            // https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.bindingoperations.enablecollectionsynchronization?view=net-5.0
            BindingOperations.EnableCollectionSynchronization(CpuCollection, _itemsLock);
            // In order to stay responsive run updater from another thread
            Task.Run(Update);
        }

        private void Update()
        {
            PerformanceCounter cpuCounter;
            PerformanceCounter ramCounter;
            
            lock (CurrentProcess)
            {
                cpuCounter = new PerformanceCounter("Process", "% Processor Time", CurrentProcess.ProcessName, true);
                ramCounter = new PerformanceCounter("Process", "Working Set", CurrentProcess.ProcessName, true);
            }
            
            while (true)
            {
                // not thread safe
                cpuCounter.NextValue(); // must be done twice
                Thread.Sleep(1000);
                // Get values
                float ramUsag = ramCounter.NextValue() / 1048576;
                float cpuPerc = cpuCounter.NextValue();
                // update graph
                int cpuP = (int)cpuPerc;

                lock(CpuCollection)
                {
                    CpuCollection[0].Values.Add(cpuP);
                    CpuCollection[0].Values.RemoveAt(0);
                }

                lock (RamCollection)
                {
                   RamCollection[0].Values.Add(ramUsag);
                   RamCollection[0].Values.RemoveAt(0);
                }

                if (Record)
                {
                    lock (_customProcess)
                    {
                        _customProcess.AddCpuValue(cpuP);
                        _customProcess.AddRamValue(ramUsag);

                        if (SaveRecord)
                        {
                            // generate file
                            Record = false;
                            SaveRecord = false;
                            _customProcess.GenerateReport();
                            _customProcess.Duration = DateTime.Now - RecordStart;
                        }
                    }
                    
                }
                
                // Enviroment process count, multiproc pcs, we show 200% instead of 2 processors are being 100% used
                CpuUsageS = $"{Math.Round(cpuPerc, 2)}%";
                RamUsageS = $"{Math.Round(ramUsag, 2)} MB";
                
            }
           
        }
        
    }
}