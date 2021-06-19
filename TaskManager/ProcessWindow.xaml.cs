using System;
using System.Diagnostics;
using System.Windows;
using TaskManager.Models;
using System.Collections.Generic;

namespace TaskManager
{
    public partial class ProcessWindow : Window
    {
        private ProcessContext context;
        public ProcessWindow(Process data)
        {
            InitializeComponent();
            context = new ProcessContext(data);
            this.DataContext = context;

            var l = new List<string>();

            for (int i = 0; i < 100; i++)
            {
                l.Add(i.ToString());
            }

            cpy.Labels = l;
            

        }

        private void Record_Click(object sender, RoutedEventArgs e)
        {
            recordBtn.IsEnabled = false;
            saveRecordBtn.IsEnabled = true;
            context.Record = true;
            context.RecordStart = DateTime.Now;
        }

        private void SaveRecord_Click(object sender, RoutedEventArgs e)
        {
            recordBtn.IsEnabled = true;
            saveRecordBtn.IsEnabled = false;
            context.SaveRecord = true;
        }
    }
}