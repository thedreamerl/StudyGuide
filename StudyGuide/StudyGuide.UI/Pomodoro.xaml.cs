using StudyGuide.Logic.Models;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace StudyGuide.UI
{
    /// <summary>
    /// Логика взаимодействия для Pomodoro.xaml
    /// </summary>
    public partial class Pomodoro : Window
    {
        TimeSpan mw;
        TimeSpan mr;
        TimeSpan temp;
        bool b = true;

        public Pomodoro(int minutesForWork, int minutesForRest, IEnumerable<TaskViewModel> tasks)
        {
            InitializeComponent();
            mw = new TimeSpan(0, minutesForWork, 0);
            mr = new TimeSpan(0, minutesForRest, 0);
            temp = mw;
            StartClock();
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
            TimeSpan ts = new TimeSpan(0, 0, 1);

            temp -= ts;
            if ((temp.Seconds == 0) && (temp.Minutes == 0))
            {
                Timer.Text = "00:00";
                if (b)
                    ToRest();
                else
                    ToWork();
            }
            else
                Timer.Text = $"{temp.Minutes}:{temp.Seconds}";
        }
            void ToRest()
        {
            temp = mr;
            b = false;
            workorrestTextBlock.Text = "Rest!:)";
            Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
            doneButton.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
        }
        void ToWork()
        {
            temp = mw;
            b = true;
            workorrestTextBlock.Text = "Work:)";
            Background = new SolidColorBrush(Color.FromRgb(139, 0, 0));
            doneButton.Background = new SolidColorBrush(Color.FromRgb(139, 0, 0));
        }

        private void doneButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
