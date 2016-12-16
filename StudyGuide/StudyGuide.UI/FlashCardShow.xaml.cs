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
        public FlashCardShow(FlashCardViewModel fc)
        {           
             flashCard = fc;
            textblock1.Text = flashCard.Term;
            InitializeComponent();
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
    }

    }
