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

namespace StudyGuide.UI
{
    /// <summary>
    /// Логика взаимодействия для Deadline.xaml
    /// </summary>
    public partial class Deadline : Window
    {
        ScheduleViewModel schedule;
        public Deadline(ScheduleViewModel s)
        {
            InitializeComponent();
            SubjectText.Text = s.Subject;
            TypeText.Text = s.WorkType;
            schedule = s;
            var studyPlans = Factory.Default.GetStudyPlanRepo().ShowAll(schedule);
            foreach (var sp in studyPlans)
            {
                StudyPlanList.Items.Add(sp.Begin.ToLongTimeString());
                var tasks = Factory.Default.GetTasksRepo().ShowAll(sp.Begin, schedule);
                foreach (var task in tasks)
                {
                    StudyPlanList.Items.Add(task.Name);
                }
            }
        }

        private void Button_CardList_Click(object sender, RoutedEventArgs e)
        {
            var fc = new FlashCardsList(schedule);
            fc.Show();
        }
        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete deadline? This action can't be undone", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;
            Factory.Default.GetScheduleRepo().DeleteSchedule(schedule);
            this.Close();
        }
    }
}
