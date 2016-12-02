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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudyGuide.UI
{
    /// <summary> 
    /// Логика взаимодействия для MainWindow.xaml 
    /// </summary> 
    public partial class FlashCards : Window
    {
        public FlashCards()
        {
            InitializeComponent();
            headerTextBlock.Visibility = Visibility.Hidden;
            headerTextBox.Visibility = Visibility.Hidden;
            resultTextBlock.Visibility = Visibility.Hidden;
            resultTextBox.Visibility = Visibility.Hidden;
            loadButton.Visibility = Visibility.Hidden;
            createButton.Visibility = Visibility.Hidden;

        }

        bool isEdited = false;

        private void langRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            headerTextBlock.Text = "Word";
            loadButton.Content = "Translate";
            resultTextBlock.Text = "Translation";
            loadButton.Visibility = Visibility.Visible;
            headerTextBlock.Visibility = Visibility.Visible;
            headerTextBox.Visibility = Visibility.Visible;
            resultTextBlock.Visibility = Visibility.Visible;
            resultTextBox.Visibility = Visibility.Visible;
            createButton.Visibility = Visibility.Visible;
        }

        private void DefRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            headerTextBlock.Text = "Term";
            loadButton.Content = "Get definition from Wikipedia";
            resultTextBlock.Text = "Definition";
            loadButton.Visibility = Visibility.Visible;
            headerTextBlock.Visibility = Visibility.Visible;
            headerTextBox.Visibility = Visibility.Visible;
            resultTextBlock.Visibility = Visibility.Visible;
            resultTextBox.Visibility = Visibility.Visible;
            createButton.Visibility = Visibility.Visible;
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            if (resultTextBox.Text.Length != 0 && isEdited)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to edit the content?\nAll your edits won't be saved.", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                    return;
            }
            // Some function... 
            isEdited = false;
        }

        private void resultTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            isEdited = true;
        }
    }
}