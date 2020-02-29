using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TyreDegradationSimulation.Views;
using TyreDegradationSimulation.Models;
using TyreDegradationSimulation.Handlers;
using TyreDegradationSimulation.ViewModels;


namespace TyreDegradationSimulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mvm;

        public MainWindow()
        {
            mvm = new MainViewModel();
            InitializeComponent();

            this.DataContext = mvm;
            AddSelection();
            PopulateResultWindows();
 
        }
        private async void ComboBox_TrackChange(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(mvm.TrackLocation);
            string cityname = mvm.TrackLocation;
            Temperature temp = await mvm.TempHandler.GetTemperatureInfo(cityname);
            mvm.Temperature = temp.Main.Temp.ToString();
        }

        public void PopulateResultWindows()
        {
            ResultBox frLt = new ResultBox("Front Left");
            ResultBox frRt = new ResultBox("Front Right");
            ResultBox rrLt = new ResultBox("Rear Left");
            ResultBox rrRt = new ResultBox("Rear Right");

            resultGrid.Children.Add(frLt);
            Grid.SetRow(frLt, 0);
            Grid.SetColumn(frLt, 0);

            resultGrid.Children.Add(frRt);
            Grid.SetRow(frRt, 0);
            Grid.SetColumn(frRt, 1);

            resultGrid.Children.Add(rrLt);
            Grid.SetRow(rrLt, 1);
            Grid.SetColumn(rrLt, 0);

            resultGrid.Children.Add(rrRt);
            Grid.SetRow(rrRt, 1);
            Grid.SetColumn(rrRt, 1);
            
        }

        public void AddSelection()
        {
            foreach (Tyre tyre in mvm.AvailableTyres)
            {
                switch (tyre.Placement)
                {
                    case "FL":
                        cbTyresFL.Items.Add(tyre.Name);
                        break;
                    case "FR":
                        cbTyresFR.Items.Add(tyre.Name);
                        break;
                    case "RL":
                        cbTyresRL.Items.Add(tyre.Name);
                        break;
                    case "RR":
                        cbTyresRR.Items.Add(tyre.Name);
                        break;
                    default:
                        break;
                }

            }

            foreach (TrackDegrCoef coef in mvm.CoefPoints)
            {
                cbTracks.Items.Add(coef.TrackLocation);
            }
        }


    }
}
