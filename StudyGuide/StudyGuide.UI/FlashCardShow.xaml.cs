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
    /// Логика взаимодействия для FlashCardShow.xaml
    /// </summary>
    public partial class FlashCardShow : Window
    {
        FlashCardViewModel flashCard;
        ScheduleViewModel schedule;
        public FlashCardShow(FlashCardViewModel fc, ScheduleViewModel s)
        {           
            InitializeComponent();
            flashCard = fc;
            schedule = s;
            textblock1.Text = flashCard.Term;
            textblock1.FontSize = 20;
            wrongButton.Visibility = Visibility.Hidden;
            rightButton.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textblock1.Text = flashCard.Definition;
            textblock1.FontSize = 15;
            wrongButton.Visibility = Visibility.Visible;
            rightButton.Visibility = Visibility.Visible;
            checkButton.Visibility = Visibility.Hidden;

        }

        private void wrongButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void rightButton_Click(object sender, RoutedEventArgs e)
        {
            Factory.Default.GetFlashCardsRepo().LevelUp(flashCard, schedule);
            this.Close();
        }
    }

    }
