using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;

namespace DataProcessing
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class thietlapHeSo : Page
    {

        public thietlapHeSo()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Mở đường dẫn đến file xls, xlsx và điền đường dẫn vào textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browseFile(object sender, RoutedEventArgs e)
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
        private void startSearch(object sender, RoutedEventArgs e)
        {

            string startdatetime = startd.SelectedDate == null ? "" : startd.SelectedDate.Value.ToString("dd/M/yyyy");
            string enddatetime = endd.SelectedDate == null ? "" : endd.SelectedDate.Value.ToString("dd/M/yyyy");
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
            else
            {
                int ngaybatdau = 0, ngayketthuc = 0;
                Excel.Application excel;
                excel = new Excel.Application();
                Excel.Workbook WB = excel.Workbooks.Open(txtFilePath.Text);
                WB = excel.ActiveWorkbook;
                Excel.Worksheet WS;
                WS = WB.ActiveSheet;
                int start = Environment.TickCount;

                //Khoanh vùng ngày bắt đầu và kết thúc
                for (int row = 2; row <= WS.UsedRange.Rows.Count; row++)
                {

                    string cell = (WS.Cells[1][row] as Excel.Range).Value == null ? "" : (WS.Cells[1][row] as Excel.Range).Value.ToString("dd/M/yyyy");
                    if (cell == startdatetime)
                        ngaybatdau = row;

                    else if (cell == enddatetime)
                    {
                        ngayketthuc = row;
                        break;
                    }

                }
                MessageBox.Show("Ngày bắt đầu:"+" " + ngaybatdau + Environment.NewLine + "Kết thúc:" + " " + ngayketthuc);

                //Tính tổng tất cả các cột theo thời gian đã định
                for (int i = 2; i <= WS.UsedRange.Columns.Count; i++)
                {

                    int temp = 0;
                    Excel.Range b = WS.get_Range((Excel.Range)WS.Cells[i][ngaybatdau], (Excel.Range)WS.Cells[i][ngayketthuc]);
                    object arr = b.Value;
                    foreach (object s in (Array)arr)
                    {
                        string tmp = (string)s;
                        if (tmp != "1")
                            continue;
                        else
                            temp += int.Parse(tmp);
                    }
                }
                MessageBox.Show("Đọc hết: " + ((double)(Environment.TickCount - start) / 1000).ToString() + "ms");
            }
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Handle(sender as CheckBox);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Handle(sender as CheckBox);
        }

        void Handle(CheckBox checkBox)
        {
            // Use IsChecked.
            bool flag = checkBox.IsChecked.Value;

            // Assign Window Title.
            this.Title = "IsChecked = " + flag.ToString();
            MessageBox.Show(Title);
        }

    }
}