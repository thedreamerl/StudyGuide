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
    /// Логика взаимодействия для EditStudyPlan.xaml
    /// </summary>
    public partial class EditStudyPlan : Window
    {
        List<string> Tasks = new List<string>();
        ScheduleViewModel Schedule;
        DateTime Date;

        public EditStudyPlan(ScheduleViewModel schedule, DateTime date)
        {
            InitializeComponent();
            TextBlockDate.Text = date.ToShortDateString();
            Date = date;
            Schedule = schedule;
        }
        private void AddingButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(TaskName.Text))
            {
                Tasks.Add(TaskName.Text);
                TasksList.Items.Add(TaskName.Text);
                TaskName.Text = "";
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            // нужно сделать проверку регулярным выражением времени 
            if (TasksList.Items == null || TasksList.Items.Count == 0)
            {
                MessageBox.Show("You haven't added any tasks, please add some");
                return;
            }
            var temp = Time.Text.Split(':');
            Date.AddHours(double.Parse(temp[0]));
            Date.AddMinutes(double.Parse(temp[1]));
            Factory.Default.GetStudyPlanRepo().AddNew(Date, Schedule);
            var list = new List<string>();
            foreach (var item in TasksList.Items)
            {
                list.Add(item.ToString());
            }
            Factory.Default.GetTasksRepo().AddNewTasks(list, Date, Schedule);
            MessageBox.Show("Study plan item has been added succussfully!");
            this.Hide();
        }
    }
}
