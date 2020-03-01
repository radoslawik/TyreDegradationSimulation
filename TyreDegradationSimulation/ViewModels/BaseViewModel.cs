using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using TyreDegradationSimulation.Models;

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
            set
            {
                try
                {   
                    if(value != "")
                    {
                        int temperatureVal = Int32.Parse(value);
                    }     
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Temperature error");
                    return;
                }
                temperature = value;
                NotifyPropertyChanged("Temperature");
            }
        }

        private AvailableTyresList availableTyres;
        public AvailableTyresList AvailableTyres
        {
            get { return availableTyres; }
            set { availableTyres = value; NotifyPropertyChanged("AvailableTyres"); }
        }

        private List<TrackDegrCoef> trackCoefPoints;
        public List<TrackDegrCoef> TrackCoefPoints
        {
            get { return trackCoefPoints; }
            set { trackCoefPoints = value; NotifyPropertyChanged("TrackCoefPoints"); }
        }

        private int selectedTrackIndex = -1;
        public int SelectedTrackIndex
        {
            get { return selectedTrackIndex; }
            set
            {
                selectedTrackIndex = value;
                if(value != -1)
                    NotifyPropertyChanged("Temperature");
        
                NotifyPropertyChanged("SelectedTrackIndex");
            }
        }
    

        private Tyre selectedFL;
        public Tyre SelectedFL
        {
            get { return selectedFL; }
            set { selectedFL = value; NotifyPropertyChanged("SelectedFL"); }
        }

        private Tyre selectedFR;
        public Tyre SelectedFR
        {
            get { return selectedFR; }
            set { selectedFR = value; NotifyPropertyChanged("SelectedFR"); }
        }

        private Tyre selectedRL;
        public Tyre SelectedRL
        {
            get { return selectedRL; }
            set { selectedRL = value; NotifyPropertyChanged("SelectedRL"); }
        }

        private Tyre selectedRR;
        public Tyre SelectedRR
        {
            get { return selectedRR; }
            set { selectedRR = value; NotifyPropertyChanged("SelectedRR"); }
        }

    }

}
