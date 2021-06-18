using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.Annotations;
using LiveCharts;
using LiveCharts.Wpf;
using TaskManager.Utils;
using System.Windows.Data;

namespace TaskManager.Models
{
    /*
     * Process data class encapsulating all necessary data.
     * To gather information about processes we use System.Diagnostics .
     */
    public class MainContext : NotifyPropertyChanged
    {
        public SeriesCollection SeriesCollection { get; set; }
        
        public WindowsPC PcInfo { get; }
        
        public ObservableCollection<Process> Processes { get; set; }

        private readonly object _itemsLock = new object();
        
        public MainContext()
        {
            PcInfo = new WindowsPC();
            Processes = new ObservableCollection<Process>(Process.GetProcesses());
            
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> {3, 5, 7, 4}
                },
                new ColumnSeries
                {
                    Values = new ChartValues<decimal> {5, 6, 2, 7}
                }
            };
            
            BindingOperations.EnableCollectionSynchronization(Processes, _itemsLock);
            Task.Run(Updater);
        }
        
        
        /*
         * Updating UI with data 
         */
        
        
        private void Updater()
        {
            while (true)
            {
                Thread.Sleep(2500);
                UpdateProcessesPreview();
            }
        }
        
        /*
         * If we clear the observable and then add the
         * processes one by one it flickers. To avoid that we update
         * the data grid line by line.
         */
        private void UpdateProcessesPreview()
        {
            int current = 0;
            var temp = Process.GetProcesses().ToList();
            while (current < temp.Count)
            {
                if (current >= Processes.Count)
                {
                    Processes.Add(temp[current]);
                }
                else
                {
                    Processes[current] = temp[current];
                }

                ++current;
            }

            int count = Processes.Count;
            while (current < count)
            {
                // Remove old data
                Processes.RemoveAt(current);
                ++current;
            }
        }
        

    }
}