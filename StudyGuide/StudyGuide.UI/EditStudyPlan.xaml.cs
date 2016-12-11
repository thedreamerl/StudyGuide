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

        public EditStudyPlan()
        {
            InitializeComponent();
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
    }
}
