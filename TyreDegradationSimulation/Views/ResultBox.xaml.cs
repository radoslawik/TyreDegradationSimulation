using System.Windows.Controls;

namespace TyreDegradationSimulation.Views
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultBox : Grid
    {
        public ResultBox(string tyrePosition)
        {
            InitializeComponent();
            tbTyrePosition.Text = tyrePosition;
        }
    }
}
