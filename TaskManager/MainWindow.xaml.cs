using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        // Demo purpose ! It is goind to be moved elsewhere
        private List<Process> _processes = new List<Process>();
        
        public MainWindow()
        {
            InitializeComponent();

            _processes = Process.GetProcesses().ToList();
            processList.ItemsSource = _processes;
        }
    }
}