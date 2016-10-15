using System;
using System.Collections.Generic;
using System.Linq;
using DataProcessing.Model;

namespace DataProcessing.Controller
{
    public class AlgorithmController
    {
       
        
        /// <summary>
        /// Hàm xử lý nhóm 2 màu
        /// </summary>
        public void processGroup2()
        {
            thietlaphesoModel model = new thietlaphesoModel();
            string print = "";
            int currentColumnValue; // giá trị cột làm mốc
            int biggestValue = 0; // giá trị lớn nhất khi gộp 2 cột
            int biggestCosts = 0; // trọng số lớn nhất
            int[] value = model.getValue();
            int[][] zeroOne = model.getZeroOne();
            int[] index = model.getIndex();
            string[] color = model.getColor();

            for (int i = 0; i < model.getColCount() - 2; i++) // chọn từng cột mốc trong 9 colors ( do không chọn đến cột cuối cùng làm mốc )
            {

                List<int> checkList = new List<int>(); // list so sánh theo ngày không bán được
                currentColumnValue = value[i]; // giá trị cột làm mốc

                for (int j = 0; j < ExcelController.ngayketthuc - ExcelController.ngaybatdau + 1; j++) // duyệt từng ngày của màu
                {
                    if (zeroOne[i][j] == 0) // tìm ngày không bán được để so
                    {
                        checkList.Add(j);
                    }
                }

                if (!checkList.Any())
                {
                    Console.WriteLine("cot 1 da full 1 roi");
                    for (int j = i + 1; j < model.getColCount() - 1; j++)
                    {
                        //Console.WriteLine(color[i] + "-" + color[j]);
                        if (index[i] < index[j])
                        {
                            print += color[i] + "-" + color[j] + Environment.NewLine;
                        }
                        else
                        {
                            print += color[j] + "-" + color[i] + Environment.NewLine;
                        }
                        Console.WriteLine(index[i] + " " + index[j]);
                        Console.WriteLine(print);
                        if (value[j] > value[j + 1])
                        {
                            break;
                        }
                    }
                    if (value[i] > value[i + 1] || value[i + 1] > value[i + 2])
                    {
                        break;
                    }

                }
                else
                {

                    for (int j = i + 1; j < model.getColCount() - 1; j++) // duyệt các màu tiếp theo, có 10 màu
                    {
                        //                    if (value[q] < biggestValue - currentColumnValue) // giá trị cột hiện tại + giá trị cột mốc < giá trị sau khi ghép lớn nhất -> dừng lại
                        //                    {
                        //                            Console.WriteLine("Dung lai duoc roi");
                        //                            Console.WriteLine(q);

                        //            break;
                        //                    }
                        List<int> checkListTemp = new List<int>(checkList); // list so sánh theo ngày không bán được
                        int currentCosts = 0; // Trọng số cột đang xét

                        foreach (int temp in checkListTemp) // duyệt từng ngày của màu để đánh trọng số
                        {
                            if (zeroOne[j][temp] == 1)
                            {
                                currentCosts++;
                            }

                        }

                        if (currentCosts + currentColumnValue > biggestValue)
                        {
                            Console.WriteLine("> bigvalue roi");
                            biggestValue = currentCosts + currentColumnValue;
                            // Console.WriteLine("CLEAR");
                            // Console.WriteLine(color[i] + "-" + color[j]);
                            // Console.WriteLine(biggestValue);
                            print = "";
                            if (index[i] < index[j])
                            {
                                print += color[i] + "-" + color[j] + ": " + biggestValue + Environment.NewLine;
                            }
                            else
                            {
                                print += color[j] + "-" + color[i] + ": " + biggestValue + Environment.NewLine;
                            }
                            biggestCosts = currentCosts;
                            Console.WriteLine(print);
                        }
                        else if (currentCosts + currentColumnValue == biggestValue)
                        {
                            Console.WriteLine("giong nhau roi");
                            // Console.WriteLine(color[i] + "-" + color[j]);
                            //  Console.WriteLine(biggestValue);
                            if (index[i] < index[j])
                            {
                                print += color[i] + "-" + color[j] + ": " + biggestValue + Environment.NewLine;
                            }
                            else
                            {
                                print += color[j] + "-" + color[i] + ": " + biggestValue + Environment.NewLine;
                            }
                            Console.WriteLine(index[i] + " " + index[j]);

                        }

                    }
                }

                //if (abc.check == true)
                //{
                //    break;
                //}

            }
            using (System.IO.StreamWriter writetext = new System.IO.StreamWriter("write.txt"))
            {
                writetext.WriteLine(print);
            }
        }

