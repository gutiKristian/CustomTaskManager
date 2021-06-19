using System.Diagnostics;
using System.Windows;
using TaskManager.Models;
using System.Collections.Generic;

namespace TaskManager
{
    public partial class ProcessWindow : Window
    {
        public ProcessWindow(Process data)
        {
            InitializeComponent();
            this.DataContext = new ProcessContext(data);

            var l = new List<string>();

            for (int i = 0; i < 100; i++)
            {
                l.Add(i.ToString());
            }

            cpy.Labels = l;
            

        }
    }
}