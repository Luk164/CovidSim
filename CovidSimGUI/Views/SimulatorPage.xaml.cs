using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CovidSim;
using CovidSimGUI.Helpers;
using LiveCharts;
using LiveCharts.Wpf;

namespace CovidSimGUI.Views
{
    public partial class SimulatorPage : Page, INotifyPropertyChanged
    {
        //Simulation values
        public uint CitizenCount { get; set; } = 10000;
        public uint MedicalStaffCount { get; set; } = 20;
        public uint FirstResponderCount { get; set; } = 10;
        public uint MilitaryCount { get; set; } = 10;
        public uint InfectedCitizenCount { get; set; } = 10;
        public uint DayCount { get; set; } = 5;

        public SeriesCollection SeriesCollection { get; set; }


        private readonly LineSeries _healthyLine;

        public SimulatorPage()
        {
            InitializeComponent();
            Console.SetOut(new MultiTextWriter(new ControlWriter(textBox), Console.Out));

            _healthyLine = new LineSeries
            {
                Values = new ChartValues<double>()
            };

            SeriesCollection = new SeriesCollection
            {
                _healthyLine
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

        private void bt_testClick(object sender, RoutedEventArgs e)
        {
            _healthyLine.Values.Add((double)5);
        }

        private async void Bt_start(object sender, RoutedEventArgs e)
        {
            var sim = new Simulator(CitizenCount, MedicalStaffCount, FirstResponderCount, MilitaryCount, InfectedCitizenCount);
            sim.EndOfDayEvent += (o, args) =>
            {
                _healthyLine.Values.Add((double) args.HealthyCount);
            };

            await Task.Run(() =>
            {
                for (var i = 0; i < DayCount; i++)
                {
                    sim.Day();
                }
            });
        }
    }
}
