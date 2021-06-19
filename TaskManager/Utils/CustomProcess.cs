using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TaskManager.Utils
{
    public class CustomProcess
    {
        private List<float> cpuPercentageUsage;
        
        private List<float> ramUsage;

        public TimeSpan Duration { get; set; }

        public CustomProcess()
        {
            this.ResetRecording();
        }

        public void AddCpuValue(float val)
        {
            cpuPercentageUsage.Add(val);
        }

        public void AddRamValue(float val)
        {
            ramUsage.Add(val);
        }

        public void ResetRecording()
        {
            cpuPercentageUsage = new List<float>();
            ramUsage = new List<float>();
        }

        public void GenerateCsv(string path)
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.WriteLine("CpuPercentageUsage, RamMBUsage");
                for (int i = 0; i < cpuPercentageUsage.Count; ++i)
                {
                    streamWriter.WriteLine($"{cpuPercentageUsage[i].ToString().Replace(',', '.')}, {ramUsage[i].ToString().Replace(',', '.')}");
                }
            }
        }

        public void GenerateReport()
        {
            GenerateCsv("C:\\Users\\krist\\Desktop\\record.csv");
            // average, max, duration
            float cpuAverage = cpuPercentageUsage.Average();
            float ramAverage = ramUsage.Average();
            float cpuMax = cpuPercentageUsage.Max();
            float ramMax = ramUsage.Max();
        }
    }
}