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
        private ResultViewModel rvmFL, rvmFR, rvmRL, rvmRR;
        private ResultBox frLt, frRt, rrLt, rrRt;

        public MainWindow()
        {
            mvm = new MainViewModel();
            rvmFL = new ResultViewModel();
            rvmFR = new ResultViewModel();
            rvmRL = new ResultViewModel();
            rvmRR = new ResultViewModel();
            InitializeComponent();

            this.DataContext = mvm;
            PopulateResultWindows();
            ClearResults();
            mvm.SelectedTrackIndex = -1;
 
        }
        private async void ComboBox_TrackChange(object sender, SelectionChangedEventArgs e)
        {
            mvm.Temperature = await mvm.GetCurrentTemperature();
            if (CheckSelection())
                UpdateResults();
            return;

        }

        private void ComboBox_CheckTyresSetup(object sender, SelectionChangedEventArgs e)
        {
            if (!CheckRules())
            {
                (sender as ComboBox).SelectedItem = null;
                ClearResults();
            }
            else
            {
                if (CheckSelection())
                {
                    Console.WriteLine("show results");
                    UpdateResults();
                }
                       
            }
                
            return;
        }
        private void ComboBox_ManualTemp(object sender, RoutedEventArgs e)
        {
            if (CheckSelection())
                UpdateResults();
            return;
        }
        public void PopulateResultWindows()
        {       
            frLt = new ResultBox("Front Left");
            frRt = new ResultBox("Front Right");   
            rrLt = new ResultBox("Rear Left");   
            rrRt = new ResultBox("Rear Right");      

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

        public Result CalculateTyreDegradation(Tyre tyreData)
        {
            Result retval = new Result();
            TrackDegrCoef trackData = mvm.TrackCoefPoints[mvm.SelectedTrackIndex];
            List<int> trackPoints = trackData.Values;
            if (trackPoints.Count < 0)
            {
                Console.WriteLine("wooops track points = 0");
                return null;
            }
                
            double average = trackPoints.Average();
            double range_min = trackPoints.Min();
            double range_max = trackPoints.Max();
            double mode =
                trackPoints
                    .GroupBy(x => x)
                    .OrderByDescending(x => x.Count()).ThenBy(x => x.Key)
                    .Select(x => x.Key)
                    .FirstOrDefault();

            double degPercentage;
            switch (tyreData.Type)
            {
                case "SuperSoft":
                    degPercentage = 0.8;
                    break;
                case "Soft":
                    degPercentage = 0.8;
                    break;
                case "Medium":
                    degPercentage = 0.9;
                    break;
                case "Hard":
                    degPercentage = 0.75;
                    break;
                default:
                    degPercentage = 1;
                    break;

            }
            double tyreCoef = degPercentage * tyreData.DegradationCoefficient;

            int trackTemperature;
            try
            {
                trackTemperature = Int32.Parse(mvm.Temperature);
            }
            catch (FormatException e)
            {
                MessageBox.Show(e.Message, "Temperature error");
                return null;
            }
            retval.Average = (int)Math.Ceiling((average * trackTemperature) / tyreCoef);
            retval.Mode = (int)Math.Ceiling((mode * trackTemperature) / tyreCoef);
            retval.Range = (int)Math.Ceiling((range_max * trackTemperature) / tyreCoef) - (int)Math.Ceiling((range_min * trackTemperature) / tyreCoef);

            return retval;
        }

        public bool CheckRules()
        {
            bool correct = true;

            if (mvm.SelectedFL != null && mvm.SelectedFR != null && mvm.SelectedRL != null && mvm.SelectedRR != null) //all tyres selected
            {
                if ((mvm.SelectedFL.Type != mvm.SelectedFR.Type) || (mvm.SelectedFR.Type != mvm.SelectedRL.Type) || (mvm.SelectedRL.Type != mvm.SelectedRR.Type))
                {
                    Console.WriteLine("wrong types");
                    correct = false;
                }
            }
            if (mvm.SelectedFL != null && mvm.SelectedFR != null) //checking front tyres
            {
                if (mvm.SelectedFL.Family != mvm.SelectedFR.Family)
                {
                    Console.WriteLine("wrong family on front");
                    correct = false;
                }

            }
            if (mvm.SelectedRL != null && mvm.SelectedRR != null) //checking rear tyres
            {
                if (mvm.SelectedRL.Family != mvm.SelectedRR.Family)
                {
                    Console.WriteLine("wrong family on rear");
                    correct = false;
                }
            }
            return correct;

        }

        public bool CheckSelection()
        {
            bool correct = false;
            if (mvm.SelectedFL != null && mvm.SelectedFR != null
                && mvm.SelectedRL != null && mvm.SelectedRR != null
                && mvm.Temperature != "" && mvm.SelectedTrackIndex != -1)
            {
                Console.WriteLine("everything selected");
                correct = true;
            }
            else
            {
                ClearResults();
            }
            Console.WriteLine("NOT everything selected");
            return correct;
        }

        public void UpdateResults()
        {
            rvmFL.Results = CalculateTyreDegradation(mvm.SelectedFL);
            frLt.DataContext = rvmFL;
            rvmFR.Results = CalculateTyreDegradation(mvm.SelectedFR);
            frRt.DataContext = rvmFR;
            rvmRL.Results = CalculateTyreDegradation(mvm.SelectedRL);
            rrLt.DataContext = rvmRL;
            rvmRR.Results = CalculateTyreDegradation(mvm.SelectedRR);
            rrRt.DataContext = rvmRR;
        }

        public void ClearResults()
        {
            rvmFL.Results = null;
            frLt.DataContext = rvmFL;
            rvmFR.Results = null;
            frRt.DataContext = rvmFR;
            rvmRL.Results = null;
            rrLt.DataContext = rvmRL;
            rvmRR.Results = null;
            rrRt.DataContext = rvmRR;

        }



    }
}
