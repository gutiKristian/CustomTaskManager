using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.Models;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        
        // Demo purpose ! It is goind to be moved elsewhere
        private List<Process> _processes;
        
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new CustomProcess();
            
            _processes = Process.GetProcesses().ToList();
            processGrid.ItemsSource = _processes;
            // processList.ItemsSource = _processes;
        }

        private void Process_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Process data = (sender as DataGridRow)?.DataContext as Process;
            if (data is null)
            {
                MessageBox.Show("NULL");
                return;
            }
            // Create dialog with the process
            // register events for exit the dialog and also exit process -> generate log file
            ProcessWindow processWindow = new ProcessWindow(data);
            processWindow.Show();
        }
    }
}