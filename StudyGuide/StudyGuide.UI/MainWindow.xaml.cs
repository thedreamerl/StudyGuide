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
using StudyGuide.Logic.EntityRepos;
using Hardcodet.Wpf.TaskbarNotification;

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
            NotificationRepo.Notify += ShowNotificationWindow;
        }
        private void ShowNotificationWindow(StudyPlanViewModel s, IEnumerable<TaskViewModel> tasks)
        {
            Notification n = new Notification(s, tasks);
            n.Show();
        }
        private async void UpdateListSource()
        {
            listBoxDeadlines.ItemsSource = await Factory.Default.GetScheduleRepo().ShowAll();
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
        int _previousMinute = -1; // ++++++ 
        int _previousDay = -1;
        private void tickEvent(object sender, EventArgs e)
        {
            if (DateTime.Now.Minute != _previousMinute)
            {
                _previousMinute = DateTime.Now.Minute;
                NotificationRepo.CheckStudyPlans();
            }
            if (DateTime.Now.Day != _previousDay)
            {
                _previousDay = DateTime.Now.Day;
                UpdateTodayPlan();
            }
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
                        var deadline = new Deadline(schedule);
                        deadline.ShowDialog();
                    }
                    break;
                }
                obj = VisualTreeHelper.GetParent(obj);
            }
        }
        bool _finalShutdown = false;
        bool _firstTimeHint = true;
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_finalShutdown)
            {
                e.Cancel = true;
                Hide();
                taskBarIcon.Visibility = Visibility.Visible;
                if (_firstTimeHint)
                {
                    taskBarIcon.ShowBalloonTip("Study Guide", "The application is still running",
                    BalloonIcon.Info);
                    _firstTimeHint = false;
                }
            }
        }
        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            _finalShutdown = true;
            taskBarIcon.Dispose();
            Application.Current.Shutdown();
        }

        private void MenuItemRestore_Click(object sender, RoutedEventArgs e)
        {
            Show();
            taskBarIcon.Visibility = Visibility.Hidden;
        }
    }
}
