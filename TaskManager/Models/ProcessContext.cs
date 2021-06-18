using System.Diagnostics;

namespace TaskManager.Models
{
    public class ProcessContext
    {
        public Process CurrentProcess { get; }
        
        public ProcessContext(Process data)
        {
            CurrentProcess = data;
        }
    }
}