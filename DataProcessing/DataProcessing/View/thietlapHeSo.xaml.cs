using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace DataProcessing
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class thietlapHeSo : Page
    {
        public static string startdatetime = "", enddatetime = "";
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
        public void startSearch(object sender, RoutedEventArgs e)
        {
            Controller.ExcelController excelcontroller = new Controller.ExcelController();
            Controller.AlgorithmController tlhscontroller = new Controller.AlgorithmController();

            // string mamaunguoidungnhap = "E";
            startdatetime = startd.SelectedDate == null ? "" : startd.SelectedDate.Value.ToString("dd/M/yyyy");
            enddatetime = endd.SelectedDate == null ? "" : endd.SelectedDate.Value.ToString("dd/M/yyyy");
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
                    tlhscontroller.processGroup2();
                    MessageBox.Show("Mau 2 het: " + ((double)(Environment.TickCount - timestart) / 1000).ToString() + "s");
                }
                else if (group3)
                {
                    int timestart = Environment.TickCount;
                    tlhscontroller.processGroup3();
                    MessageBox.Show("Mau 3 het: " + ((double)(Environment.TickCount - timestart) / 1000).ToString() + "s");
                }
                else if (group4)
                {
                    int timestart = Environment.TickCount;
                    tlhscontroller.processGroup4();
                    MessageBox.Show("Mau 4 het: " + ((double)(Environment.TickCount - timestart) / 1000).ToString() + "s");
                }
                else if (group5)
                {
                    int timestart = Environment.TickCount;
                    tlhscontroller.processGroup5();
                    MessageBox.Show("Mau 5 het: " + ((double)(Environment.TickCount - timestart) / 1000).ToString() + "s");
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
            group2 = true;
        }

        private void RadioButton2_Unchecked(object sender, RoutedEventArgs e)
        {
            group2 = false;
        }

        private void RadioButton3_Checked(object sender, RoutedEventArgs e)
        {
            group3 = true;
        }

        private void RadioButton3_Unchecked(object sender, RoutedEventArgs e)
        {
            group3 = false;
        }

        private void RadioButton4_Checked(object sender, RoutedEventArgs e)
        {
            group4 = true;
        }

        private void RadioButton4_Unchecked(object sender, RoutedEventArgs e)
        {
            group4 = false;
        }

        private void RadioButton5_Checked(object sender, RoutedEventArgs e)
        {
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