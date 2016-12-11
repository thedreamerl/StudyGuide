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
    /// Логика взаимодействия для Pomodoro.xaml
    /// </summary>
    public partial class Pomodoro : Window
    {
        public Pomodoro()
        {
            InitializeComponent();
        }
        void ToRest()
        {
            workorrestTextBlock.Text = "Rest!:)";
            Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
            doneButton.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
        }
        void ToWork()
        {
            workorrestTextBlock.Text = "Work:)";
            Background = new SolidColorBrush(Color.FromRgb(139, 0, 0));
            doneButton.Background = new SolidColorBrush(Color.FromRgb(139, 0, 0));
        }

        private void doneButton_Click(object sender, RoutedEventArgs e)
        {
            // to be edited... 
        }
    }
}
