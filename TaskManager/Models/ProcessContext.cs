using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Utils;

namespace TaskManager.Models
{
    public class ProcessContext : NotifyPropertyChanged
    {
        // Private
        private string _cpuusageS;
        private string _ramusageS;

        private CustomProcess _customProcess;
        
        
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
        
        // Methods
        
        public ProcessContext(Process data)
        {
            CurrentProcess = data;
            _customProcess = new CustomProcess();
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
                double a = cpuCounter.NextValue(); // must be done twice
                Thread.Sleep(1000);
                double ramUsag = ramCounter.NextValue();
                double cpuPerc = cpuCounter.NextValue();
                // Enviroment process count
                CpuUsageS = cpuPerc + "%";
                
                RamUsageS = $"{ramUsag / 1048576} MB";
                // cs
                lock (_customProcess)
                {
                    _customProcess.AddRamValue(ramUsag);
                    _customProcess.AddCpuValue((int) cpuPerc);    
                }
                
            }
            
            // auto save ?
        }
        
    }
}