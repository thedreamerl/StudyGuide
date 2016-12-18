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
        List<FlashCardViewModel> cards;
        public FlashCardsList(ScheduleViewModel s)
        {
            InitializeComponent();
            schedule = s;
            UpdateTable();
            Factory.Default.GetFlashCardsRepo().ShowMessgae += Message;
        }
        private async void UpdateFlashCardList()
        {
            cards = (List<FlashCardViewModel>)await Factory.Default.GetFlashCardsRepo().AllFlashCards(schedule);
            flashcardsDataGrid.ItemsSource = cards;
        }
        private void Message(string m)
        {
            MessageBox.Show(m);
            UpdateFlashCardList();
        }
        private async void UpdateTable()
        {
            cards = (List<FlashCardViewModel>) await Factory.Default.GetFlashCardsRepo().AllFlashCards(schedule);
            if (cards.Count() == 0)
            {
                MessageBoxResult result = MessageBox.Show("There are no flash cards yet, do you want create some?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    FlashCards fc = new FlashCards(schedule);
                    fc.ShowDialog();
                }
            }
            UpdateFlashCardList();

        }

        private void Revise_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateTable();
            foreach (var card in cards)
            {
                FlashCardShow fc = new FlashCardShow(card, schedule);
                this.Hide();
                fc.ShowDialog();
            }
            UpdateFlashCardList();
            this.Show();
        }
    }
}
