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

namespace DataProcessing
{
    /// <summary>
    /// Interaction logic for FindingStatus.xaml
    /// </summary>
    public partial class FindingStatus : Page
    {

        public FindingStatus()
        {
            InitializeComponent();
            this.Loaded += FindingStatus_Loaded;

        }

        private void FindingStatus_Loaded(object sender, RoutedEventArgs e)
        {

            this.NavigationService.LoadCompleted += LoadCompletedEventHandler;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }

        private void LoadCompletedEventHandler(object sender, NavigationEventArgs e)
        {
            var t = e.ExtraData;
        }
    }
}
