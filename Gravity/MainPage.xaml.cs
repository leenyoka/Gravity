using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Text;

namespace Gravity
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Linda Nyoka");
            builder.AppendLine("");
            builder.AppendLine("Nelson Mandela Metropolitan University Student");
            builder.AppendLine("");
            builder.AppendLine("Leenyoka@gmail.com");

            MessageBox.Show(builder.ToString(), "About", MessageBoxButton.OK);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Goal");
            builder.AppendLine("The goal of the game is to prevent the ball from hitting " +
                "the ground. To stop the ball from hitting the ground you need to slide " +
                "the blocker at the bottom to the block that the ball was going to fall.");
            builder.AppendLine("Levels");
            builder.AppendLine("Once the ball eats enough from the food at the top " +
                "a new level will be loaded. Each level the ball is moving faster");

            MessageBox.Show(builder.ToString(), "Instructions", MessageBoxButton.OK);

        }
    }
}