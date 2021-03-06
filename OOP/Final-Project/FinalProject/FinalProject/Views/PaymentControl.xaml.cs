﻿using System.Windows;
using System.Windows.Controls;

namespace FinalProject.Views
{
    /// <summary>
    /// Interaction logic for PaymetControl.xaml
    /// </summary>
    public partial class PaymetControl : UserControl
    {
        public bool Bitcoin { get; set; } = false;
        public PaymetControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if(this.bitcoinInput != null && this.cardPanel != null)
            {
                this.bitcoinInput.Visibility = Visibility.Hidden;
                this.bitcoinInput.Margin = new Thickness(0);

                this.cardPanel.Visibility = Visibility.Hidden;
                this.cardPanel.Margin = new Thickness(0);
            }
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            this.bitcoinInput.Visibility = Visibility.Visible;
            this.bitcoinInput.Margin = new Thickness(0, 5, 0, 45);

            this.cardPanel.Visibility = Visibility.Hidden;
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            this.bitcoinInput.Visibility = Visibility.Hidden;
            this.bitcoinInput.Margin = new Thickness(0);

            this.cardPanel.Visibility = Visibility.Visible;
        }
    }
}
