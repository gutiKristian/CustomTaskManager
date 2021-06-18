using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.Models;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private MainContext context;

        public MainWindow()
        {
            InitializeComponent();
            // Create context variable and set the context
            context = new MainContext();
            
            
            this.DataContext = context;

            var l = new List<string>();
            
            for (int i = 0; i < 100; i++)
            {
                l.Add(i.ToString());
            }

            slY.Labels = l;
            slY.Title = "CPU usage in %";
            slY.FontSize = 14;
            slY.FontWeight = FontWeight.FromOpenTypeWeight(20);
            slx.Title = "";
            slx.ShowLabels = false;

            ramY.Labels = l;
            ramY.Title = "RAM usage in %";
            ramY.FontSize = 14;
            ramY.FontWeight = FontWeight.FromOpenTypeWeight(20);
            ramX.Title = "";
            ramX.ShowLabels = false;

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