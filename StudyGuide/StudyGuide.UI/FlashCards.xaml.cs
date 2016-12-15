﻿using System;
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
using StudyGuide.Logic;

namespace StudyGuide.UI
{
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
            try
            {
                if ((bool)langRadioButton.IsChecked)
                    resultTextBox.Text = NetRepository.Translate(headerTextBox.Text);
                else
                    resultTextBox.Text = NetRepository.GetDefinition(headerTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unfortunately, a mistake happened while loading the needed information:\n" + ex.Message, "Error");
            }
            isEdited = false;
        }

        private void resultTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            isEdited = true;
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            // some function for adding current card to context
            MessageBoxResult result = MessageBox.Show("The card was successfully created!\nDo you want to creat one more?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                this.Close();
            else
            {
                headerTextBlock.Visibility = Visibility.Hidden;
                headerTextBox.Visibility = Visibility.Hidden;
                headerTextBox.Text = "";
                resultTextBlock.Visibility = Visibility.Hidden;
                resultTextBox.Visibility = Visibility.Hidden;
                resultTextBox.Text = "";
                loadButton.Visibility = Visibility.Hidden;
                createButton.Visibility = Visibility.Hidden;
            }
        }
    }
}