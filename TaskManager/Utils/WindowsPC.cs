using System.Management;
using Microsoft.Win32;

namespace TaskManager.Utils
{
    public class WindowsPC
    {
        public string PcName { get; set; }
        public string CpuName { get; set; }
        public string RamCapacity { get; set; }

        public WindowsPC()
        {
        }
    }
}