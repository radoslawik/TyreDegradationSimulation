using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TyreDegradationSimulation.Models;

namespace TyreDegradationSimulation.ViewModels
{
    public class ResultViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }

        }

        private Result results;
        public Result Results
        {
            get { return results; }
            set
            {
                results = value;
                ResultColors rs = new ResultColors();
                if (value != null)
                {
                    rs.AverageColor = new SolidColorBrush(GetColor(value.Average));
                    rs.ModeColor = new SolidColorBrush(GetColor(value.Mode));
                    rs.RangeColor = new SolidColorBrush(GetColor(value.Range));
                }
                ResultBackground = rs;
                NotifyPropertyChanged("Results");
                NotifyPropertyChanged("ResultBackground");
            }

        }
        private ResultColors resultBackground;
        public ResultColors ResultBackground
        {
            get { return resultBackground; }
            set { resultBackground = value; NotifyPropertyChanged("ResultBackground"); }
        }

        private Color GetColor(int val)
        {
            Color color = new Color();
            if (val < 1000)
                color = Colors.PaleGreen;
            else if (val >= 1000 && val < 3000)
                color = Colors.Yellow;
            else
                color = Colors.Red;
            return color;
        }

    }
}
