using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using System.Threading;

namespace DataProcessing
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class thietlapHeSo : Page
    {
        public static string startdatetime = "", enddatetime = "";
        public static int n = 0;
        public static int ncolor = 0;
        Controller.ExcelController excelcontroller = new Controller.ExcelController();
        Controller.AlgorithmController tlhscontroller = new Controller.AlgorithmController();
        Controller.OutputController outcontroller = new Controller.OutputController();
        //public bool check = false;

        public thietlapHeSo()
        {
            InitializeComponent();
        }
        Boolean group2 = false, group3 = false, group4 = false, group5 = false, findmax = true;
        Boolean colorgroup0 = false, colorgroup1 = false, colorgroup2 = false, colorgroup3 = false, colorgroup4 = false, colorgroup5 = false;

        public void setProcess()
        {
            int i = 1;

            while (i < 107)
            {
                i++;

            }
        }


        /// <summary>
        /// Mở đường dẫn đến file xls, xlsx và điền đường dẫn vào textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void browseFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.DefaultExt = ".xls";

            openfile.Filter = "(.xls)|*.xls|(.xlsx)|*.xlsx";
            //openfile.ShowDialog();

            var browsefile = openfile.ShowDialog();

            if (browsefile == true)
            {
                txtFilePath.Text = openfile.FileName;
                startdatetime = startd.SelectedDate == null ? "" : startd.SelectedDate.Value.ToString("M/dd/yyyy");
                enddatetime = endd.SelectedDate == null ? "" : endd.SelectedDate.Value.ToString("M/dd/yyyy");
                await Task.Run(() => excelcontroller.readExcel(openfile.FileName));

                combo1.ItemsSource = excelcontroller.fillColorCombobox();
                combo2.ItemsSource = excelcontroller.fillColorCombobox();
                combo3.ItemsSource = excelcontroller.fillColorCombobox();
                combo4.ItemsSource = excelcontroller.fillColorCombobox();
                combo5.ItemsSource = excelcontroller.fillColorCombobox();

            }


        }


        /// <summary>
        /// Bắt đầu tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void startSearch(object sender, RoutedEventArgs e)
        {
            string color1 = combo1.SelectedValue.ToString();
            string color2 = combo2.SelectedValue.ToString();
            //string color3 = combo3.SelectedValue.ToString();
            //string color4 = combo4.SelectedValue.ToString();
            //string color5 = combo5.SelectedValue.ToString();
            // string mamaunguoidungnhap = "E";

            MessageBox.Show(combo1.SelectedValue.ToString());
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

                //tìm lớn nhất
                if (findmax)
                {

                    string limit = inputvalue.Text;
                    if (limit == "")
                    {
                        tlhscontroller.readLimit(0);
                    }
                    else
                    {
                        int limitvalue = Int32.Parse(limit);
                        tlhscontroller.readLimit(limitvalue);
                    }

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
                        outcontroller.sortOutPut(3);
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
                }
                else
                {
                    if (group2)
                    {
                        int timestart = Environment.TickCount;
             
                        await Task.Run(() => tlhscontroller.processGroupAll2(ncolor, color1 , color2));
                        MessageBox.Show("Mau 2 het: " + ((double)(Environment.TickCount - timestart) / 1000).ToString() + "s");
                    }
                    else if (group3)
                    {
                        int timestart = Environment.TickCount;
                        await Task.Run(() => tlhscontroller.processGroupAll3(ncolor));
                        MessageBox.Show("Mau 3 het: " + ((double)(Environment.TickCount - timestart) / 1000).ToString() + "s");
                    }
                    else if (group4)
                    {
                        int timestart = Environment.TickCount;
                        await Task.Run(() => tlhscontroller.processGroupAll4(ncolor));
                        MessageBox.Show("Mau 4 het: " + ((double)(Environment.TickCount - timestart) / 1000).ToString() + "s");
                    }
                    else if (group5)
                    {
                        int timestart = Environment.TickCount;
                        await Task.Run(() => tlhscontroller.processGroupAll5(ncolor));
                        MessageBox.Show("Mau 5 het: " + ((double)(Environment.TickCount - timestart) / 1000).ToString() + "s");
                    }
                    else
                    {
                        MessageBox.Show("Lỗi");
                    }
                }
            }
        }

        // Radio button số lượng mã màu

        private void RadioButton2_Checked(object sender, RoutedEventArgs e)
        {
            textcolornumber.Visibility = Visibility.Visible;
            colornumber0.Visibility = Visibility.Visible;
            colornumber0.IsChecked = true;
            colornumber1.Visibility = Visibility.Visible;
            colornumber2.Visibility = Visibility.Visible;
            colornumber3.Visibility = Visibility.Hidden;
            colornumber4.Visibility = Visibility.Hidden;
            colornumber5.Visibility = Visibility.Hidden;
            n = 2;
            tlhscontroller.readN(2);
            group2 = true;
        }

        private void RadioButton2_Unchecked(object sender, RoutedEventArgs e)
        {
            group2 = false;
        }

        private void RadioButton3_Checked(object sender, RoutedEventArgs e)
        {
            textcolornumber.Visibility = Visibility.Visible;
            colornumber0.Visibility = Visibility.Visible;
            colornumber0.IsChecked = true;
            colornumber1.Visibility = Visibility.Visible;
            colornumber2.Visibility = Visibility.Visible;
            colornumber3.Visibility = Visibility.Visible;
            colornumber4.Visibility = Visibility.Hidden;
            colornumber5.Visibility = Visibility.Hidden;
            n = 3;
            tlhscontroller.readN(3);
            group3 = true;
        }

        private void RadioButton3_Unchecked(object sender, RoutedEventArgs e)
        {
            group3 = false;
        }

        private void RadioButton4_Checked(object sender, RoutedEventArgs e)
        {
            textcolornumber.Visibility = Visibility.Visible;
            colornumber0.Visibility = Visibility.Visible;
            colornumber0.IsChecked = true;
            colornumber1.Visibility = Visibility.Visible;
            colornumber2.Visibility = Visibility.Visible;
            colornumber3.Visibility = Visibility.Visible;
            colornumber4.Visibility = Visibility.Visible;
            colornumber5.Visibility = Visibility.Hidden;
            n = 4;
            tlhscontroller.readN(4);
            group4 = true;
        }



        private void RadioButton4_Unchecked(object sender, RoutedEventArgs e)
        {
            group4 = false;
        }

        private void RadioButton5_Checked(object sender, RoutedEventArgs e)
        {
            textcolornumber.Visibility = Visibility.Visible;
            colornumber0.Visibility = Visibility.Visible;
            colornumber0.IsChecked = true;
            colornumber1.Visibility = Visibility.Visible;
            colornumber2.Visibility = Visibility.Visible;
            colornumber3.Visibility = Visibility.Visible;
            colornumber4.Visibility = Visibility.Visible;
            colornumber5.Visibility = Visibility.Visible;
            n = 5;
            tlhscontroller.readN(5);
            group5 = true;
        }

        private void RadioButton5_Unchecked(object sender, RoutedEventArgs e)
        {
            group5 = false;
        }

        private void inputvalue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            {
                if (!char.IsDigit(e.Text, e.Text.Length - 1))
                {
                    e.Handled = true;
                    MessageBox.Show("I only accept numbers, sorry. :(", "This textbox says...");
                }
            }
        }

        private void txtFilePath_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        //radiobutton tìm max hoặc tất cả
        private void RadioButtonTop_Checked(object sender, RoutedEventArgs e)
        {
            findmax = true;
        }

        private void RadioButtonTop_Unchecked(object sender, RoutedEventArgs e)
        {
            findmax = false;
        }

        private void RadioButtonAll_Checked(object sender, RoutedEventArgs e)
        {
            findmax = false;
        }

        private void RadioButtonAll_Unchecked(object sender, RoutedEventArgs e)
        {
            findmax = true;
        }

        // Radiobutton số lượng mã màu người dùng nhập
        private void ColorButton0_Checked(object sender, RoutedEventArgs e)
        {
            ncolor = 0;
            colorgroup0 = true;
            combo1.Visibility = Visibility.Hidden;
            combo2.Visibility = Visibility.Hidden;
            combo3.Visibility = Visibility.Hidden;
            combo4.Visibility = Visibility.Hidden;
            combo5.Visibility = Visibility.Hidden;
        }

        private void ColorButton0_Unchecked(object sender, RoutedEventArgs e)
        {
            colorgroup0 = false;
        }

        private void ColorButton1_Checked(object sender, RoutedEventArgs e)
        {
            ncolor = 1;
            colorgroup1 = true;
            combo1.Visibility = Visibility.Visible;
            combo2.Visibility = Visibility.Hidden;
            combo3.Visibility = Visibility.Hidden;
            combo4.Visibility = Visibility.Hidden;
            combo5.Visibility = Visibility.Hidden;
        }

        private void ColorButton1_Unchecked(object sender, RoutedEventArgs e)
        {
            colorgroup1 = false;
        }

        private void ColorButton2_Checked(object sender, RoutedEventArgs e)
        {
            ncolor = 2;
            colorgroup2 = true;
            combo1.Visibility = Visibility.Visible;
            combo2.Visibility = Visibility.Visible;
            combo3.Visibility = Visibility.Hidden;
            combo4.Visibility = Visibility.Hidden;
            combo5.Visibility = Visibility.Hidden;
        }

        private void ColorButton2_Unchecked(object sender, RoutedEventArgs e)
        {
            colorgroup2 = false;
        }

        private void ColorButton3_Checked(object sender, RoutedEventArgs e)
        {
            ncolor = 3;
            colorgroup3 = true;
            combo1.Visibility = Visibility.Visible;
            combo2.Visibility = Visibility.Visible;
            combo3.Visibility = Visibility.Visible;
            combo4.Visibility = Visibility.Hidden;
            combo5.Visibility = Visibility.Hidden;
        }

        private void ColorButton3_Unchecked(object sender, RoutedEventArgs e)
        {
            colorgroup3 = false;
        }

        private void ColorButton4_Checked(object sender, RoutedEventArgs e)
        {
            ncolor = 4;
            colorgroup4 = true;
            combo1.Visibility = Visibility.Visible;
            combo2.Visibility = Visibility.Visible;
            combo3.Visibility = Visibility.Visible;
            combo4.Visibility = Visibility.Visible;
            combo5.Visibility = Visibility.Hidden;
        }

        private void ColorButton4_Unchecked(object sender, RoutedEventArgs e)
        {
            colorgroup4 = false;
        }

        private void ColorButton5_Checked(object sender, RoutedEventArgs e)
        {
            ncolor = 5;
            colorgroup5 = true;
            combo1.Visibility = Visibility.Visible;
            combo2.Visibility = Visibility.Visible;
            combo3.Visibility = Visibility.Visible;
            combo4.Visibility = Visibility.Visible;
            combo5.Visibility = Visibility.Visible;
        }

        private void ColorButton5_Unchecked(object sender, RoutedEventArgs e)
        {
            colorgroup5 = false;
        }

    }
}