using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
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

        public ushort MeetingCount
        {
            get => Simulator.MeetingCount;
            set => Simulator.MeetingCount = value;
        }

        private readonly LineSeries _healthyLine;
        private readonly LineSeries _asymptomaticLine;
        private readonly LineSeries _symptomsLine;
        private readonly LineSeries _seriouslyIllLine;
        private readonly LineSeries _deceasedLine;
        private readonly LineSeries _immuneLine;

        private Simulator Simulator { get; init; }

        public SimulatorPage()
        {
            InitializeComponent();
            Console.SetOut(new MultiTextWriter(new ControlWriter(textBox), Console.Out));

            Simulator = new Simulator(CitizenCount, MedicalStaffCount, FirstResponderCount, MilitaryCount, InfectedCitizenCount);

            _healthyLine = new LineSeries
            {
                Values = new ChartValues<double> {CitizenCount + MedicalStaffCount + FirstResponderCount + MilitaryCount},
                Title = "Healthy",
                PointGeometrySize = 0
            };
            
            _asymptomaticLine = new LineSeries
            {
                Values = new ChartValues<double> {0},
                Title = "Asymptomatic",
                PointGeometrySize = 0
            };

            _symptomsLine = new LineSeries
            {
                Values = new ChartValues<double> {InfectedCitizenCount},
                Title = "Infected",
                PointGeometrySize = 0
            };

            _seriouslyIllLine = new LineSeries
            {
                Values = new ChartValues<double> {0},
                Title = "Serious",
                PointGeometrySize = 0
            };

            _immuneLine = new LineSeries
            {
                Values = new ChartValues<double> {0},
                Title = "Immune",
                PointGeometrySize = 0
            };

            _deceasedLine = new LineSeries
            {
                Values = new ChartValues<double> {0},
                Title = "Deceased",
                PointGeometrySize = 0
            };

            SeriesCollection = new SeriesCollection
            {
                _healthyLine,
                _asymptomaticLine,
                _symptomsLine,
                _seriouslyIllLine,
                _immuneLine,
                _deceasedLine
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
            ((Button) sender).IsEnabled = false;

            var sim = new Simulator(CitizenCount, MedicalStaffCount, FirstResponderCount, MilitaryCount, InfectedCitizenCount);
            sim.EndOfDayEvent += (o, args) =>
            {
                _healthyLine.Values.Add((double) args.HealthyCount);
                _asymptomaticLine.Values.Add((double) args.AsymptomaticCount);
                _symptomsLine.Values.Add((double) args.SymptomsCount);
                _seriouslyIllLine.Values.Add((double) args.SeriouslyIllCount);
                _immuneLine.Values.Add((double) args.ImmuneCount);
                _deceasedLine.Values.Add((double) args.DeceasedCount);

                // ReSharper disable once PossibleLossOfFraction Don't care
                ProgressBar.Dispatcher.Invoke(()=> {
                    // Code causing the exception or requires UI thread access
                    ProgressBar.Value = Math.Round(args.day / DayCount * 100.0);
                });
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
