using System.Collections.Generic;

namespace TaskManager.Utils
{
    public class CustomProcess
    {
        private List<int> cpuPercentageUsage;

        private List<int> ramPercentageUsage;

        private List<double> ramUsage;

        public CustomProcess()
        {
            this.ResetRecording();
        }

        public void AddCpuValue(int val)
        {
            cpuPercentageUsage.Add(val);
        }

        public void AddRamPerctenageValue(int val)
        {
            ramPercentageUsage.Add(val);
        }

        public void AddRamValue(double val)
        {
            ramUsage.Add(val);
        }

        public void ResetRecording()
        {
            cpuPercentageUsage = new List<int>();
            ramPercentageUsage = new List<int>();
            ramUsage = new List<double>();
        }
    }
}