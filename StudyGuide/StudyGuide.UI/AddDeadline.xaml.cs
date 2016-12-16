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
using StudyGuide.Logic;
using StudyGuide.Logic.Models;

namespace StudyGuide.UI
{
    /// <summary>
    /// Логика взаимодействия для AddDeadline.xaml
    /// </summary>
    public partial class AddDeadline : Window
    {
        public AddDeadline()
        {
            InitializeComponent();
            Subjects.ItemsSource = Factory.Default.GetSubjectRepo().ShowAll();
            WorkTypes.ItemsSource = Factory.Default.GetWorkTypeRepo().ShowAll();
            TextBlock_Days.Visibility = Visibility.Hidden;
            Days.Visibility = Visibility.Hidden;
            HelperText.Visibility = Visibility.Hidden;
            Days.DisplayDateStart = DateTime.Now;
            Deadline.DisplayDateStart = DateTime.Now.AddDays(1);
        }

        private void NewSubject_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(Subjects.Text))
                try
                {
                    Factory.Default.GetSubjectRepo().AddNew(Subjects.Text);
                    MessageBox.Show("Subject was successfully added!");
                    Subjects.ItemsSource = Factory.Default.GetSubjectRepo().ShowAll();
                    Subjects.SelectedIndex = Subjects.Items.Count - 1;
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void NewWorkType_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(WorkTypes.Text))
                try
                {
                    Factory.Default.GetWorkTypeRepo().AddNew(WorkTypes.Text);
                    MessageBox.Show("Subject was successfully added!");
                    WorkTypes.ItemsSource = Factory.Default.GetWorkTypeRepo().ShowAll();
                    WorkTypes.SelectedItem = WorkTypes.Items.Count - 1;
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void Deadline_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBlock_Days.Visibility = Visibility.Visible;
            Days.Visibility = Visibility.Visible;
            HelperText.Visibility = Visibility.Visible;
            Days.DisplayDateEnd = Deadline.SelectedDate;
        }
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (Subjects.SelectedIndex == -1)
            {
                MessageBox.Show("You haven't selected subject or added new one to the collection");
                return;
            }
            if (WorkTypes.SelectedIndex == -1)
            {
                MessageBox.Show("You haven't selected work type or added new one to the collection");
                return;
            }
            if (Deadline.SelectedDate == null)
            {
                MessageBox.Show("You haven't selected date of the deadline");
                return;
            }
            if (Days.SelectedDates == null || Days.SelectedDates.Count == 0)
            {
                MessageBox.Show("You haven't selected any dates of the study plan");
                return;
            }
            var Schedule = new ScheduleViewModel
            {
                Subject = Subjects.Text,
                WorkType = WorkTypes.Text,
                Deadline = (DateTime)Deadline.SelectedDate
            };
            try
            {
                Factory.Default.GetScheduleRepo().AddNew(Schedule);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            this.Hide();
            foreach (var date in Days.SelectedDates)
            {
                var studyPlan = new EditStudyPlan(Schedule, date);
                studyPlan.ShowDialog();
            }
            MessageBoxResult result = MessageBox.Show("Do you want to create flash cards for revising?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var fc = new FlashCards(Schedule);
                fc.ShowDialog();
            }
            MessageBox.Show("Congratulations! New deadline was created!");
            this.Close();
        }
    }
}
