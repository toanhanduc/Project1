
using System;
using System.Windows.Controls;
using System.Windows.Forms;
using DataProcessing.Controller;
using System.Numerics;

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
        BigInteger sp = 1;
        BigInteger totalcolor = 0;
        BigInteger es_gio = 0, es_phut = 0, es_giay = 0;
        public FindingStatus()
        {

            InitializeComponent();
            start.Text = thietlapHeSo.startdatetime;
            end.Text = thietlapHeSo.enddatetime;
            colorgroup.Text = thietlapHeSo.n.ToString();
            totalcolor =  MiddlewareController.estimateTime(middle.getExcelCol(), thietlapHeSo.n);
            Console.WriteLine(totalcolor);
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
                if (giay < 1)
                {
                    processtime.Text = "0h 0m 0s";
                }
                estimate.Text =  "0h 0m 0s";
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

                sp = speed1;
                totalcolor -= sp;
                es_giay = totalcolor / sp;
                es_gio = es_giay / 3600;
                es_giay %= 3600;
                es_phut = es_giay / 60;
                es_giay %= 60;
                estimate.Text = es_gio.ToString() + "h " + es_phut.ToString() + "m " + es_giay.ToString() + "s";
               // estimate.Text = (totalcolor).ToString();

                foundedcolor.Text = middle.getFoundedColorValue().ToString();
            }

        }
    }
}
