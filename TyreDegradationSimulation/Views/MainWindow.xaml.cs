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


namespace TyreDegradationSimulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ResultBox> resultWindows;
        public MainWindow()
        {
            InitializeComponent();
            PopulateResultWindows();

            XmlHandler xmlHandler = new XmlHandler();
            List<Tyre> tyres = xmlHandler.DeserializeXml("Resources/TyresXML.xml");
            Console.WriteLine(tyres[0].Name);

            TrackDegrCofHandler cofHandler = new TrackDegrCofHandler();
            List<TrackDegrCof> coefs = cofHandler.DeserializeTxt("Resources/TrackDegradationCoefficients.txt");
            Console.WriteLine(coefs[0].TrackLocation);
            Console.WriteLine(coefs[0].TrackName);

            foreach (Tyre tyre in tyres)
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

        }

        public void PopulateResultWindows()
        {
            ResultBox frLt = new ResultBox();
            ResultBox frRt = new ResultBox();
            ResultBox rrLt = new ResultBox();
            ResultBox rrRt = new ResultBox();

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

        public void PopulateResultWindows2()
        {
            resultWindows = new List<ResultBox>
            {
                new ResultBox{ },
                new ResultBox{ },
                new ResultBox{ },
                new ResultBox{ },
             };

        }
    }
}
