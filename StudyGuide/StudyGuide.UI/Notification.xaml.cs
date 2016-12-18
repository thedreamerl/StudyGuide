using StudyGuide.Logic;
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
    /// Interaction logic for Notification.xaml 
    /// </summary> 
    public partial class Notification : Window
    {
        IEnumerable<TaskViewModel> tasks;
        StudyPlanViewModel studyPlan;
        public Notification(StudyPlanViewModel s, IEnumerable<TaskViewModel> t)
        {
            InitializeComponent();
            Subject_Name.Text = s.Subject;
            WorkType_Name.Text = s.WorkType;
            tasks = t;
            studyPlan = s;
            Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
            {
                var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
                var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
                var corner = transform.Transform(new Point(workingArea.Right, workingArea.Bottom));

                this.Left = corner.X - this.ActualWidth - 100;
                this.Top = corner.Y - this.ActualHeight;
            }));

        }

        private void PomodoroButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var pomodoro = new Pomodoro(15, 5, tasks);
            pomodoro.ShowDialog();
            this.Show();
        }

        private void FlashCardsButton_Click(object sender, RoutedEventArgs e)
        {
            var cards = Factory.Default.GetFlashCardsRepo().AllFlashCards(new ScheduleViewModel { Subject = studyPlan.Subject, WorkType = studyPlan.WorkType });
            if (cards.Count() == 0)
            {
                MessageBox.Show("There are no cards to revise", "Error");
                FlashCardsButton.IsEnabled = false;
                return;
            }
            foreach (var card in cards)
            {
                FlashCardShow fc = new FlashCardShow(card, new ScheduleViewModel { Subject = studyPlan.Subject, WorkType = studyPlan.WorkType });
                this.Hide();
                fc.ShowDialog();
            }
            this.Show();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}