        /// <summary>
        /// Hàm xử lý nhóm 3 màu
        /// </summary>
        public void processGroup3()
        {
            thietlaphesoModel model = new thietlaphesoModel();
            string print = "";
            int currentValue1; // giá trị ở vòng 1
            int currentValue2; // giá trị cột mốc thứ 2
            int biggestValue = 0;
            int[] value = model.getValue();
            int[][] zeroOne = model.getZeroOne();
            int[] index = model.getIndex();
            string[] color = model.getColor();

            for (int i = 0; i < model.getColCount() - 3; i++) // chọn từng cột làm mốc vòng 1 trong 8 màu ( để lại 2 màu đế ghép )
            {
                List<int> checkList1 = new List<int>(); // list so sánh theo ngày không bán được sau vòng 1
                currentValue1 = value[i];

                for (int j = 0; j < ExcelController.ngayketthuc - ExcelController.ngaybatdau + 1; j++) // duyệt từng ngày của màu
                {
                    if (zeroOne[i][j] == 0) // tìm ngày không bán được để so
                    {
                        checkList1.Add(j);
                    }
                }

                if (!checkList1.Any())
                {
                    Console.WriteLine("Cot dau tien da full 1");
                    if (biggestValue < currentValue1)
                    {
                        Console.WriteLine("CLEAR");
                        print = "";
                        biggestValue = currentValue1;
                        for (int j = i + 1; j < model.getColCount() - 2; j++)
                        {
                            for (int q = j + 1; q < model.getColCount() - 1; q++)
                            {
                                //Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q]);

                                String[] colorOut = new String[3];
                                int[] colorOutIndex = new int[3];

                                colorOut[0] = color[i];
                                colorOut[1] = color[j];
                                colorOut[2] = color[q];

                                colorOutIndex[0] = index[i];
                                colorOutIndex[1] = index[j];
                                colorOutIndex[2] = index[q];

                                for (int x = 0; x < 3; x++)
                                {
                                    for (int y = x + 1; y < 3; y++)
                                    {
                                        if (colorOutIndex[x] > colorOutIndex[y])
                                        {
                                            String temp;
                                            temp = colorOut[x];
                                            colorOut[x] = colorOut[y];
                                            colorOut[y] = temp;

                                            int tempInt;
                                            tempInt = colorOutIndex[x];
                                            colorOutIndex[x] = colorOutIndex[y];
                                            colorOutIndex[y] = tempInt;
                                        }
                                    }
                                }


                                print += colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + Environment.NewLine;
                                if (value[q] > value[q + 1])
                                {
                                    break;
                                }
                            }
                            if (value[j] > value[j + 1] || value[j + 1] > value[j + 2])
                            {
                                break;
                            }
                        }
                        if (value[i] > value[i + 1] || value[i + 1] > value[i + 2] || value[i + 2] > value[i + 3])
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (value[i] > value[i + 1] || value[i + 1] > value[i + 2])
                        {
                            break;
                        }
                        for (int j = i + 1; j < model.getColCount() - 2; j++)
                        {
                            for (int q = j + 1; q < model.getColCount() - 1; q++)
                            {
                                //Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q]);
                                String[] colorOut = new String[3];
                                int[] colorOutIndex = new int[3];

                                colorOut[0] = color[i];
                                colorOut[1] = color[j];
                                colorOut[2] = color[q];

                                colorOutIndex[0] = index[i];
                                colorOutIndex[1] = index[j];
                                colorOutIndex[2] = index[q];

                                for (int x = 0; x < 3; x++)
                                {
                                    for (int y = x + 1; y < 3; y++)
                                    {
                                        if (colorOutIndex[x] > colorOutIndex[y])
                                        {
                                            String temp;
                                            temp = colorOut[x];
                                            colorOut[x] = colorOut[y];
                                            colorOut[y] = temp;

                                            int tempInt;
                                            tempInt = colorOutIndex[x];
                                            colorOutIndex[x] = colorOutIndex[y];
                                            colorOutIndex[y] = tempInt;
                                        }
                                    }
                                }


                                print = colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + Environment.NewLine;
                                if (value[q] > value[q + 1])
                                {
                                    break;
                                }
                            }
                            if (value[j] > value[j + 1] || value[j + 1] > value[j + 2])
                            {
                                break;
                            }
                        }
                        if (value[i] > value[i + 1] || value[i + 1] > value[i + 2] || value[i + 2] > value[i + 3])
                        {
                            break;
                        }
                    }
                    continue;
                }
                else
                {
                    for (int j = i + 1; j < model.getColCount() - 2; j++) // chọn từng cột trong vòng 2
                    {
                        List<int> checkList2 = new List<int>(checkList1);

                        int currentCosts = 0; // trọng số cột hiện tại

                        foreach (int temp in checkList1)
                        {
                            if (zeroOne[j][temp] == 1)
                            {
                                currentCosts++;
                                checkList2.Remove(temp);
                            }
                        }
                        currentValue2 = currentValue1 + currentCosts;
                        //biggestValue = currentValue2;
                        if (!checkList2.Any()) // full 1
                        {
                            Console.WriteLine("2 cột đã Full 1 roi !");
                            if (biggestValue < currentValue2)
                            {
                                Console.WriteLine("CLEAR");
                                print = "";
                                biggestValue = currentValue2;

                                for (int q = j + 1; q < model.getColCount() - 1; q++)
                                {
                                    //  Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q]);
                                    String[] colorOut = new String[3];
                                    int[] colorOutIndex = new int[3];

                                    colorOut[0] = color[i];
                                    colorOut[1] = color[j];
                                    colorOut[2] = color[q];

                                    colorOutIndex[0] = index[i];
                                    colorOutIndex[1] = index[j];
                                    colorOutIndex[2] = index[q];

                                    for (int x = 0; x < 3; x++)
                                    {
                                        for (int y = x + 1; y < 3; y++)
                                        {
                                            if (colorOutIndex[x] > colorOutIndex[y])
                                            {
                                                String temp;
                                                temp = colorOut[x];
                                                colorOut[x] = colorOut[y];
                                                colorOut[y] = temp;

                                                int tempInt;
                                                tempInt = colorOutIndex[x];
                                                colorOutIndex[x] = colorOutIndex[y];
                                                colorOutIndex[y] = tempInt;
                                            }
                                        }
                                    }


                                    print = colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + Environment.NewLine;
                                    if (value[q] > value[q + 1])
                                    {
                                        break;
                                    }
                                }
                                if (value[j] > value[j + 1] || value[j + 1] > value[j + 2])
                                {
                                    break;
                                }

                            }
                            else
                            {
                                if (value[j] > value[j + 1])
                                {
                                    break;
                                }
                                for (int q = j + 1; q < model.getColCount() - 1; q++)
                                {
                                    // Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q]);
                                    String[] colorOut = new String[3];
                                    int[] colorOutIndex = new int[3];

                                    colorOut[0] = color[i];
                                    colorOut[1] = color[j];
                                    colorOut[2] = color[q];

                                    colorOutIndex[0] = index[i];
                                    colorOutIndex[1] = index[j];
                                    colorOutIndex[2] = index[q];

                                    for (int x = 0; x < 3; x++)
                                    {
                                        for (int y = x + 1; y < 3; y++)
                                        {
                                            if (colorOutIndex[x] > colorOutIndex[y])
                                            {
                                                String temp;
                                                temp = colorOut[x];
                                                colorOut[x] = colorOut[y];
                                                colorOut[y] = temp;

                                                int tempInt;
                                                tempInt = colorOutIndex[x];
                                                colorOutIndex[x] = colorOutIndex[y];
                                                colorOutIndex[y] = tempInt;
                                            }
                                        }
                                    }


                                    print = colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + Environment.NewLine;
                                    if (value[q] > value[q + 1])
                                    {
                                        break;
                                    }
                                }
                                if (value[j] > value[j + 1] || value[j + 1] > value[j + 2])
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            int compVar = 0; // bien so sanh
                            for (int q = j + 1; q < model.getColCount() - 1; q++)
                            {
                                if (value[q] + value[j] + value[i] < biggestValue)
                                {
                                    Console.WriteLine("Chon mau khac lam cot 2 di");
                                    break;
                                }

                                currentCosts = 0; // trọng số cột hiện tại

                                foreach (int temp in checkList2)
                                {
                                    if (zeroOne[q][temp] == 1)
                                    {
                                        currentCosts++;
                                    }
                                }


                                if (currentCosts + currentValue2 > biggestValue)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("> biggest value roi");
                                    biggestValue = currentValue2 + currentCosts;
                                    //Console.WriteLine(biggestValue);
                                    //Console.WriteLine("CLEAR");
                                    //Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q]);
                                    print = "";
                                    String[] colorOut = new String[3];
                                    int[] colorOutIndex = new int[3];

                                    colorOut[0] = color[i];
                                    colorOut[1] = color[j];
                                    colorOut[2] = color[q];

                                    colorOutIndex[0] = index[i];
                                    colorOutIndex[1] = index[j];
                                    colorOutIndex[2] = index[q];

                                    for (int x = 0; x < 3; x++)
                                    {
                                        for (int y = x + 1; y < 3; y++)
                                        {
                                            if (colorOutIndex[x] > colorOutIndex[y])
                                            {
                                                String temp;
                                                temp = colorOut[x];
                                                colorOut[x] = colorOut[y];
                                                colorOut[y] = temp;

                                                int tempInt;
                                                tempInt = colorOutIndex[x];
                                                colorOutIndex[x] = colorOutIndex[y];
                                                colorOutIndex[y] = tempInt;
                                            }
                                        }
                                    }


                                    print += colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + ": " + (currentValue2 + currentCosts) + Environment.NewLine;

                                    compVar = value[q];
                                }
                                else if (currentCosts + currentValue2 == biggestValue)
                                {
                                    Console.WriteLine("giong nhau roi");

                                    if (value[q] < compVar)
                                    {
                                        continue;
                                    }

                                    if (q < model.getColCount() - 2 && value[q] > value[q + 1])
                                    {
                                        compVar = value[q];
                                    }

                                    //Console.WriteLine(currentValue2 + currentCosts);
                                    //Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q]);
                                    String[] colorOut = new String[3];
                                    int[] colorOutIndex = new int[3];

                                    colorOut[0] = color[i];
                                    colorOut[1] = color[j];
                                    colorOut[2] = color[q];

                                    colorOutIndex[0] = index[i];
                                    colorOutIndex[1] = index[j];
                                    colorOutIndex[2] = index[q];

                                    for (int x = 0; x < 3; x++)
                                    {
                                        for (int y = x + 1; y < 3; y++)
                                        {
                                            if (colorOutIndex[x] > colorOutIndex[y])
                                            {
                                                String temp;
                                                temp = colorOut[x];
                                                colorOut[x] = colorOut[y];
                                                colorOut[y] = temp;

                                                int tempInt;
                                                tempInt = colorOutIndex[x];
                                                colorOutIndex[x] = colorOutIndex[y];
                                                colorOutIndex[y] = tempInt;
                                            }
                                        }
                                    }


                                    print += colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + ": " + (currentValue2 + currentCosts) + Environment.NewLine;
                                }

                            }
                        }
                    }
                }

                //if (abc.check == true)
                //{
                //    break;
                //}
            }
            using (System.IO.StreamWriter writetext = new System.IO.StreamWriter("write.txt"))
            {
                writetext.WriteLine(print);
            }

        }
        /// <summary>
        /// Hàm xử lý nhóm 4 màu
        /// </summary>
        public void processGroup4()
        {
            thietlaphesoModel model = new thietlaphesoModel();
            string print = "";
            int currentValue1; // giá trị ở vòng 1
            int currentValue2; // giá trị ở vòng 2
            int currentValue3; // giá trị ở vòng 3
            int biggestValue = 0;
            int[] value = model.getValue();
            int[][] zeroOne = model.getZeroOne();
            int[] index = model.getIndex();
            string[] color = model.getColor();

            for (int i = 0; i < model.getColCount() - 4; i++) // để lại 3 cột để ghép màu
            {

                List<int> checkList1 = new List<int>(); // list so sánh theo ngày không bán được sau vòng 1
                currentValue1 = value[i];

                for (int j = 0; j < ExcelController.ngayketthuc - ExcelController.ngaybatdau + 1; j++) // duyệt từng ngày của màu
                {
                    if (zeroOne[i][j] == 0) // tìm ngày không bán được để so
                    {
                        checkList1.Add(j);
                    }
                }

                if (!checkList1.Any()) // full 1
                {
                    Console.WriteLine("Cot dau tien da full 1");
                    if (biggestValue < currentValue1)
                    {
                        Console.WriteLine("CLEAR");
                        print = "";
                        biggestValue = currentValue1;
                        for (int j = i + 1; j < model.getColCount() - 3; j++)
                        {
                            for (int q = j + 1; q < model.getColCount() - 2; q++)
                            {
                                for (int k = q + 1; k < model.getColCount() - 1; k++)
                                {
                                    Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k]);
                                    //  print = color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k];
                                    if (value[k] > value[k + 1])
                                    {
                                        break;
                                    }
                                }
                                if (value[q] > value[q + 1] || value[q + 1] > value[q + 2])
                                {
                                    break;
                                }

                            }
                            if (value[j] > value[j + 1] || value[j + 1] > value[j + 2] || value[j + 2] > value[j + 3])
                            {
                                break;
                            }
                        }
                        if (value[i] > value[i + 1] || value[i + 1] > value[i + 2] || value[i + 2] > value[i + 3] || value[i + 3] > value[i + 4])
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (value[i] > value[i + 1] || value[i + 1] > value[i + 2] || value[i + 2] > value[i + 3])
                        {
                            break;
                        }
                        for (int j = i + 1; j < model.getColCount() - 3; j++)
                        {
                            for (int q = j + 1; q < model.getColCount() - 2; q++)
                            {
                                for (int k = q + 1; k < model.getColCount() - 1; k++)
                                {
                                    Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k]);

                                    if (value[k] > value[k + 1])
                                    {
                                        break;
                                    }
                                }
                                if (value[q] > value[q + 1] || value[q + 1] > value[q + 2])
                                {
                                    break;
                                }
                            }
                            if (value[j] > value[j + 1] || value[j + 1] > value[j + 2] || value[j + 2] > value[j + 3])
                            {
                                break;
                            }
                        }
                        if (value[i] > value[i + 1] || value[i + 1] > value[i + 2] || value[i + 2] > value[i + 3] || value[i + 3] > value[i + 4])
                        {
                            break;
                        }
                    }
                    continue;
                }
                else
                {
                    for (int j = i + 1; j < model.getColCount() - 3; j++) // chọn từng cột ở vòng 2, để lại 2 cột để ghép với 2 cột đã chọn
                    {
                        List<int> checkList2 = new List<int>(checkList1);

                        int currentCosts = 0; // trọng số cột hiện tại ở vòng 2

                        foreach (int temp in checkList1)
                        {
                            if (zeroOne[j][temp] == 1)
                            {
                                currentCosts++;
                                checkList2.Remove(temp);
                            }
                        }

                        currentValue2 = currentValue1 + currentCosts;

                        if (!checkList2.Any()) // full 1
                        {
                            Console.WriteLine("2 cột đã Full 1 roi !");
                            if (biggestValue < currentValue2)
                            {
                                Console.WriteLine("CLEAR");
                                print = "";
                                biggestValue = currentValue2;

                                for (int q = j + 1; q < model.getColCount() - 2; q++)
                                {
                                    for (int k = q + 1; k < model.getColCount() - 1; k++)
                                    {
                                        Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k]);

                                        if (value[k] > value[k + 1])
                                        {
                                            break;
                                        }
                                    }
                                    if (value[q] > value[q + 1] || value[q + 1] > value[q + 2])
                                    {
                                        break;
                                    }
                                }
                                if (value[j] > value[j + 1] || value[j + 1] > value[j + 2] || value[j + 2] > value[j + 3])
                                {
                                    break;
                                }
                            }
                            else
                            {
                                if (value[j] > value[j + 1] || value[j + 1] > value[j + 2])
                                {
                                    break;
                                }
                                for (int q = j + 1; q < model.getColCount() - 2; q++)
                                {
                                    for (int k = q + 1; k < model.getColCount() - 1; k++)
                                    {
                                        Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k]);

                                        if (value[k] > value[k + 1])
                                        {
                                            break;
                                        }
                                    }
                                    if (value[q] > value[q + 1] || value[q + 1] > value[q + 2])
                                    {
                                        break;
                                    }
                                }
                                if (value[j] > value[j + 1] || value[j + 1] > value[j + 2] || value[j + 2] > value[j + 3])
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int q = j + 1; q < model.getColCount() - 2; q++) // chọn từng cột ở vòng 3, để lại 1 cột để ghép màu
                            {
                                //THÊM ĐIỀU KIỆN DỪNG

                                List<int> checkList3 = new List<int>(checkList2);

                                currentCosts = 0;

                                foreach (int temp in checkList2)
                                {
                                    if (zeroOne[q][temp] == 1)
                                    {
                                        currentCosts++;
                                        checkList3.Remove(temp);
                                    }
                                }

                                currentValue3 = currentValue2 + currentCosts;

                                if (!checkList3.Any()) // full 1
                                {
                                    Console.WriteLine("3 cot da Full 1 roi !");
                                    if (biggestValue < currentValue3)
                                    {
                                        Console.WriteLine("CLEAR");
                                        print = "";
                                        biggestValue = currentValue3;

                                        for (int k = q + 1; k < model.getColCount() - 1; k++)
                                        {
                                            Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k]);
                                            if (value[k] > value[k + 1])
                                            {
                                                break;
                                            }
                                        }
                                        if (value[q] > value[q + 1] || value[q + 1] > value[q + 2])
                                        {
                                            break;
                                        }

                                    }
                                    else
                                    {
                                        if (value[q] > value[q + 1])
                                        {
                                            break;
                                        }
                                        for (int k = q + 1; k < model.getColCount() - 1; k++)
                                        {
                                            Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k]);
                                            if (value[k] > value[k + 1])
                                            {
                                                break;
                                            }
                                        }
                                        if (value[q] > value[q + 1] || value[q + 1] > value[q + 2])
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    int compVar = 0;
                                    for (int k = q + 1; k < model.getColCount() - 1; k++) // chọn màu ở vòng 4
                                    {
                                        //THÊM ĐIỀU KIỆN DỪNG
                                        if (value[k] + value[q] + value[j] + value[i] < biggestValue)
                                        {
                                            Console.WriteLine("Chon mau khac lam cot 3 di");
                                            break;
                                        }
                                        currentCosts = 0;

                                        foreach (int temp in checkList3)
                                        {
                                            if (zeroOne[k][temp] == 1)
                                            {
                                                currentCosts++;
                                            }
                                        }

                                        if (currentValue3 + currentCosts > biggestValue)
                                        {
                                            Console.WriteLine("> biggest value roi");
                                            Console.WriteLine("CLEAR");
                                            print = "";
                                            biggestValue = currentValue3 + currentCosts;
                                            //Console.WriteLine(biggestValue);
                                            //Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k]);

                                            String[] colorOut = new String[4];
                                            int[] colorOutIndex = new int[4];

                                            colorOut[0] = color[i];
                                            colorOut[1] = color[j];
                                            colorOut[2] = color[q];
                                            colorOut[3] = color[k];

                                            colorOutIndex[0] = index[i];
                                            colorOutIndex[1] = index[j];
                                            colorOutIndex[2] = index[q];
                                            colorOutIndex[3] = index[k];

                                            for (int x = 0; x < 4; x++)
                                            {
                                                for (int y = x + 1; y < 4; y++)
                                                {
                                                    if (colorOutIndex[x] > colorOutIndex[y])
                                                    {
                                                        String temp;
                                                        temp = colorOut[x];
                                                        colorOut[x] = colorOut[y];
                                                        colorOut[y] = temp;

                                                        int tempInt;
                                                        tempInt = colorOutIndex[x];
                                                        colorOutIndex[x] = colorOutIndex[y];
                                                        colorOutIndex[y] = tempInt;
                                                    }
                                                }
                                            }

                                            print += colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + "-" + colorOut[3] + ": " + biggestValue + Environment.NewLine;

                                            compVar = value[k];
                                        }
                                        else if (currentValue3 + currentCosts == biggestValue)
                                        {
                                            Console.WriteLine("giong nhau roi");

                                            if (value[k] < compVar)
                                            {
                                                continue;
                                            }

                                            if (k < model.getColCount() - 2 && value[k] > value[k + 1])
                                            {
                                                compVar = value[k];
                                            }

                                            //Console.WriteLine(biggestValue);
                                            //Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k]);
                                            String[] colorOut = new String[4];
                                            int[] colorOutIndex = new int[4];

                                            colorOut[0] = color[i];
                                            colorOut[1] = color[j];
                                            colorOut[2] = color[q];
                                            colorOut[3] = color[k];

                                            colorOutIndex[0] = index[i];
                                            colorOutIndex[1] = index[j];
                                            colorOutIndex[2] = index[q];
                                            colorOutIndex[3] = index[k];

                                            for (int x = 0; x < 4; x++)
                                            {
                                                for (int y = x + 1; y < 4; y++)
                                                {
                                                    if (colorOutIndex[x] > colorOutIndex[y])
                                                    {
                                                        String temp;
                                                        temp = colorOut[x];
                                                        colorOut[x] = colorOut[y];
                                                        colorOut[y] = temp;

                                                        int tempInt;
                                                        tempInt = colorOutIndex[x];
                                                        colorOutIndex[x] = colorOutIndex[y];
                                                        colorOutIndex[y] = tempInt;
                                                    }
                                                }
                                            }

                                            print += colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + "-" + colorOut[3] + ": " + biggestValue + Environment.NewLine;
                                        }

                                    }
                                }
                            }
                        }

                    }
                }

                //if (abc.check == true)
                //{
                //    break;
                //}
            }
            using (System.IO.StreamWriter writetext = new System.IO.StreamWriter("write.txt"))
            {
                writetext.WriteLine(print);
            }
        }
        /// <summary>
        /// Hàm xử lý nhóm 5 màu
        /// </summary>
        public void processGroup5()
        {
            thietlaphesoModel model = new thietlaphesoModel();
            string print = "";
            int currentValue1; // giá trị ở vòng 1
            int currentValue2; // giá trị ở vòng 2
            int currentValue3; // giá trị ở vòng 3
            int currentValue4; // giá trị ở vòng 4
            int biggestValue = 0;
            int[] value = model.getValue();
            int[][] zeroOne = model.getZeroOne();
            int[] index = model.getIndex();
            string[] color = model.getColor();

            for (int i = 0; i < model.getColCount() - 5; i++) // để lại 4 cột để ghép màu
            {
                // THÊM ĐIỀU KIỆN ĐỂ KẾT THÚC CHƯƠNG TRÌNH

                List<int> checkList1 = new List<int>(); // list so sánh theo ngày không bán được sau vòng 1
                currentValue1 = value[i];

                for (int j = 0; j < ExcelController.ngayketthuc - ExcelController.ngaybatdau + 1; j++) // duyệt từng ngày của màu
                {
                    if (zeroOne[i][j] == 0) // tìm ngày không bán được để so
                    {
                        checkList1.Add(j);
                    }
                }

                if (!checkList1.Any()) // full 1
                {
                    Console.WriteLine("cot dau tien da full 1");
                    if (biggestValue < currentValue1)
                    {
                        Console.WriteLine("CLEAR");
                        biggestValue = currentValue1;

                        for (int j = i + 1; j < model.getColCount() - 4; j++)
                        {
                            for (int q = j + 1; q < model.getColCount() - 3; q++)
                            {
                                for (int k = q + 1; k < model.getColCount() - 2; k++)
                                {
                                    for (int l = k + 1; l < model.getColCount() - 1; l++)
                                    {
                                        Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k] + "-" + color[l]);

                                        if (value[l] > value[l + 1])
                                        {
                                            break;
                                        }
                                    }
                                    if (value[k] > value[k + 1] || value[k + 1] > value[k + 2])
                                    {
                                        break;
                                    }
                                }
                                if (q < model.getColCount() - 3 && (value[q] > value[q + 1] || value[q + 1] > value[q + 2] || value[q + 2] > value[q + 3]))
                                {
                                    break;
                                }
                            }
                            if (value[j] > value[j + 1] || value[j + 1] > value[j + 2] || value[j + 2] > value[j + 3] || value[j + 3] > value[j + 4])
                            {
                                break;
                            }
                        }
                        if (value[i] > value[i + 1] || value[i + 1] > value[i + 2] || value[i + 2] > value[i + 3] || value[i + 3] > value[i + 4] || value[i + 4] > value[i + 5])
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (value[i] > value[i + 1] || value[i + 1] > value[i + 2] || value[i + 2] > value[i + 3] || value[i + 3] > value[i + 4])
                        {
                            break;
                        }

                        for (int j = i + 1; j < model.getColCount() - 4; j++)
                        {
                            for (int q = j + 1; q < model.getColCount() - 3; q++)
                            {
                                for (int k = q + 1; k < model.getColCount() - 2; k++)
                                {
                                    for (int l = k + 1; l < model.getColCount() - 1; l++)
                                    {
                                        Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k] + "-" + color[l]);

                                        if (value[l] > value[l + 1])
                                        {
                                            break;
                                        }
                                    }
                                    if (value[k] > value[k + 1] || value[k + 1] > value[k + 2])
                                    {
                                        break;
                                    }
                                }
                                if (value[q] > value[q + 1] || value[q + 1] > value[q + 2] || value[q + 2] > value[q + 3])
                                {
                                    break;
                                }
                            }
                            if (value[j] > value[j + 1] || value[j + 1] > value[j + 2] || value[j + 2] > value[j + 3] || value[j + 3] > value[j + 4])
                            {
                                break;
                            }
                        }
                        if (value[i] > value[i + 1] || value[i + 1] > value[i + 2] || value[i + 2] > value[i + 3] || value[i + 3] > value[i + 4] || value[i + 4] > value[i + 5])
                        {
                            break;
                        }
                    }
                    continue;
                }
                else
                {
                    for (int j = i + 1; j < model.getColCount() - 4; j++) // chọn từng cột ở vòng 2, để lại 3 cột để ghép với 2 cột đã chọn
                    {

                        // THÊM ĐIỀU KIỆN ĐỂ SANG MÀU KHÁC

                        List<int> checkList2 = new List<int>(checkList1);

                        int currentCosts = 0; // trọng số cột hiện tại ở vòng 2

                        foreach (int temp in checkList1)
                        {
                            if (zeroOne[j][temp] == 1)
                            {
                                currentCosts++;
                                checkList2.Remove(temp);
                            }
                        }

                        currentValue2 = currentValue1 + currentCosts;

                        if (!checkList2.Any()) // full 1
                        {
                            Console.WriteLine("2 cot da Full 1 roi !");

                            if (biggestValue < currentValue2)
                            {
                                Console.WriteLine("CLEAR");
                                biggestValue = currentValue2;

                                for (int q = j + 1; q < model.getColCount() - 3; q++)
                                {
                                    for (int k = q + 1; k < model.getColCount() - 2; k++)
                                    {
                                        for (int l = k + 1; l < model.getColCount() - 1; l++)
                                        {
                                            Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k] + "-" + color[l]);

                                            if (value[l] > value[l + 1])
                                            {
                                                break;
                                            }
                                        }
                                        if (value[k] > value[k + 1] || value[k + 1] > value[k + 2])
                                        {
                                            break;
                                        }
                                    }
                                    if (q < (model.getColCount() - 5) && (value[q] > value[q + 1] || value[q + 1] > value[q + 2] || value[q + 2] > value[q + 3]))
                                    {
                                        break;
                                    }
                                }
                                if (value[j] > value[j + 1] || value[j + 1] > value[j + 2] || value[j + 2] > value[j + 3] || value[j + 3] > value[j + 4])
                                {
                                    break;
                                }
                            }
                            else
                            {
                                if (value[j] > value[j + 1] || value[j + 1] > value[j + 2] || value[j + 2] > value[j + 3])
                                {
                                    break;
                                }

                                for (int q = j + 1; q < model.getColCount() - 3; q++)
                                {
                                    for (int k = q + 1; k < model.getColCount() - 2; k++)
                                    {
                                        for (int l = k + 1; l < model.getColCount() - 1; l++)
                                        {
                                            Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k] + "-" + color[l]);

                                            if (value[l] > value[l + 1])
                                            {
                                                break;
                                            }
                                        }
                                        if (value[k] > value[k + 1] || value[k + 1] > value[k + 2])
                                        {
                                            break;
                                        }
                                    }
                                    if (value[q] > value[q + 1] || value[q + 1] > value[q + 2] || value[q + 2] > value[q + 3])
                                    {
                                        break;
                                    }
                                }
                                if (value[j] > value[j + 1] || value[j + 1] > value[j + 2] || value[j + 2] > value[j + 3] || value[j + 3] > value[j + 4])
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int q = j + 1; q < model.getColCount() - 3; q++) // chọn từng cột ở vòng 3, để lại 2 cột để ghép màu
                            {
                                //THÊM ĐIỀU KIỆN DỪNG


                                List<int> checkList3 = new List<int>(checkList2);

                                currentCosts = 0;

                                foreach (int temp in checkList2)
                                {
                                    if (zeroOne[q][temp] == 1)
                                    {
                                        currentCosts++;
                                        checkList3.Remove(temp);
                                    }
                                }

                                currentValue3 = currentValue2 + currentCosts;

                                if (!checkList3.Any()) // full 1
                                {
                                    Console.WriteLine("3 cot đa Full 1 roi !");
                                    if (biggestValue < currentValue3)
                                    {
                                        Console.WriteLine("CLEAR");
                                        biggestValue = currentValue3;

                                        for (int k = q + 1; k < model.getColCount() - 2; k++)
                                        {
                                            for (int l = k + 1; l < model.getColCount() - 1; l++)
                                            {
                                                Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k] + "-" + color[l]);

                                                if (value[l] > value[l + 1])
                                                {
                                                    break;
                                                }
                                            }
                                            if (value[k] > value[k + 1] || value[k + 1] > value[k + 2])
                                            {
                                                break;
                                            }
                                        }
                                        if (value[q] > value[q + 1] || value[q + 1] > value[q + 2] || value[q + 2] > value[q + 3])
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (value[q] > value[q + 1] || value[q + 1] > value[q + 2])
                                        {
                                            break;
                                        }
                                        for (int k = q + 1; k < model.getColCount() - 2; k++)
                                        {
                                            for (int l = k + 1; l < model.getColCount() - 1; l++)
                                            {
                                                Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k] + "-" + color[l]);

                                                if (value[l] > value[l + 1])
                                                {
                                                    break;
                                                }
                                            }
                                            if (value[k] > value[k + 1] || value[k + 1] > value[k + 2])
                                            {
                                                break;
                                            }
                                        }
                                        if (value[q] > value[q + 1] || value[q + 1] > value[q + 2] || value[q + 2] > value[q + 3])
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int k = q + 1; k < model.getColCount() - 2; k++) // chọn màu ở vòng 4, để lại 1 màu để ghép
                                    {
                                        //THÊM ĐIỀU KIỆN DỪNG

                                        List<int> checkList4 = new List<int>(checkList3);

                                        currentCosts = 0;

                                        foreach (int temp in checkList3)
                                        {
                                            if (zeroOne[k][temp] == 1)
                                            {
                                                currentCosts++;
                                                checkList4.Remove(temp);
                                            }
                                        }

                                        currentValue4 = currentValue3 + currentCosts;

                                        if (!checkList4.Any()) // full 1
                                        {
                                            Console.WriteLine("4 cot đa Full 1 roi !");
                                            if (biggestValue < currentValue3)
                                            {
                                                Console.WriteLine("CLEAR");
                                                biggestValue = currentValue4;

                                                for (int l = k + 1; l < model.getColCount() - 1; l++)
                                                {
                                                    Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k] + "-" + color[l]);

                                                    if (value[l] > value[l + 1])
                                                    {
                                                        break;
                                                    }
                                                }
                                                if (value[k] > value[k + 1] || value[k + 1] > value[k + 2])
                                                {
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                if (value[k] > value[k + 1])
                                                {
                                                    break;
                                                }
                                                for (int l = k + 1; l < model.getColCount() - 1; l++)
                                                {
                                                    Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k] + "-" + color[l]);

                                                    if (value[l] > value[l + 1])
                                                    {
                                                        break;
                                                    }
                                                }
                                                if (value[k] > value[k + 1] || value[k + 1] > value[k + 2])
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            int compVar = 0;

                                            for (int l = k + 1; l < model.getColCount() - 1; l++) // chon mau o vong 5
                                            {
                                                //THÊM ĐIỀU KIỆN DỪNG
                                                if (value[l] + value[k] + value[q] + value[j] + value[i] < biggestValue)
                                                {
                                                    break;
                                                }

                                                currentCosts = 0;

                                                foreach (int temp in checkList4)
                                                {
                                                    if (zeroOne[l][temp] == 1)
                                                    {
                                                        currentCosts++;
                                                    }
                                                }

                                                if (currentValue4 + currentCosts > biggestValue)
                                                {
                                                    Console.WriteLine("> biggest value roi");
                                                    Console.WriteLine("CLEAR");
                                                    print = "";
                                                    biggestValue = currentValue4 + currentCosts;

                                                    String[] colorOut = new String[5];
                                                    int[] colorOutIndex = new int[5];

                                                    colorOut[0] = color[i];
                                                    colorOut[1] = color[j];
                                                    colorOut[2] = color[q];
                                                    colorOut[3] = color[k];
                                                    colorOut[4] = color[l];

                                                    colorOutIndex[0] = index[i];
                                                    colorOutIndex[1] = index[j];
                                                    colorOutIndex[2] = index[q];
                                                    colorOutIndex[3] = index[k];
                                                    colorOutIndex[4] = index[l];

                                                    for (int x = 0; x < 5; x++)
                                                    {
                                                        for (int y = x + 1; y < 5; y++)
                                                        {
                                                            if (colorOutIndex[x] > colorOutIndex[y])
                                                            {
                                                                String temp;
                                                                temp = colorOut[x];
                                                                colorOut[x] = colorOut[y];
                                                                colorOut[y] = temp;

                                                                int tempInt;
                                                                tempInt = colorOutIndex[x];
                                                                colorOutIndex[x] = colorOutIndex[y];
                                                                colorOutIndex[y] = tempInt;
                                                            }
                                                        }
                                                    }

                                                    print += colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + "-" + colorOut[3] + "-" + colorOut[4] + ": " + biggestValue + Environment.NewLine;
                                                    compVar = value[l];
                                                }
                                                else if (currentValue4 + currentCosts == biggestValue)
                                                {
                                                    Console.WriteLine("giong nhau roi");

                                                    if (value[l] < compVar)
                                                    {
                                                        continue;
                                                    }

                                                    if (l < model.getColCount() - 2 && value[l] > value[l + 1])
                                                    {
                                                        compVar = value[l];
                                                    }

                                                    //Console.WriteLine(biggestValue);
                                                    //Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k] + "-" + color[l]);
                                                    String[] colorOut = new String[5];
                                                    int[] colorOutIndex = new int[5];

                                                    colorOut[0] = color[i];
                                                    colorOut[1] = color[j];
                                                    colorOut[2] = color[q];
                                                    colorOut[3] = color[k];
                                                    colorOut[4] = color[l];

                                                    colorOutIndex[0] = index[i];
                                                    colorOutIndex[1] = index[j];
                                                    colorOutIndex[2] = index[q];
                                                    colorOutIndex[3] = index[k];
                                                    colorOutIndex[4] = index[l];

                                                    for (int x = 0; x < 5; x++)
                                                    {
                                                        for (int y = x + 1; y < 5; y++)
                                                        {
                                                            if (colorOutIndex[x] > colorOutIndex[y])
                                                            {
                                                                String temp;
                                                                temp = colorOut[x];
                                                                colorOut[x] = colorOut[y];
                                                                colorOut[y] = temp;

                                                                int tempInt;
                                                                tempInt = colorOutIndex[x];
                                                                colorOutIndex[x] = colorOutIndex[y];
                                                                colorOutIndex[y] = tempInt;
                                                            }
                                                        }
                                                    }

                                                    print += colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + "-" + colorOut[3] + "-" + colorOut[4] + ": " + biggestValue + Environment.NewLine;

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


                Console.WriteLine(i);
            }
            using (System.IO.StreamWriter writetext = new System.IO.StreamWriter("write.txt"))
            {
                writetext.WriteLine(print);
            }
        }
    }
}
