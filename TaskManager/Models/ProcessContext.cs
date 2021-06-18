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

        private CustomProcess _customProcess;
        
        // Public
        public Process CurrentProcess { get; }
        
        // Properties for UI
        public string CpuUsageS
        {
            get => _cpuusageS;

            set => Set<string>(ref _cpuusageS, value);
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
            while (true)
            {
                
            }
        }
        
    }
}