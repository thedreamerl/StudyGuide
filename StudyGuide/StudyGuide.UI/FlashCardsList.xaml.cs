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
    /// Логика взаимодействия для FlashCardsList.xaml
    /// </summary>
    public partial class FlashCardsList : Window
    {
        ScheduleViewModel schedule;
        public FlashCardsList(ScheduleViewModel s)
        {
            InitializeComponent();
            schedule = s;
            var cards = Factory.Default.GetFlashCardsRepo().AllFlashCards(s);
            if (cards.Count() == 0)
            {
                MessageBoxResult result = MessageBox.Show("There are no flash cards yet, do you want create some?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    FlashCards fc = new FlashCards(schedule);
                    fc.ShowDialog();
                    UpdateTable();
                }
            }
        }
        private void UpdateTable()
        {
            flashcardsDataGrid.ItemsSource = Factory.Default.GetFlashCardsRepo().AllFlashCards(schedule);
        }
    }
}
