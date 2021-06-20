using System;
using System.Linq;
using System.Windows;
using TaskManager.Utils;

namespace TaskManager
{
    public partial class ResultWindow : Window
    {
        private CustomProcess _customProcess;
        
        public string CpuPercentage { get; }
        public string RamAverage { get; }
        public string CpuMax { get; }
        public string RamMax { get; }
        public string Duration { get; }
        
        
        
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
            CPUMAX.Text = $"CPU MAX: {CpuPercentage}%";
            RAMAVERAGE.Text = $"RAM AVERAGE: {RamAverage}%";
            RAMMAX.Text = $"RAM MAX: {RAMMAX}%";
            DURATION.Text = $"DURATION: {Duration} seconds";
        }
    }
}