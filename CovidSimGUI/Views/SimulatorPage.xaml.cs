using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace CovidSimGUI.Views
{
    public partial class SimulatorPage : Page, INotifyPropertyChanged
    {
        public PlotModel PlotModel { get; set; } = new PlotModel();

        public SimulatorPage()
        {
            InitializeComponent();
            PlotModel.Series.Add(new FunctionSeries(Math.Sin, 0, 10, 0.1, "sin(x)"));
            PlotModel.TextColor = OxyColors.White;
            PlotModel.DefaultColors =  new List<OxyColor>
            {
                OxyColors.GhostWhite
            };
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
