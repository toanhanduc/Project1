
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
        int speed1 = 0, speed2 = 0;
        int totalcolor = 0;
        int es_gio = 0, es_phut = 0, es_giay = 0;
        public FindingStatus()
        {

            InitializeComponent();
            start.Text = thietlapHeSo.startdatetime;
            end.Text = thietlapHeSo.enddatetime;
            colorgroup.Text = thietlapHeSo.n.ToString();
            totalcolor = middle.estimateTime(middle.getExcelCol(),thietlapHeSo.n);
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
                processSpeed.Text = "0";
                MessageBox.Show("Tìm kiếm kết thúc");
            }
            else
            {
                if ((giay + 1) == 60)
                {
                    ++phut;
                    giay = -1;
                }
                if ((phut + 1) == 60)
                {
                    ++gio;
                    phut = -1;
                }
                processtime.Text = gio.ToString() + "h " + phut.ToString() + "m " + (++giay).ToString() + "s";


                speed1 = middle.getFoundedColorValue() - speed2;
                speed2 += speed1;
                processSpeed.Text = speed1.ToString() + " màu/s";

                totalcolor -= speed1;
                es_giay = totalcolor / speed1;
                es_gio = es_giay / 3600;
                es_giay %= 3600;
                es_phut = es_giay / 60;
                es_giay %= 60;
                estimate.Text = es_gio.ToString() + "h " + es_phut.ToString() + "m " + es_giay.ToString() + "s";

                foundedcolor.Text = middle.getFoundedColorValue().ToString();
            }

        }
    }
}
