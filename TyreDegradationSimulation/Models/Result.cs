using System.Windows.Media;

namespace TyreDegradationSimulation.Models
{
    public class Result
    {
        public int Average { get; set; }
        public int Mode { get; set; }
        public int Range { get; set; }
    }

    public class ResultColors
    {
        public ResultColors()
        {
            AverageColor = new SolidColorBrush(Colors.White);
            ModeColor = new SolidColorBrush(Colors.White);
            RangeColor = new SolidColorBrush(Colors.White);
        }
        public Brush AverageColor { get; set; }
        public Brush ModeColor { get; set; }
        public Brush RangeColor { get; set; }
    }

}
