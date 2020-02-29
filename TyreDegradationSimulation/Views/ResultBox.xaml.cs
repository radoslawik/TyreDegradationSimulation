﻿using System;
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

namespace TyreDegradationSimulation.Views
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultBox : Grid
    {
        // string tyrePosition, string average, string mode, string range
        public ResultBox(string tyrePosition)
        {
            InitializeComponent();
            tbResultName.Text = tyrePosition;
        }
    }
}
