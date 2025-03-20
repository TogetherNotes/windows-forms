using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using TogetherNotes.ViewModel;

namespace TogetherNotes.Forms
{
    public partial class Home : UserControl
    {
        public ChartValues<int> UserRegistrations { get; set; }
        public List<string> Months { get; set; }

        public Home()
        {
            InitializeComponent();
            LoadChartData();
            this.DataContext = new HomeVM();
        }

        private void LoadChartData()
        {
            Months = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            UserRegistrations = new ChartValues<int> { 10, 25, 30, 50, 40, 60, 90, 70, 85, 100, 120, 150 };
        }
    }
}