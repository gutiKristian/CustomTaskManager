using System.Diagnostics;
using System.Windows;

namespace TaskManager
{
    public partial class ProcessWindow : Window
    {
        public Process Data { get; }
        public ProcessWindow(Process data)
        {
            Data = data;
            
            InitializeComponent();
            
            selectedProcess.Text = $"Selected process: {Data.ProcessName}";
        }
    }
}