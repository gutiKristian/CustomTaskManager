using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace TaskManager.Utils
{
    public class CustomProcess
    {
        public List<float> cpuPercentageUsage { get; private set; }

        public List<float> ramUsage { get; private set; }

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

        public void GenerateJSON(string path)
        {
            var data = new
            {
                CpuAverage = cpuPercentageUsage.Average(),
                RamAverage = ramUsage.Average(),
                CpuMax = cpuPercentageUsage.Max(),
                RamMax = ramUsage.Max(),
                OverallDuration = Duration
            };
            var json = JsonSerializer.Serialize(data);
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.Write(json);
            }
        }

        public void GenerateReport()
        {
            string path = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "report_" +
                          DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            path = "C:\\Users\\krist\\Desktop\\report";
            GenerateCsv(path + ".csv");
            GenerateJSON(path + ".json");
        }
    }
}