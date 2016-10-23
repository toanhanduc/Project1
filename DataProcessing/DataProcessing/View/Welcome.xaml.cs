using System;
using System.Windows;
using System.Windows.Controls;


namespace DataProcessing
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Page
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void gotothietlapHeso(object sender, RoutedEventArgs e)
        {
            thietlapHeSo tlhs = new thietlapHeSo();
            this.NavigationService.Navigate(tlhs);
        }

        private void myGif_MediaEnded(object sender, RoutedEventArgs e)
        {
            myGif.Position = new TimeSpan(0, 0, 1);
            myGif.Play();
        }
    }
}
