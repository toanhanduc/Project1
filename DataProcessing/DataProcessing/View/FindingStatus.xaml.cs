
using System.Windows.Controls;


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
            start.Text = thietlapHeSo.startdatetime;
            end.Text = thietlapHeSo.enddatetime;

        }
    }
}
