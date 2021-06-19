using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Utils;
using LiveCharts;
using System.Windows.Data;
using LiveCharts.Wpf;

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

        // Methods

        public ProcessContext(Process data)
        {
            CurrentProcess = data;
            _customProcess = new CustomProcess();

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
            
            BindingOperations.EnableCollectionSynchronization(CpuCollection, _itemsLock);
            // In order to stay responsive run updater from another thread
            Task.Run(Update);
        }

        private void Update()
        {
            PerformanceCounter cpuCounter =
                new PerformanceCounter("Process", "% Processor Time", CurrentProcess.ProcessName, true);
            PerformanceCounter ramCounter =
                new PerformanceCounter("Process", "Working Set", CurrentProcess.ProcessName, true);
            while (true)
            {
                // not thread safe
                cpuCounter.NextValue(); // must be done twice
                Thread.Sleep(1000);
                // Get values
                float ramUsag = ramCounter.NextValue();
                float cpuPerc = cpuCounter.NextValue();
                // update graph
                int cpuP = (int)cpuPerc;
                CpuCollection[0].Values.Add(cpuP);
                CpuCollection[0].Values.RemoveAt(0);

                // Enviroment process count
                CpuUsageS = cpuPerc + "%";
                
                RamUsageS = $"{ramUsag / 1048576} MB";
                
            }
            
            // auto save ?
        }
        
    }
}