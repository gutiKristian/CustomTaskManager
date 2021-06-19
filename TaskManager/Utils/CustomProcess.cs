using System;
using System.Collections.Generic;

namespace TaskManager.Utils
{
    public class CustomProcess
    {
        private List<int> cpuPercentageUsage;
        
        private List<float> ramUsage;

        public TimeSpan Duration { get; set; }

        public CustomProcess()
        {
            this.ResetRecording();
        }

        public void AddCpuValue(int val)
        {
            cpuPercentageUsage.Add(val);
        }

        public void AddRamValue(float val)
        {
            ramUsage.Add(val);
        }

        public void ResetRecording()
        {
            cpuPercentageUsage = new List<int>();
            ramUsage = new List<float>();
        }

        public void GenerateReport()
        {
            // average, max, duration
        }
    }
}