
using System;
using System.Windows.Controls;
using System.Windows.Forms;
using DataProcessing.Controller;

namespace DataProcessing
{
    /// <summary>
    /// Interaction logic for FindingStatus.xaml
    /// </summary>
    public partial class FindingStatus : Page
    {
        MiddlewareController middle = new MiddlewareController();
        private Timer timer1 = new Timer();
        int gio = 0, phut = 0, giay = 0;
        public FindingStatus()
        {

            InitializeComponent();
            start.Text = thietlapHeSo.startdatetime;
            end.Text = thietlapHeSo.enddatetime;
            colorgroup.Text = thietlapHeSo.n.ToString();
            if (thietlapHeSo.limit.ToString() == "")
            {
                limitvalue.Text = "0";
            }
            else
                limitvalue.Text = thietlapHeSo.limit.ToString();
           
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000; // in miliseconds
            timer1.Start();
        }

        
        
    

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (thietlapHeSo.checkstop)
            {
                timer1.Stop();
                foundedcolor.Text = middle.getFoundedColorValue().ToString();
                MessageBox.Show("Tìm kiếm kết thúc");
            }
            else
            {
                if ((giay + 1) == 60)
                {
                    ++phut;
                    giay = -1;
                }   
                if ((phut+1) == 60)
                {
                    ++gio;
                    phut = -1;
                }
                processtime.Text = gio.ToString() + "h " + phut.ToString() + "m " + (++giay).ToString() + "s";
                foundedcolor.Text = middle.getFoundedColorValue().ToString();
            }

        }
    }
}
