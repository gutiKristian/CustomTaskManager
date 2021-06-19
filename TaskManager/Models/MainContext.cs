using System;
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
using System.Windows;
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
        public SeriesCollection CpuCollection { get; set; }
        public SeriesCollection RamCollection { get; set; }
        
        public WindowsPC PcInfo { get; }
        
        public ObservableCollection<Process> Processes { get; set; }

        private readonly object _itemsLock = new object();
        
        public MainContext()
        {
            PcInfo = new WindowsPC();
            Processes = new ObservableCollection<Process>(Process.GetProcesses());
            
            CpuCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<int> {0, 0, 0, 0, 0, 0, 0, 0, 0},
                    Title = "CPU",
                    LineSmoothness = 1,
                    LabelPoint = (ChartPoint p) =>  $"{p.Y}%",
                }
            };
            
            RamCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> {0, 0, 0, 0, 0, 0, 0, 0, 0},
                    Title = "RAM",
                    LineSmoothness = 1,
                    LabelPoint = (ChartPoint p) =>  $"{p.Y}%",
                }
            };
            
           
            
            BindingOperations.EnableCollectionSynchronization(Processes, _itemsLock);
            BindingOperations.EnableCollectionSynchronization(CpuCollection, _itemsLock);
            BindingOperations.EnableCollectionSynchronization(RamCollection, _itemsLock);
            Task.Run(Updater);
            Task.Run(UpdateResourceUsage);
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
            
            int count = Processes.Count;

            while (current < temp.Count)
            {
                if (current >= count)
                {
                    Processes.Add(temp[current]);
                }
                else
                {
                    Processes[current] = temp[current];
                }

                ++current;
            }


            for (; current < count - 1; ++current)
            {
                Processes.RemoveAt(current);
            }
            
        }

        private void UpdateResourceUsage()
        {
            
            while (true)
            {
                Thread.Sleep(1000);
                
                CpuCollection[0].Values.Add(PcInfo.CpuStatus());
                CpuCollection[0].Values.RemoveAt(0);
                
                RamCollection[0].Values.Add(PcInfo.RamStatus());
                RamCollection[0].Values.RemoveAt(0);
            }
        }

    }
}