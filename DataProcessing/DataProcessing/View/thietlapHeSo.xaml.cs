using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Threading.Tasks;

namespace DataProcessing
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class thietlapHeSo : Page
    {
        public static string startdatetime = "", enddatetime = "";
        public int n = 0;
        Controller.ExcelController excelcontroller = new Controller.ExcelController();
        Controller.AlgorithmController tlhscontroller = new Controller.AlgorithmController();
        Controller.OutputController outcontroller = new Controller.OutputController();
        //public bool check = false;

        public thietlapHeSo()
        {
           InitializeComponent();        
        }
        Boolean group2 = false, group3 = false, group4 = false, group5 = false;
        /// <summary>
        /// Mở đường dẫn đến file xls, xlsx và điền đường dẫn vào textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void browseFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.DefaultExt = ".xls";

            openfile.Filter = "(.xls)|*.xls|(.xlsx)|*.xlsx";
            //openfile.ShowDialog();

            var browsefile = openfile.ShowDialog();

            if (browsefile == true)
                txtFilePath.Text = openfile.FileName;
        }
        

        /// <summary>
        /// Bắt đầu tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void startSearch(object sender, RoutedEventArgs e)
        {
           

            // string mamaunguoidungnhap = "E";
            startdatetime = startd.SelectedDate == null ? "" : startd.SelectedDate.Value.ToString("M/dd/yyyy");
            enddatetime = endd.SelectedDate == null ? "" : endd.SelectedDate.Value.ToString("M/dd/yyyy");
            excelcontroller.readExcel(txtFilePath.Text);
            if (txtFilePath.Text.Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn đường dẫn!");
            }
            else if (startdatetime.Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu");
            }
            else if (enddatetime.Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc");
            }
            else if (!group2 && !group3 && !group4 && !group5)
            {
                MessageBox.Show("Bạn chưa chọn nhóm màu");
            }
            else
            {
                FindingStatus find = new FindingStatus();
                this.NavigationService.Navigate(find);
               
                if (group2)
                {
                    int timestart = Environment.TickCount;
                    await Task.Run(new Action(tlhscontroller.processGroup));
                    MessageBox.Show("Mau 2 het: " + ((double)(Environment.TickCount - timestart) / 1000).ToString() + "s");
                    outcontroller.sortOutPut(2);
                }
                else if (group3)
                {
                    int timestart = Environment.TickCount;
                    await Task.Run(new Action(tlhscontroller.processGroup));                  
                    MessageBox.Show("Mau 3 het: " + ((double)(Environment.TickCount - timestart) / 1000).ToString() + "s");
                    int timestart1 = Environment.TickCount;
                    outcontroller.sortOutPut(3);
                    MessageBox.Show("Mau 3 sx: " + ((double)(Environment.TickCount - timestart1) / 1000).ToString() + "s");
                }
                else if (group4)
                {
                    int timestart = Environment.TickCount;
                    await Task.Run(new Action(tlhscontroller.processGroup));
                    MessageBox.Show("Mau 4 het: " + ((double)(Environment.TickCount - timestart) / 1000).ToString() + "s");
                    outcontroller.sortOutPut(4);
                }
                else if (group5)
                {
                    int timestart = Environment.TickCount;
                    await Task.Run(new Action(tlhscontroller.processGroup));
                    MessageBox.Show("Mau 5 het: " + ((double)(Environment.TickCount - timestart) / 1000).ToString() + "s");
                    outcontroller.sortOutPut(5);
                }
                else
                {
                    MessageBox.Show("Lỗi");
                }

            }
            //FindingStatus fds = new FindingStatus();
            //this.NavigationService.Navigate(fds);
        }


        private void RadioButton2_Checked(object sender, RoutedEventArgs e)
        {
            tlhscontroller.readN(2);
            group2 = true;
        }

        private void RadioButton2_Unchecked(object sender, RoutedEventArgs e)
        {
            group2 = false;
        }

        private void RadioButton3_Checked(object sender, RoutedEventArgs e)
        {
            tlhscontroller.readN(3);
            group3 = true;
        }

        private void RadioButton3_Unchecked(object sender, RoutedEventArgs e)
        {
            group3 = false;
        }

        private void RadioButton4_Checked(object sender, RoutedEventArgs e)
        {
            tlhscontroller.readN(4);
            group4 = true;
        }

        private void RadioButton4_Unchecked(object sender, RoutedEventArgs e)
        {
            group4 = false;
        }

        private void RadioButton5_Checked(object sender, RoutedEventArgs e)
        {
            tlhscontroller.readN(5);
            group5 = true;
        }

        private void RadioButton5_Unchecked(object sender, RoutedEventArgs e)
        {
            group5 = false;
        }

        private void txtFilePath_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

      

    }
}