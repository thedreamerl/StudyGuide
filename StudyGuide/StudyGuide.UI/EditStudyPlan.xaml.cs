using StudyGuide.Logic;
using StudyGuide.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            Regex timeRegex = new Regex(@"^(\d{1,2}):(\d{2})$");
            Match match = timeRegex.Match(Time.Text);
            if (!match.Success || int.Parse(match.Groups[1].Value) > 23
                || int.Parse(match.Groups[2].Value) > 59)
            {
                MessageBox.Show("The format of time is incorrect");
                Time.Text = "00:00";
                return;
            }
            if (Date.AddHours(int.Parse(match.Groups[1].Value)).AddMinutes(int.Parse(match.Groups[2].Value)) <= DateTime.Now)
            {
                MessageBox.Show("Chosen time is earlier than the current, choose an appropriate one");
                Time.Text = "00:00";
                return;
            }
            if (TasksList.Items == null || TasksList.Items.Count == 0)
            {
                MessageBox.Show("You haven't added any tasks, please add some");
                return;
            }
            Date = Date.AddHours(int.Parse(match.Groups[1].Value)).AddMinutes(int.Parse(match.Groups[2].Value));
            try
            {
                Factory.Default.GetStudyPlanRepo().AddNew(new StudyPlanViewModel { Begin = Date, Subject = Schedule.Subject, WorkType = Schedule.WorkType });
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            var list = new List<string>();
            foreach (var item in TasksList.Items)
            {
                list.Add(item.ToString());
            }
            Factory.Default.GetTasksRepo().AddNewTasks(list, Date, Schedule);
            MessageBox.Show("Study plan item has been added succussfully!");
            this.Close();
        }
    }
}
