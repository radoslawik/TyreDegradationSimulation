using System.Windows;
using System.Windows.Controls;
using TyreDegradationSimulation.Views;
using TyreDegradationSimulation.ViewModels;
using System;

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
        }

        private async void ComboBox_TrackChange(object sender, SelectionChangedEventArgs e)
        {
            mvm.Temperature = await mvm.GetCurrentTemperature();
            if (mvm.CheckSelection())
                UpdateResults();
            return;
        }

        private void ComboBox_CheckTyresSetup(object sender, SelectionChangedEventArgs e)
        {
            if (mvm.CheckRules())
            {
                if (mvm.CheckSelection())
                {
                    UpdateResults();
                }         
            }
            else
            {
                (sender as ComboBox).SelectedItem = null;
                ClearResults();
            }           
            return;
        }
        private void ComboBox_ManualTemp(object sender, RoutedEventArgs e)
        {
            if (mvm.CheckSelection())
                UpdateResults();
            else
                ClearResults();
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
    
        public void UpdateResults()
        {
            rvmFL.Results = mvm.CalculateTyreDegradation(mvm.SelectedFL);
            rvmFR.Results = mvm.CalculateTyreDegradation(mvm.SelectedFR);
            rvmRL.Results = mvm.CalculateTyreDegradation(mvm.SelectedRL);
            rvmRR.Results = mvm.CalculateTyreDegradation(mvm.SelectedRR);

            frLt.DataContext = rvmFL;     
            frRt.DataContext = rvmFR;       
            rrLt.DataContext = rvmRL;  
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
