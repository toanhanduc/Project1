﻿using System;
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
        /// 
        
        bool canStop = true;
        string printOut = "";
        thietlaphesoModel model = new thietlaphesoModel();
        static int[] max;
        public void processGroup21()
        {
            thietlaphesoModel model = new thietlaphesoModel();
            string print = "";
            int currentColumnValue; // giá trị cột làm mốc
            int biggestValue = 0; // giá trị lớn nhất khi gộp 2 cột
            int[] value = model.getValue();
            int[][] zeroOne = model.getZeroOne();
            int[] index = model.getIndex();
            max = new int[model.getColCount() - 1];
            string[] color = model.getColor();
            int n = 2;

            if (value[n - 1] == 0)
            {
                canStop = false;
            }

            for (int i = 0; i < model.getColCount() - 2; i++) // chọn từng cột mốc trong 9 colors ( do không chọn đến cột cuối cùng làm mốc )
            {
                print = "";
                biggestValue = 0;
                // điều kiện dừng
                if (checkToBreak(n, biggestValue, value[i]) || (value[i + 1] == 0 && canStop))
                {
                    break;
                }

                List<int> checkList = new List<int>(); // list so sánh theo ngày không bán được
                currentColumnValue = value[i]; // giá trị cột làm mốc

                for (int j = 0; j < ExcelController.ngayketthuc - ExcelController.ngaybatdau + 1; j++) // duyệt từng ngày của màu
                {
                    if (zeroOne[i][j] == 0) // tìm ngày không bán được để so
                    {
                        checkList.Add(j);
                    }
                }

                // kiểm tra xem còn ô trống nào không
                if (!checkList.Any())
                {
                    biggestValue = value[i];
                    for (int j = i + 1; j < model.getColCount() - 1; j++)
                    {
                        if (value[j] == 0 && canStop)
                        {
                            break;
                        }

                        if (index[i] < index[j])
                        {
                            print += color[i] + "-" + color[j] + ": " + biggestValue + Environment.NewLine;
                        }
                        else
                        {
                            print += color[j] + "-" + color[i] + ": " + biggestValue + Environment.NewLine;
                        }
                    }

                }
                else // còn ô trống
                {
                    for (int j = 0; j < model.getColCount() - 1; j++) // duyệt các màu tiếp theo, có 10 màu
                    {
                        if (j == i)
                        {
                            continue;
                        }

                        if (value[j] < (biggestValue - value[i]) || (value[j] == 0 && canStop))
                        {
                            break;
                        }

                        List<int> checkListTemp = new List<int>(checkList); // list so sánh theo ngày không bán được
                        int currentCosts = 0; // Trọng số cột đang xét

                        foreach (int temp in checkListTemp) // duyệt từng ngày của màu để đánh trọng số
                        {
                            if (zeroOne[j][temp] == 1)
                            {
                                currentCosts++;
                            }
                        }

                        if (j > i || j < i && currentCosts + currentColumnValue < max[j])
                        {
                            if (currentCosts + currentColumnValue > biggestValue)
                            {
                                biggestValue = currentCosts + currentColumnValue;
                                if (index[i] < index[j])
                                {
                                    print = color[i] + "-" + color[j] + ": " + biggestValue + Environment.NewLine;
                                }
                                else
                                {
                                    print = color[j] + "-" + color[i] + ": " + biggestValue + Environment.NewLine;
                                }
                            }
                            else if (currentCosts + currentColumnValue == biggestValue)
                            {
                                if (index[i] < index[j])
                                {
                                    print += color[i] + "-" + color[j] + ": " + biggestValue + Environment.NewLine;
                                }
                                else
                                {
                                    print += color[j] + "-" + color[i] + ": " + biggestValue + Environment.NewLine;
                                }
                            }
                            
                        }
                        //if (currentCosts + currentColumnValue > biggestValue)
                        //{
                        //    biggestValue = currentCosts + currentColumnValue;
                        //    if (index[i] < index[j])
                        //    {
                        //        print = color[i] + "-" + color[j] + ": " + biggestValue + Environment.NewLine;
                        //    }
                        //    else
                        //    {
                        //        print = color[j] + "-" + color[i] + ": " + biggestValue + Environment.NewLine;
                        //    }
                        //}
                        //else if (currentCosts + currentColumnValue == biggestValue)
                        //{
                        //    if (index[i] < index[j])
                        //    {
                        //        print += color[i] + "-" + color[j] + ": " + biggestValue + Environment.NewLine;
                        //    }
                        //    else
                        //    {
                        //        print += color[j] + "-" + color[i] + ": " + biggestValue + Environment.NewLine;
                        //    }
                        //}
                    }
                }
                max[i] = biggestValue;
                printOut += print;
                if (value[i + 1] < value[i])
                {
                    break;
                }
                if (value[n - 1] == 0)
                {
                    break;
                }
                if(value[i + 1] < value[i])
                {
                    break;
                }
            }
            using (System.IO.StreamWriter writetext = new System.IO.StreamWriter("write2.txt"))
            {
                writetext.WriteLine(printOut);
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
            int biggestValue2 = 0;
            int[] value = model.getValue();
            int[][] zeroOne = model.getZeroOne();
            int[] index = model.getIndex();
            string[] color = model.getColor();
            int n = 3;

            if(value[n - 1] == 0)
            {
                canStop = false;
            }

            for (int i = 0; i < model.getColCount() - 3; i++)
            {
                print = "";
                biggestValue = 0;
                // điều kiện dừng
                if (checkToBreak(3, biggestValue, value[i]) || (value[i + 2] == 0 && canStop))
                {
                    break;
                }

                List<int> checkList1 = new List<int>(); // list so sánh theo ngày không bán được sau vòng 1
                currentValue1 = value[i];

                for (int j = 0; j < ExcelController.ngayketthuc - ExcelController.ngaybatdau + 1; j++) // duyệt từng ngày của màu
                {
                    if (zeroOne[i][j] == 0) // tìm ngày không bán được để so
                    {
                        checkList1.Add(j);
                    }
                }

                // kiểm tra xem còn ô trông không
                if (!checkList1.Any())
                {
                    biggestValue = currentValue1;
                    for (int j = i + 1; j < model.getColCount() - 2; j++)
                    {
                        if (value[j] == 0 && (canStop || (value[n - 1] == 0 && value[n - 2] != 0)))
                        {
                            break;
                        }
                        for (int q = j + 1; q < model.getColCount() - 1; q++)
                        {
                            if (value[q] == 0 && canStop)
                            {
                                break;
                            }

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
                            print += colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + ": " + biggestValue + Environment.NewLine;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < model.getColCount() - 2; j++) // chọn từng cột trong vòng 2
                    {
                        // điều kiện dừng
                        if (checkToBreak(n, biggestValue, value[i] + value[j]) || (value[j] == 0 && (canStop || value[n - 1] == 0 && value[n - 2] != 0)))
                        {
                            break;
                        }
                        if(j == i)
                        {
                            continue;
                        }

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
                        if (!checkList2.Any()) // full 1
                        {
                            if (biggestValue < currentValue2)
                            {
                                print = "";
                                biggestValue = currentValue2;

                                for (int q = j + 1; q < model.getColCount() - 1; q++)
                                {
                                    if (value[q] == 0 && canStop)
                                    {
                                        break;
                                    }

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

                                    print += colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + ": " + biggestValue + Environment.NewLine;
                                }

                            }
                            else
                            {
                                for (int q = j + 1; q < model.getColCount() - 1; q++)
                                {
                                    if (value[q] == 0 && canStop)
                                    {
                                        break;
                                    }

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

                                    print += colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + ": " + biggestValue + Environment.NewLine;
                                }
                            }
                        }
                        else
                        {
                            for (int q = j + 1; q < model.getColCount() - 1; q++)
                            {
                                if ((value[q] + value[j] + value[i] < biggestValue) || (value[q] == 0 && canStop))
                                {
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
                                    biggestValue = currentValue2 + currentCosts;

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


                                    print = colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + ": " + biggestValue + Environment.NewLine;
                                    
                                }
                                else if (currentCosts + currentValue2 == biggestValue)
                                {
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
                                    print += colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + ": " + biggestValue + Environment.NewLine;
                                }
                            }
                        }
                    }
                }
                printOut += print;
                if (value[n - 1] == 0 && value[n - 2] != 0)
                {
                    break;
                }
                if (value[i + 1] < value[i])
                {
                    break;
                }
            }
            using (System.IO.StreamWriter writetext = new System.IO.StreamWriter("write.txt"))
            {
                writetext.WriteLine(printOut);
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
            int n = 4;

            if (value[n - 1] == 0) // kiểm tra xem có lấy các giá trị = 0 không
            {
                canStop = false;
            }

            for (int i = 0; i < model.getColCount() - 4; i++) // để lại 3 cột để ghép màu
            {
                print = "";
                biggestValue = 0;
                // điều kiện dừng
                if (checkToBreak(n, biggestValue, value[i]) || (value[i + 3] == 0 && canStop))
                {
                    break;
                }

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
                    biggestValue = currentValue1;
                    for (int j = i + 1; j < model.getColCount() - 3; j++)
                    {
                        if (value[j] == 0 && (canStop || (value[n - 1] == 0 && value[n - 3] != 0)))
                        {
                            break;
                        }
                        for (int q = j + 1; q < model.getColCount() - 2; q++)
                        {
                            if (value[q] == 0 && (canStop || (value[n - 1] == 0 && value[n - 2] != 0)))
                            {
                                break;
                            }
                            for (int k = q + 1; k < model.getColCount() - 1; k++)
                            {
                                if (value[k] == 0 && canStop)
                                {
                                    break;
                                }
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
                else
                {
                    for (int j = i + 1; j < model.getColCount() - 3; j++) // chọn từng cột ở vòng 2, để lại 2 cột để ghép với 2 cột đã chọn
                    {
                        // điều kiện dừng
                        if (checkToBreak(n, biggestValue, value[i] + value[j]) || (value[j] == 0 && (canStop || (value[n - 1] == 0 && value[n - 3] != 0))))
                        {
                            break;
                        }

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
                            if (biggestValue < currentValue2)
                            {
                                print = "";
                                biggestValue = currentValue2;

                                for (int q = j + 1; q < model.getColCount() - 2; q++)
                                {
                                    if (value[q] == 0 && (canStop || (value[n - 1] == 0 && value[n - 2] != 0)))
                                    {
                                        break;
                                    }
                                    for (int k = q + 1; k < model.getColCount() - 1; k++)
                                    {
                                        if (value[k] == 0 && canStop)
                                        {
                                            break;
                                        }
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
                            else
                            {
                                for (int q = j + 1; q < model.getColCount() - 2; q++)
                                {
                                    if (value[q] == 0 && (canStop || (value[n - 1] == 0 && value[n - 2] != 0)))
                                    {
                                        break;
                                    }
                                    for (int k = q + 1; k < model.getColCount() - 1; k++)
                                    {
                                        if (value[k] == 0 && canStop)
                                        {
                                            break;
                                        }
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
                        else
                        {
                            for (int q = j + 1; q < model.getColCount() - 2; q++) // chọn từng cột ở vòng 3, để lại 1 cột để ghép màu
                            {
                                // điều kiện dừng
                                if (checkToBreak(n, biggestValue, value[i] + value[j] + value[q]) || (value[q] == 0 && (canStop || (value[n - 1] == 0 && value[n - 2] != 0))))
                                {
                                    break;
                                }

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
                                    if (biggestValue < currentValue3)
                                    {
                                        print = "";
                                        biggestValue = currentValue3;

                                        for (int k = q + 1; k < model.getColCount() - 1; k++)
                                        {
                                            if (value[k] == 0 && canStop)
                                            {
                                                break;
                                            }
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
                                    else
                                    {
                                        for (int k = q + 1; k < model.getColCount() - 1; k++)
                                        {
                                            if (value[k] == 0 && canStop)
                                            {
                                                break;
                                            }
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
                                else
                                {
                                    for (int k = q + 1; k < model.getColCount() - 1; k++) // chọn màu ở vòng 4
                                    {
                                        //THÊM ĐIỀU KIỆN DỪNG
                                        if (value[k] + value[q] + value[j] + value[i] < biggestValue || (value[k] == 0 && canStop))
                                        {
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
                                            biggestValue = currentValue3 + currentCosts;

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

                                            print = colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + "-" + colorOut[3] + ": " + biggestValue + Environment.NewLine;
                                            
                                        }
                                        else if (currentValue3 + currentCosts == biggestValue)
                                        {
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
                printOut += print;
                if (value[n - 1] == 0 && value[n - 2] != 0)
                {
                    break;
                }
                if (value[i + 1] < value[i])
                {
                    break;
                }
            }
            using (System.IO.StreamWriter writetext = new System.IO.StreamWriter("write.txt"))
            {
                writetext.WriteLine(printOut);
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
            int n = 5;

            if (value[n - 1] == 0)
            {
                canStop = false;
            }

            for (int i = 0; i < model.getColCount() - 5; i++) // để lại 4 cột để ghép màu
            {
                print = "";
                biggestValue = 0;
                // điều kiện dừng
                if (checkToBreak(n, biggestValue, value[i]) || (value[i + 4] == 0 && canStop))
                {
                    break;
                }

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
                    biggestValue = currentValue1;
                    for (int j = i + 1; j < model.getColCount() - 4; j++)
                    {
                        if (value[j] == 0 && (canStop || (value[n - 1] == 0 && value[n - 4] != 0)))
                        {
                            break;
                        }
                        for (int q = j + 1; q < model.getColCount() - 3; q++)
                        {
                            if (value[q] == 0 && (canStop || (value[n - 1] == 0 && value[n - 3] != 0)))
                            {
                                break;
                            }
                            for (int k = q + 1; k < model.getColCount() - 2; k++)
                            {
                                if (value[k] == 0 && (canStop || (value[n - 1] == 0 && value[n - 2] != 0)))
                                {
                                    break;
                                }
                                for (int l = k + 1; l < model.getColCount() - 1; l++)
                                {
                                    if (value[l] == 0 && canStop)
                                    {
                                        break;
                                    }
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
                else
                {
                    for (int j = i + 1; j < model.getColCount() - 4; j++) // chọn từng cột ở vòng 2, để lại 3 cột để ghép với 2 cột đã chọn
                    {
                        // điều kiện dừng
                        if (checkToBreak(n, biggestValue, value[i] + value[j]) || (value[j] == 0 && (canStop || (value[n - 1] == 0 && value[n - 4] != 0))))
                        {
                            break;
                        }

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
                            if (biggestValue < currentValue2)
                            {
                                biggestValue = currentValue2;
                                print = "";

                                for (int q = j + 1; q < model.getColCount() - 3; q++)
                                {
                                    if (value[q] == 0 && (canStop || (value[n - 1] == 0 && value[n - 3] != 0)))
                                    {
                                        break;
                                    }
                                    for (int k = q + 1; k < model.getColCount() - 2; k++)
                                    {
                                        if (value[k] == 0 && (canStop || (value[n - 1] == 0 && value[n - 2] != 0)))
                                        {
                                            break;
                                        }
                                        for (int l = k + 1; l < model.getColCount() - 1; l++)
                                        {
                                            if (value[l] == 0 && canStop)
                                            {
                                                break;
                                            }
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
                            else
                            {
                                for (int q = j + 1; q < model.getColCount() - 3; q++)
                                {
                                    if (value[q] == 0 && (canStop || (value[n - 1] == 0 && value[n - 3] != 0)))
                                    {
                                        break;
                                    }
                                    for (int k = q + 1; k < model.getColCount() - 2; k++)
                                    {
                                        if (value[k] == 0 && (canStop || (value[n - 1] == 0 && value[n - 2] != 0)))
                                        {
                                            break;
                                        }
                                        for (int l = k + 1; l < model.getColCount() - 1; l++)
                                        {
                                            if (value[l] == 0 && canStop)
                                            {
                                                break;
                                            }
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
                        else
                        {
                            for (int q = j + 1; q < model.getColCount() - 3; q++) // chọn từng cột ở vòng 3, để lại 2 cột để ghép màu
                            {
                                // điều kiện dừng
                                if (checkToBreak(n, biggestValue, value[i] + value[j] + value[q]) || (value[q] == 0 && (canStop || (value[n - 1] == 0 && value[n - 3] != 0))))
                                {
                                    break;
                                }
                                
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
                                    if (biggestValue < currentValue3)
                                    {
                                        biggestValue = currentValue3;
                                        print = "";

                                        for (int k = q + 1; k < model.getColCount() - 2; k++)
                                        {
                                            if (value[k] == 0 && (canStop || (value[n - 1] == 0 && value[n - 2] != 0)))
                                            {
                                                break;
                                            }
                                            for (int l = k + 1; l < model.getColCount() - 1; l++)
                                            {
                                                if (value[l] == 0 && canStop)
                                                {
                                                    break;
                                                }
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
                                    else
                                    {
                                        for (int k = q + 1; k < model.getColCount() - 2; k++)
                                        {
                                            if (value[k] == 0 && (canStop || (value[n - 1] == 0 && value[n - 2] != 0)))
                                            {
                                                break;
                                            }
                                            for (int l = k + 1; l < model.getColCount() - 1; l++)
                                            {
                                                if (value[l] == 0 && canStop)
                                                {
                                                    break;
                                                }
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
                                else
                                {
                                    for (int k = q + 1; k < model.getColCount() - 2; k++) // chọn màu ở vòng 4, để lại 1 màu để ghép
                                    {
                                        // điều kiện dừng
                                        if (checkToBreak(n, biggestValue, value[i] + value[j] + value[q] + value[k]) || (value[k] == 0 && (canStop || (value[n - 1] == 0 && value[n - 2] != 0))))
                                        {
                                            break;
                                        }

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
                                            if (biggestValue < currentValue3)
                                            {
                                                biggestValue = currentValue4;
                                                print = "";

                                                for (int l = k + 1; l < model.getColCount() - 1; l++)
                                                {
                                                    if (value[l] == 0 && canStop)
                                                    {
                                                        break;
                                                    }
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
                                            else
                                            {
                                                for (int l = k + 1; l < model.getColCount() - 1; l++)
                                                {
                                                    if (value[l] == 0 && canStop)
                                                    {
                                                        break;
                                                    }
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
                                        else
                                        {
                                            for (int l = k + 1; l < model.getColCount() - 1; l++) // chon mau o vong 5
                                            {
                                                //THÊM ĐIỀU KIỆN DỪNG
                                                if (value[l] + value[k] + value[q] + value[j] + value[i] < biggestValue || (value[l] == 0 && canStop))
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
                                                    print = colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + "-" + colorOut[3] + "-" + colorOut[4] + ": " + biggestValue + Environment.NewLine;
                                                }
                                                else if (currentValue4 + currentCosts == biggestValue)
                                                {
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
                printOut += print;
                if (value[n - 1] == 0 && value[n - 2] != 0)
                {
                    break;
                }
                if (value[i + 1] < value[i])
                {
                    break;
                }
            }
            using (System.IO.StreamWriter writetext = new System.IO.StreamWriter("write.txt"))
            {
                writetext.WriteLine(printOut);
            } 
        }

        // new
        public void processGroup2()
        {
            thietlaphesoModel model = new thietlaphesoModel();
            string print = "";
            int currentValue1; // giá trị ở vòng 1
            int currentValue2; // giá trị ở vòng 2
            int currentValue3; // giá trị ở vòng 3
            int currentValue4; // giá trị ở vòng 4
            int currentValue5;
            int biggestValue = 0;
            int biggestValue2 = 0;
            int biggestValue3 = 0;
            int[] value = model.getValue();
            int[][] zeroOne = model.getZeroOne();
            int[] index = model.getIndex();
            int[] max = new int[model.getColCount() - 1];
            string[] color = model.getColor();
            List<int> savedRound2 = new List<int>();
            List<int> savedRound3 = new List<int>();
            List<int> savedRound4 = new List<int>();

            int n = 5;

            if (value[n - 1] == 0)
            {
                canStop = false;
            }

            for (int i = 0; i < model.getColCount() - n; i++)
            {
                print = "";
                biggestValue = 0;
                biggestValue2 = 0;
                List<int> checkList1 = new List<int>(); // list so sánh theo ngày không bán được sau vòng 1
                currentValue1 = value[i];
                for (int j = 0; j < ExcelController.ngayketthuc - ExcelController.ngaybatdau + 1; j++) // tạo list chứa những ngày không bán được của màu đầu tiên
                {
                    if (zeroOne[i][j] == 0) // tìm ngày không bán được để add vào list
                    {
                        checkList1.Add(j);
                    }
                }

                if (!checkList1.Any()) // màu đầu tiên full 1
                {
                    biggestValue = currentValue1;
                    for (int j = i + 1; j < model.getColCount() - n + 1; j++)
                    {
                        if (n == 2)
                        {
                            print += color[i] + " - " + color[j] + ": " + biggestValue + Environment.NewLine;
                            continue;
                        }
                        else // n > 2
                        {
                            for (int q = j + 1; q < model.getColCount() - n + 2; q++)
                            {
                                
                                if (n == 3)
                                {
                                    print += color[i] + " - " + color[j] + " - " + color[q] + ": " + biggestValue + Environment.NewLine;
                                    continue;
                                }
                                else // n > 3
                                {
                                    for (int k = q + 1; k < model.getColCount() - n + 3; k++)
                                    {
                                        if (n == 4)
                                        {
                                            print += color[i] + " - " + color[j] + " - " + color[q] + "- " + color[k] + ": " + biggestValue + Environment.NewLine;
                                            continue;
                                        }
                                        else // n > 4
                                        {
                                            for (int l = k + 1; l < model.getColCount() - n + 4; l++)
                                            {
                                                print += color[i] + " - " + color[j] + " - " + color[q] + "- " + color[k] + " - " + color[l] + ": " + biggestValue + Environment.NewLine;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else // màu đầu tiên không full 1
                {
                    for (int j = 0; j < model.getColCount() - n + 1; j++)
                    {
                        if (j == i)
                        {
                            continue;
                        }

                        List<int> checkList2 = new List<int>(checkList1);

                        int currentCosts = 0; // trọng số cột hiện tại

                        foreach (int temp in checkList1) // đánh trọng số cho màu thứ 2
                        {
                            if (zeroOne[j][temp] == 1)
                            {
                                currentCosts++;
                                checkList2.Remove(temp);
                            }
                        }
                        currentValue2 = currentValue1 + currentCosts;

                        if (currentValue2 < biggestValue2)
                        {
                            continue;
                        }

                        if (n == 2)
                        {
                            if (j > i || j < i && currentValue2 < max[j])
                            {
                                if (currentValue2 > biggestValue)
                                {
                                    biggestValue = currentValue2;
                                    if (index[i] < index[j])
                                    {
                                        print = color[i] + "-" + color[j] + ": " + biggestValue + Environment.NewLine;
                                    }
                                    else
                                    {
                                        print = color[j] + "-" + color[i] + ": " + biggestValue + Environment.NewLine;
                                    }

                                }
                                else if (currentValue2 == biggestValue)
                                {
                                    if (index[i] < index[j])
                                    {
                                        print += color[i] + "-" + color[j] + ": " + biggestValue + Environment.NewLine;
                                    }
                                    else
                                    {
                                        print += color[j] + "-" + color[i] + ": " + biggestValue + Environment.NewLine;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (j > i || j < i && currentValue2 < max[j])
                            {
                                if (currentValue2 > biggestValue2)
                                {
                                    biggestValue2 = currentValue2;
                                    savedRound2.Clear();
                                    savedRound2.Add(j);
                                }
                                else if (currentValue2 == biggestValue2)
                                {
                                    savedRound2.Add(j);
                                }
                            }
                        }
                        printOut += print;
                    }

                    if (n > 2)
                    {
                        foreach (int j in savedRound2)
                        {
                            biggestValue = 0;
                            biggestValue3 = 0;
                            for (int q = 0; q < model.getColCount() - n + 1; q++)
                            {
                                if (q == i || q == j)
                                {
                                    continue;
                                }

                                List<int> checkList2 = new List<int>(checkList1);

                                int currentCosts = 0; // trọng số cột hiện tại

                                foreach (int temp in checkList1) // đánh trọng số cho màu thứ 2
                                {
                                    if (zeroOne[j][temp] == 1)
                                    {
                                        currentCosts++;
                                        checkList2.Remove(temp);
                                    }
                                }
                                currentValue2 = currentValue1 + currentCosts;
                                //////////////////
                                List<int> checkList3 = new List<int>(checkList2);
                                currentCosts = 0;

                                foreach (int temp in checkList2) // đánh trọng số cho màu thứ 2
                                {
                                    if (zeroOne[q][temp] == 1)
                                    {
                                        currentCosts++;
                                        checkList3.Remove(temp);
                                    }
                                }
                                currentValue3 = currentValue2 + currentCosts;

                                if(currentValue3 < biggestValue3)
                                {
                                    continue;
                                }

                                if (n == 3)
                                {
                                    if (currentValue3 > biggestValue)
                                    {
                                        biggestValue = currentValue3;

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
                                                    //String temp;
                                                    //temp = colorOut[x];
                                                    //colorOut[x] = colorOut[y];
                                                    //colorOut[y] = temp;

                                                    //int tempInt;
                                                    //tempInt = colorOutIndex[x];
                                                    //colorOutIndex[x] = colorOutIndex[y];
                                                    //colorOutIndex[y] = tempInt;
                                                }
                                            }
                                        }
                                        print = colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + ": " + biggestValue + Environment.NewLine;
                                    }
                                    else if (currentValue3 == biggestValue)
                                    {
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
                                                    //String temp;
                                                    //temp = colorOut[x];
                                                    //colorOut[x] = colorOut[y];
                                                    //colorOut[y] = temp;

                                                    //int tempInt;
                                                    //tempInt = colorOutIndex[x];
                                                    //colorOutIndex[x] = colorOutIndex[y];
                                                    //colorOutIndex[y] = tempInt;
                                                }
                                            }
                                        }
                                        print += colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + ": " + biggestValue + Environment.NewLine;
                                    }
                                }
                                else
                                {
                                    if (currentValue3 > biggestValue)
                                    {
                                        biggestValue = currentValue3;
                                        savedRound3.Clear();
                                        savedRound3.Add(q);
                                    }
                                    else if (currentValue3 == biggestValue)
                                    {
                                        savedRound3.Add(q);
                                    }
                                }
                            }

                            if (n > 3)
                            {
                                foreach (int q in savedRound3)
                                {
                                    biggestValue = 0;
                                    for (int k = 0; k < model.getColCount() - n + 1; k++)
                                    {
                                        if (k == i || k == j || k == q)
                                        {
                                            continue;
                                        }
                                        List<int> checkList2 = new List<int>(checkList1);

                                        int currentCosts = 0; // trọng số cột hiện tại

                                        foreach (int temp in checkList1) // đánh trọng số cho màu thứ 2
                                        {
                                            if (zeroOne[j][temp] == 1)
                                            {
                                                currentCosts++;
                                                checkList2.Remove(temp);
                                            }
                                        }
                                        currentValue2 = currentValue1 + currentCosts;

                                        List<int> checkList3 = new List<int>(checkList2);
                                        currentCosts = 0;

                                        foreach (int temp in checkList2) // đánh trọng số cho màu thứ 2
                                        {
                                            if (zeroOne[q][temp] == 1)
                                            {
                                                currentCosts++;
                                                checkList3.Remove(temp);
                                            }
                                        }
                                        currentValue3 = currentValue2 + currentCosts;
                                        //////////////////////////
                                        List<int> checkList4 = new List<int>(checkList3);
                                        currentCosts = 0;

                                        foreach (int temp in checkList3) // đánh trọng số cho màu thứ 2
                                        {
                                            if (zeroOne[k][temp] == 1)
                                            {
                                                currentCosts++;
                                                checkList4.Remove(temp);
                                            }
                                        }
                                        currentValue4 = currentValue3 + currentCosts;

                                        if (n == 4)
                                        {
                                            if (currentValue4 > biggestValue)
                                            {
                                                biggestValue = currentValue4;

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

                                                //for (int x = 0; x < 4; x++)
                                                //{
                                                //    for (int y = x + 1; y < 4; y++)
                                                //    {
                                                //        if (colorOutIndex[x] > colorOutIndex[y])
                                                //        {
                                                //            String temp;
                                                //            temp = colorOut[x];
                                                //            colorOut[x] = colorOut[y];
                                                //            colorOut[y] = temp;

                                                //            int tempInt;
                                                //            tempInt = colorOutIndex[x];
                                                //            colorOutIndex[x] = colorOutIndex[y];
                                                //            colorOutIndex[y] = tempInt;
                                                //        }
                                                //    }
                                                //}

                                                print = colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + "-" + colorOut[3] + ": " + biggestValue + Environment.NewLine;

                                            }
                                            else if (currentValue4 == biggestValue)
                                            {
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

                                                //for (int x = 0; x < 4; x++)
                                                //{
                                                //    for (int y = x + 1; y < 4; y++)
                                                //    {
                                                //        if (colorOutIndex[x] > colorOutIndex[y])
                                                //        {
                                                //            String temp;
                                                //            temp = colorOut[x];
                                                //            colorOut[x] = colorOut[y];
                                                //            colorOut[y] = temp;

                                                //            int tempInt;
                                                //            tempInt = colorOutIndex[x];
                                                //            colorOutIndex[x] = colorOutIndex[y];
                                                //            colorOutIndex[y] = tempInt;
                                                //        }
                                                //    }
                                                //}
                                                print += colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + "-" + colorOut[3] + ": " + biggestValue + Environment.NewLine;
                                            }
                                        }
                                        else
                                        {
                                            if (currentValue4 > biggestValue)
                                            {
                                                biggestValue = currentValue4;
                                                savedRound4.Clear();
                                                savedRound4.Add(k);
                                            }
                                            else if (currentValue4 == biggestValue)
                                            {
                                                savedRound4.Add(k);
                                            }
                                        }

                                    }

                                    if(n > 4)
                                    {
                                        foreach(int k in savedRound4)
                                        {
                                            biggestValue = 0;
                                            for (int l = 0; l < model.getColCount() - n + 1; l++)
                                            {
                                                if(l == i || l == j || l == q || l == k)
                                                {
                                                    continue;
                                                }

                                                List<int> checkList2 = new List<int>(checkList1);

                                                int currentCosts = 0; // trọng số cột hiện tại

                                                foreach (int temp in checkList1) // đánh trọng số cho màu thứ 2
                                                {
                                                    if (zeroOne[j][temp] == 1)
                                                    {
                                                        currentCosts++;
                                                        checkList2.Remove(temp);
                                                    }
                                                }
                                                currentValue2 = currentValue1 + currentCosts;

                                                List<int> checkList3 = new List<int>(checkList2);
                                                currentCosts = 0;

                                                foreach (int temp in checkList2) // đánh trọng số cho màu thứ 2
                                                {
                                                    if (zeroOne[q][temp] == 1)
                                                    {
                                                        currentCosts++;
                                                        checkList3.Remove(temp);
                                                    }
                                                }
                                                currentValue3 = currentValue2 + currentCosts;

                                                List<int> checkList4 = new List<int>(checkList3);
                                                currentCosts = 0;

                                                foreach (int temp in checkList3) // đánh trọng số cho màu thứ 2
                                                {
                                                    if (zeroOne[k][temp] == 1)
                                                    {
                                                        currentCosts++;
                                                        checkList4.Remove(temp);
                                                    }
                                                }
                                                currentValue4 = currentValue3 + currentCosts;
                                                /////////////////////////
                                                List<int> checkList5 = new List<int>(checkList4);
                                                currentCosts = 0;

                                                foreach (int temp in checkList4) // đánh trọng số cho màu thứ 2
                                                {
                                                    if (zeroOne[l][temp] == 1)
                                                    {
                                                        currentCosts++;
                                                        checkList5.Remove(temp);
                                                    }
                                                }
                                                currentValue5 = currentValue4 + currentCosts;

                                                if (currentValue4 + currentCosts > biggestValue)
                                                {
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

                                                    //for (int x = 0; x < 5; x++)
                                                    //{
                                                    //    for (int y = x + 1; y < 5; y++)
                                                    //    {
                                                    //        if (colorOutIndex[x] > colorOutIndex[y])
                                                    //        {
                                                    //            String temp;
                                                    //            temp = colorOut[x];
                                                    //            colorOut[x] = colorOut[y];
                                                    //            colorOut[y] = temp;

                                                    //            int tempInt;
                                                    //            tempInt = colorOutIndex[x];
                                                    //            colorOutIndex[x] = colorOutIndex[y];
                                                    //            colorOutIndex[y] = tempInt;
                                                    //        }
                                                    //    }
                                                    //}
                                                    print = colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + "-" + colorOut[3] + "-" + colorOut[4] + ": " + biggestValue + Environment.NewLine;
                                                }
                                                else if (currentValue4 + currentCosts == biggestValue)
                                                {
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

                                                    //for (int x = 0; x < 5; x++)
                                                    //{
                                                    //    for (int y = x + 1; y < 5; y++)
                                                    //    {
                                                    //        if (colorOutIndex[x] > colorOutIndex[y])
                                                    //        {
                                                    //            String temp;
                                                    //            temp = colorOut[x];
                                                    //            colorOut[x] = colorOut[y];
                                                    //            colorOut[y] = temp;

                                                    //            int tempInt;
                                                    //            tempInt = colorOutIndex[x];
                                                    //            colorOutIndex[x] = colorOutIndex[y];
                                                    //            colorOutIndex[y] = tempInt;
                                                    //        }
                                                    //    }
                                                    //}
                                                    print += colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + "-" + colorOut[3] + "-" + colorOut[4] + ": " + biggestValue + Environment.NewLine;
                                                }
                                            }

                                            printOut += print;
                                        }
                                    }

                                }
                            }

                        }

                        
                    }

                    // tam thoi comment


                        //else if (!checkList2.Any()) // 2 màu làm full 1
                        //{
                        //    if(biggestValue < currentValue2) // mốc mới
                        //    {
                        //        biggestValue = currentValue2;
                        //        print = "";
                        //        for (int q = j + 1; q < model.getColCount() - n + 2; q++)
                        //        {
                        //            if (n == 3)
                        //            {
                        //                print += color[i] + " - " + color[j] + " - " + color[q] + ": " + biggestValue + Environment.NewLine;
                        //                continue;
                        //            }
                        //            else // n > 3
                        //            {
                        //                for (int k = q + 1; k < model.getColCount() - n + 3; k++)
                        //                {
                        //                    if (n == 4)
                        //                    {
                        //                        print += color[i] + " - " + color[j] + " - " + color[q] + "- " + color[k] + ": " + biggestValue + Environment.NewLine;
                        //                        continue;
                        //                    }
                        //                    else // n > 4
                        //                    {
                        //                        for (int l = k + 1; l < model.getColCount() - n + 4; l++)
                        //                        {
                        //                            print += color[i] + " - " + color[j] + " - " + color[q] + "- " + color[k] + " - " + color[l] + ": " + biggestValue + Environment.NewLine;
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //        }

                        //    }
                        //    else // biggestValue == currentValue2
                        //    {
                        //        biggestValue = currentValue2;
                        //        for (int q = j + 1; q < model.getColCount() - n + 2; q++)
                        //        {
                        //            if (n == 3)
                        //            {
                        //                print += color[i] + " - " + color[j] + " - " + color[q] + ": " + biggestValue + Environment.NewLine;
                        //                continue;
                        //            }
                        //            else // n > 3
                        //            {
                        //                for (int k = q + 1; k < model.getColCount() - n + 3; k++)
                        //                {
                        //                    if (n == 4)
                        //                    {
                        //                        print += color[i] + " - " + color[j] + " - " + color[q] + "- " + color[k] + ": " + biggestValue + Environment.NewLine;
                        //                        continue;
                        //                    }
                        //                    else // n > 4
                        //                    {
                        //                        for (int l = k + 1; l < model.getColCount() - n + 4; l++)
                        //                        {
                        //                            print += color[i] + " - " + color[j] + " - " + color[q] + "- " + color[k] + " - " + color[l] + ": " + biggestValue + Environment.NewLine;
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        //else // 2 màu không full 1
                        //{
                        //    for (int q = 0; q < model.getColCount() - n + 2; q++)
                        //    {
                        //        if(q == i || q == j)
                        //        {
                        //            continue;
                        //        }

                        //        List<int> checkList3 = new List<int>(checkList2);

                        //        currentCosts = 0;

                        //        foreach (int temp in checkList2)
                        //        {
                        //            if (zeroOne[q][temp] == 1)
                        //            {
                        //                currentCosts++;
                        //                checkList3.Remove(temp);
                        //            }
                        //        }

                        //        currentValue3 = currentValue2 + currentCosts;

                        //        if (n == 3)
                        //        {
                        //            //if (q > j || (q < j && currentValue3 < max[q]))
                        //            //{
                        //                if (currentValue3 > biggestValue && currentValue2 == biggestValue2)
                        //                {
                        //                    biggestValue = currentValue3;

                        //                    String[] colorOut = new String[3];
                        //                    int[] colorOutIndex = new int[3];

                        //                    colorOut[0] = color[i];
                        //                    colorOut[1] = color[j];
                        //                    colorOut[2] = color[q];

                        //                    colorOutIndex[0] = index[i];
                        //                    colorOutIndex[1] = index[j];
                        //                    colorOutIndex[2] = index[q];

                        //                    for (int x = 0; x < 3; x++)
                        //                    {
                        //                        for (int y = x + 1; y < 3; y++)
                        //                        {
                        //                            if (colorOutIndex[x] > colorOutIndex[y])
                        //                            {
                        //                                //String temp;
                        //                                //temp = colorOut[x];
                        //                                //colorOut[x] = colorOut[y];
                        //                                //colorOut[y] = temp;

                        //                                //int tempInt;
                        //                                //tempInt = colorOutIndex[x];
                        //                                //colorOutIndex[x] = colorOutIndex[y];
                        //                                //colorOutIndex[y] = tempInt;
                        //                            }
                        //                        }
                        //                    }
                        //                    print = colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + ": " + biggestValue + Environment.NewLine;
                        //                }
                        //                else if (currentValue3 == biggestValue && currentValue2 == biggestValue2)
                        //                {
                        //                    String[] colorOut = new String[3];
                        //                    int[] colorOutIndex = new int[3];

                        //                    colorOut[0] = color[i];
                        //                    colorOut[1] = color[j];
                        //                    colorOut[2] = color[q];

                        //                    colorOutIndex[0] = index[i];
                        //                    colorOutIndex[1] = index[j];
                        //                    colorOutIndex[2] = index[q];

                        //                    for (int x = 0; x < 3; x++)
                        //                    {
                        //                        for (int y = x + 1; y < 3; y++)
                        //                        {
                        //                            if (colorOutIndex[x] > colorOutIndex[y])
                        //                            {
                        //                                //String temp;
                        //                                //temp = colorOut[x];
                        //                                //colorOut[x] = colorOut[y];
                        //                                //colorOut[y] = temp;

                        //                                //int tempInt;
                        //                                //tempInt = colorOutIndex[x];
                        //                                //colorOutIndex[x] = colorOutIndex[y];
                        //                                //colorOutIndex[y] = tempInt;
                        //                            }
                        //                        }
                        //                    }
                        //                    print += colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + ": " + biggestValue + Environment.NewLine;
                        //                }
                                        
                        //            }
                        //        //}
                        //        else if (!checkList3.Any()) // 3 màu làm full 1
                        //        {
                        //            if(biggestValue < currentValue3) // mốc mới
                        //            {
                        //                biggestValue = currentValue3;
                        //                print = "";
                        //                for (int k = q + 1; k < model.getColCount() - n + 3; k++)
                        //                {
                        //                    if (n == 4)
                        //                    {
                        //                        print += color[i] + " - " + color[j] + " - " + color[q] + "- " + color[k] + ": " + biggestValue + Environment.NewLine;
                        //                        continue;
                        //                    }
                        //                    else // n > 4
                        //                    {
                        //                        for (int l = k + 1; l < model.getColCount() - n + 4; l++)
                        //                        {
                        //                            print += color[i] + " - " + color[j] + " - " + color[q] + "- " + color[k] + " - " + color[l] + ": " + biggestValue + Environment.NewLine;
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //            else // biggestValue == currentValue3
                        //            {
                        //                biggestValue = currentValue3;
                        //                for (int k = q + 1; k < model.getColCount() - n + 3; k++)
                        //                {
                        //                    if (n == 4)
                        //                    {
                        //                        print += color[i] + " - " + color[j] + " - " + color[q] + "- " + color[k] + ": " + biggestValue + Environment.NewLine;
                        //                        continue;
                        //                    }
                        //                    else // n > 4
                        //                    {
                        //                        for (int l = k + 1; l < model.getColCount() - n + 4; l++)
                        //                        {
                        //                            print += color[i] + " - " + color[j] + " - " + color[q] + "- " + color[k] + " - " + color[l] + ": " + biggestValue + Environment.NewLine;
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //        }
                        //        else // 3 màu không làm full 1
                        //        {
                        //            for (int k = q + 1; k < model.getColCount() - n + 3; k++)
                        //            {
                        //                List<int> checkList4 = new List<int>(checkList3);

                        //                currentCosts = 0;

                        //                foreach (int temp in checkList3)
                        //                {
                        //                    if (zeroOne[k][temp] == 1)
                        //                    {
                        //                        currentCosts++;
                        //                        checkList4.Remove(temp);
                        //                    }
                        //                }

                        //                currentValue4 = currentValue3 + currentCosts;

                        //                if(n == 4)
                        //                {
                        //                    if (currentValue4 > biggestValue)
                        //                    {
                        //                        biggestValue = currentValue4;

                        //                        String[] colorOut = new String[4];
                        //                        int[] colorOutIndex = new int[4];

                        //                        colorOut[0] = color[i];
                        //                        colorOut[1] = color[j];
                        //                        colorOut[2] = color[q];
                        //                        colorOut[3] = color[k];

                        //                        colorOutIndex[0] = index[i];
                        //                        colorOutIndex[1] = index[j];
                        //                        colorOutIndex[2] = index[q];
                        //                        colorOutIndex[3] = index[k];

                        //                        for (int x = 0; x < 4; x++)
                        //                        {
                        //                            for (int y = x + 1; y < 4; y++)
                        //                            {
                        //                                if (colorOutIndex[x] > colorOutIndex[y])
                        //                                {
                        //                                    String temp;
                        //                                    temp = colorOut[x];
                        //                                    colorOut[x] = colorOut[y];
                        //                                    colorOut[y] = temp;

                        //                                    int tempInt;
                        //                                    tempInt = colorOutIndex[x];
                        //                                    colorOutIndex[x] = colorOutIndex[y];
                        //                                    colorOutIndex[y] = tempInt;
                        //                                }
                        //                            }
                        //                        }

                        //                        print = colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + "-" + colorOut[3] + ": " + biggestValue + Environment.NewLine;

                        //                    }
                        //                    else if (currentValue4 == biggestValue)
                        //                    {
                        //                        String[] colorOut = new String[4];
                        //                        int[] colorOutIndex = new int[4];

                        //                        colorOut[0] = color[i];
                        //                        colorOut[1] = color[j];
                        //                        colorOut[2] = color[q];
                        //                        colorOut[3] = color[k];

                        //                        colorOutIndex[0] = index[i];
                        //                        colorOutIndex[1] = index[j];
                        //                        colorOutIndex[2] = index[q];
                        //                        colorOutIndex[3] = index[k];

                        //                        for (int x = 0; x < 4; x++)
                        //                        {
                        //                            for (int y = x + 1; y < 4; y++)
                        //                            {
                        //                                if (colorOutIndex[x] > colorOutIndex[y])
                        //                                {
                        //                                    String temp;
                        //                                    temp = colorOut[x];
                        //                                    colorOut[x] = colorOut[y];
                        //                                    colorOut[y] = temp;

                        //                                    int tempInt;
                        //                                    tempInt = colorOutIndex[x];
                        //                                    colorOutIndex[x] = colorOutIndex[y];
                        //                                    colorOutIndex[y] = tempInt;
                        //                                }
                        //                            }
                        //                        }
                        //                        print += colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + "-" + colorOut[3] + ": " + biggestValue + Environment.NewLine;
                        //                    }
                        //                }
                        //                else if (!checkList4.Any()) // 4 màu làm full 1
                        //                {
                        //                    if(biggestValue < currentValue4) // mốc mới
                        //                    {
                        //                        biggestValue = currentValue4;
                        //                        print = "";
                        //                        for (int l = k + 1; l < model.getColCount() - n + 4; l++)
                        //                        {
                        //                            print += color[i] + " - " + color[j] + " - " + color[q] + "- " + color[k] + " - " + color[l] + ": " + biggestValue + Environment.NewLine;
                        //                        }
                        //                    }
                        //                    else // biggestValue == currentValue4
                        //                    {
                        //                        biggestValue = currentValue4;
                        //                        for (int l = k + 1; l < model.getColCount() - n + 4; l++)
                        //                        {
                        //                            print += color[i] + " - " + color[j] + " - " + color[q] + "- " + color[k] + " - " + color[l] + ": " + biggestValue + Environment.NewLine;
                        //                        }
                        //                    }
                        //                }
                        //                else // 4 màu không làm full 1
                        //                {
                        //                    for (int l = k + 1; l < model.getColCount() - n + 4; l++)
                        //                    {
                        //                        currentCosts = 0;

                        //                        foreach (int temp in checkList4)
                        //                        {
                        //                            if (zeroOne[l][temp] == 1)
                        //                            {
                        //                                currentCosts++;
                        //                            }
                        //                        }

                        //                        if (currentValue4 + currentCosts > biggestValue)
                        //                        {
                        //                            biggestValue = currentValue4 + currentCosts;
                        //                            print = "";

                        //                            String[] colorOut = new String[5];
                        //                            int[] colorOutIndex = new int[5];

                        //                            colorOut[0] = color[i];
                        //                            colorOut[1] = color[j];
                        //                            colorOut[2] = color[q];
                        //                            colorOut[3] = color[k];
                        //                            colorOut[4] = color[l];

                        //                            colorOutIndex[0] = index[i];
                        //                            colorOutIndex[1] = index[j];
                        //                            colorOutIndex[2] = index[q];
                        //                            colorOutIndex[3] = index[k];
                        //                            colorOutIndex[4] = index[l];

                        //                            for (int x = 0; x < 5; x++)
                        //                            {
                        //                                for (int y = x + 1; y < 5; y++)
                        //                                {
                        //                                    if (colorOutIndex[x] > colorOutIndex[y])
                        //                                    {
                        //                                        String temp;
                        //                                        temp = colorOut[x];
                        //                                        colorOut[x] = colorOut[y];
                        //                                        colorOut[y] = temp;

                        //                                        int tempInt;
                        //                                        tempInt = colorOutIndex[x];
                        //                                        colorOutIndex[x] = colorOutIndex[y];
                        //                                        colorOutIndex[y] = tempInt;
                        //                                    }
                        //                                }
                        //                            }
                        //                            print += colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + "-" + colorOut[3] + "-" + colorOut[4] + ": " + biggestValue + Environment.NewLine;
                        //                        }
                        //                        else if (currentValue4 + currentCosts == biggestValue)
                        //                        {
                        //                            String[] colorOut = new String[5];
                        //                            int[] colorOutIndex = new int[5];

                        //                            colorOut[0] = color[i];
                        //                            colorOut[1] = color[j];
                        //                            colorOut[2] = color[q];
                        //                            colorOut[3] = color[k];
                        //                            colorOut[4] = color[l];

                        //                            colorOutIndex[0] = index[i];
                        //                            colorOutIndex[1] = index[j];
                        //                            colorOutIndex[2] = index[q];
                        //                            colorOutIndex[3] = index[k];
                        //                            colorOutIndex[4] = index[l];

                        //                            for (int x = 0; x < 5; x++)
                        //                            {
                        //                                for (int y = x + 1; y < 5; y++)
                        //                                {
                        //                                    if (colorOutIndex[x] > colorOutIndex[y])
                        //                                    {
                        //                                        String temp;
                        //                                        temp = colorOut[x];
                        //                                        colorOut[x] = colorOut[y];
                        //                                        colorOut[y] = temp;

                        //                                        int tempInt;
                        //                                        tempInt = colorOutIndex[x];
                        //                                        colorOutIndex[x] = colorOutIndex[y];
                        //                                        colorOutIndex[y] = tempInt;
                        //                                    }
                        //                                }
                        //                            }
                        //                            print += colorOut[0] + "-" + colorOut[1] + "-" + colorOut[2] + "-" + colorOut[3] + "-" + colorOut[4] + ": " + biggestValue + Environment.NewLine;
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                    //}
                }
                max[i] = biggestValue;
                //printOut += print;
                if (value[i + 1]  < value[i])
                {
                    break;
                }
            }
            using (System.IO.StreamWriter writetext = new System.IO.StreamWriter("write.txt"))
            {
                writetext.WriteLine(printOut);
            }
        }


        // biggestValue: Giá trị lớn nhất
        // valueCol1: Giá trị của cột được chọn làm mốc 1
        // valueCol2: Giá trị của cột được chọn làm cột 2
        public bool checkToBreak(int n,int biggestValue, int valueCol)
        {
            if (biggestValue % n == 0 && valueCol < biggestValue / n)
            {
                return true;
            }
            if (biggestValue % n != 0 && valueCol == biggestValue / n)
            {
                return true;
            }
            return false;
        }


    }
}
