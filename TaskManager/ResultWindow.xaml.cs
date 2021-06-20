using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using LiveCharts;
using LiveCharts.Wpf;
using TaskManager.Utils;
using System.Collections.Generic;

namespace TaskManager
{
    public partial class ResultWindow : Window
    {
        public CustomProcess _customProcess { get; }
        
        public string CpuPercentage { get; }
        public string RamAverage { get; }
        public string CpuMax { get; }
        public string RamMax { get; }
        public string Duration { get; }
        
        public SeriesCollection CpuCollection { get; set; }
        public SeriesCollection RamCollection { get; set; }
        
        
        public ResultWindow(CustomProcess customProcess)
        {
            InitializeComponent();
            _customProcess = customProcess;
            CpuPercentage = Math.Round(_customProcess.cpuPercentageUsage.Average(), 2).ToString();
            RamAverage = Math.Round(_customProcess.ramUsage.Average(), 2).ToString();
            CpuMax = Math.Round(_customProcess.cpuPercentageUsage.Max(), 2).ToString();
            RamMax = Math.Round(_customProcess.ramUsage.Max(), 2).ToString();
            Duration = _customProcess.Duration.Seconds.ToString();

            CPUAVERAGE.Text = $"CPU AVERAGE: {CpuPercentage}%";
            CPUMAX.Text = $"CPU MAX: {CpuMax}%";
            RAMAVERAGE.Text = $"RAM AVERAGE: {RamAverage}MB";
            RAMMAX.Text = $"RAM MAX: {RamMax}MB";
            DURATION.Text = $"DURATION: {Duration} seconds";

            CpuCollection = new SeriesCollection
            {
                new LineSeries()
                {
                    Values = new ChartValues<float>(),
                    Title = "CPU",
                    LineSmoothness = 1,
                    LabelPoint = (ChartPoint p) => $"{p.Y}%",
                }
            };
            
            RamCollection = new SeriesCollection
            {
                new LineSeries()
                {
                    Values = new ChartValues<float>(),
                    Title = "RAM",
                    LineSmoothness = 1,
                    LabelPoint = (ChartPoint p) => $"{p.Y}MB",
                }
            };

            foreach (var entry in _customProcess.cpuPercentageUsage)
            {
                CpuCollection[0].Values.Add(entry);
            }
            
            foreach (var entry in _customProcess.ramUsage)
            {
                RamCollection[0].Values.Add(entry);
            }

            var ylabel = new List<string>();
            for (int i = 0; i < 101; i++)
            {
                ylabel.Add(i.ToString());
            }

            cpuY.Labels = ylabel;
            
            chartCpu.Series = CpuCollection;
            chartRam.Series = RamCollection;

        }
    }
}