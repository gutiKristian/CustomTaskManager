using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using TaskManager.Annotations;
using LiveCharts;
using LiveCharts.Wpf;
using TaskManager.Utils;

namespace TaskManager.Models
{
    /*
     * Process data class encapsulating all necessary data.
     * To gather information about processes we use System.Diagnostics .
     */
    public class CustomProcess : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged; 
        public SeriesCollection SeriesCollection { get; set; }

        public ConnectionState ConnectionState;

        public WindowsPC PcInfo { get; }

        public CustomProcess()
        {
            PcInfo = new WindowsPC();
            
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
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}