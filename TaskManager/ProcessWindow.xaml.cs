using System.Diagnostics;
using System.Windows;
using TaskManager.Models;

namespace TaskManager
{
    public partial class ProcessWindow : Window
    {
        public ProcessWindow(Process data)
        {
            InitializeComponent();
            this.DataContext = new ProcessContext(data);
        }
    }
}