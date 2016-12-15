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
using Microsoft.Win32;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using System.Globalization;
using StudyGuide.Logic;
using StudyGuide.Logic.Models;

namespace StudyGuide.UI
{
    
    public partial class MainWindow : Window
    {
        RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        public MainWindow()
        {
            reg.SetValue("StudyGuide", System.Reflection.Assembly.GetExecutingAssembly().Location);
            InitializeComponent();
            StartClock();
            UpdateListSource();
            Factory.Default.GetScheduleRepo().UpdateList += UpdateListSource;
            UpdateTodayPlan();
            Factory.Default.GetStudyPlanRepo().AddEvent += UpdateTodayPlan;
        }
        private void UpdateListSource()
        {
            listBoxDeadlines.ItemsSource = Factory.Default.GetScheduleRepo().ShowAll();
        }
        private void UpdateTodayPlan()
        {
            listBoxStudyPlan.ItemsSource = Factory.Default.GetStudyPlanRepo().GetTodayStudyPlans();
        }

        private void StartClock()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickEvent;
            timer.Start(); 
        }

        private void tickEvent(object sender, EventArgs e)
        {

            timeNow.Text = DateTime.Now.ToString("T");
            dayNow.Text = DateTime.Now.ToString("D",CultureInfo.CreateSpecificCulture("en-US"));
        }

        private void AddingButton_Click(object sender, RoutedEventArgs e)
        {
            AddDeadline add = new AddDeadline();
            add.ShowDialog();
        }

        private void listBoxDeadlines_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // stackoverflow.com/questions/4454423/c-sharp-listbox-item-double-click-event 
            DependencyObject obj = (DependencyObject)e.OriginalSource;

            while (obj != null && obj != listBoxDeadlines)
            {
                if (obj.GetType() == typeof(ListBoxItem))
                {
                    var temp = listBoxDeadlines.SelectedItem;
                    if (temp.GetType() == typeof(ScheduleViewModel))
                    {
                        var schedule = temp as ScheduleViewModel;
                        // some work with windows
                    }
                    break;
                }
                obj = VisualTreeHelper.GetParent(obj);
            }
        }
    }
}
