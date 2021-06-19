﻿using System.Collections.Generic;

namespace TaskManager.Utils
{
    public class CustomProcess
    {
        private List<int> cpuPercentageUsage;
        
        private List<float> ramUsage;

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
    }
}