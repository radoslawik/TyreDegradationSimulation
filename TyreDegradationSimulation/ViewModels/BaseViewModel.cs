using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TyreDegradationSimulation.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }

        }

        private string temperature;
        public string Temperature
        {
            get { return temperature; }
            set { temperature = value; NotifyPropertyChanged("Temperature"); }
        }

        private string trackLocation;
        public string TrackLocation
        {
            get { return trackLocation; }
            set { trackLocation = value; NotifyPropertyChanged("TrackLocation"); }
        }
    }
}
