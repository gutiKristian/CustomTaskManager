using System;
using System.Management;
using Microsoft.Win32;

namespace TaskManager.Utils
{
    public class WindowsPC
    {
        /*
         Win32_ComputerSystem
         Win32_DiskDrive
         Win32_OperatingSystem
         Win32_Processor
         Win32_ProgramGroup
         Win32_SystemDevices
         Win32_StartupCommand
        */
        
        public string PcName { get; }
        public string CpuName { get; }
        public string RamCapacity { get; }

        private Double fRamCapacity;

        public WindowsPC()
        {
            CpuName = ExtractWin32Information("Win32_Processor", "Name");
            PcName = ExtractWin32Information("Win32_Processor", "SystemName");
            // Memory visible to OS
            RamCapacity = ExtractWin32Information("Win32_OperatingSystem", "TotalVisibleMemorySize");
            fRamCapacity = Math.Round(Double.Parse(RamCapacity) / 1024, 2);
            RamCapacity = fRamCapacity.ToString() + "MB";
        }

        public int CpuStatus()
        {
            try
            {
                return Int32.Parse(ExtractWin32Information("Win32_Processor", "LoadPercentage"));
            }
            catch (OverflowException e)
            {
                return 0;
            }
        }

        public double RamStatus()
        {
            try
            {
                return 100 - Math.Round((Double.Parse(ExtractWin32Information("Win32_OperatingSystem", "FreePhysicalMemory")) / (fRamCapacity * 1024)), 2) * 100;
            }
            catch (OverflowException e)
            {
                return 0;
            }
        }

        private string ExtractWin32Information(string querySpec, string information)
        {
            var searcher = new ManagementObjectSearcher($@"select * from {querySpec}").Get();
            foreach (var obj in searcher)
            {
                PropertyDataCollection props = obj.Properties;
                return props[information].Value.ToString();
            }
            return "";
        }
    }
}