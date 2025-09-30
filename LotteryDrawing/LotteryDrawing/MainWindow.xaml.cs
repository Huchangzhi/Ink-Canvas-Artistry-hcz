using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace LotteryDrawing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadNames();
        }

        public int TotalCount = 1; // 抽取人数，默认为1
        public int PeopleCount = 50; // 总人数，默认为50
        public List<string> Names = new List<string>();

        private void BorderBtnAdd_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (TotalCount >= PeopleCount) return;
            TotalCount++;
            LabelNumberCount.Text = TotalCount.ToString();
        }

        private void BorderBtnMinus_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (TotalCount < 2) return;
            TotalCount--;
            LabelNumberCount.Text = TotalCount.ToString();
        }

        private void BorderBtnRand_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Random random = new Random();
            string outputString = "";
            List<string> outputs = new List<string>();
            List<int> rands = new List<int>();

            BorderBtnRandCover.Visibility = Visibility.Visible;

            new Thread(new ThreadStart(() => {
                // 抽奖动画效果
                for (int i = 0; i < 5; i++)
                {
                    int rand = random.Next(1, PeopleCount + 1);
                    while (rands.Contains(rand))
                    {
                        rand = random.Next(1, PeopleCount + 1);
                    }
                    rands.Add(rand);
                    if (rands.Count >= PeopleCount) rands = new List<int>();
                    Application.Current.Dispatcher.Invoke(() => {
                        if (Names.Count != 0)
                        {
                            LabelOutput.Content = Names[rand - 1];
                        }
                        else
                        {
                            LabelOutput.Content = rand.ToString();
                        }
                    });

                    Thread.Sleep(150);
                }

                rands = new List<int>();
                Application.Current.Dispatcher.Invoke(() => {
                    for (int i = 0; i < TotalCount; i++)
                    {
                        int rand = random.Next(1, PeopleCount + 1);
                        while (rands.Contains(rand))
                        {
                            rand = random.Next(1, PeopleCount + 1);
                        }
                        rands.Add(rand);
                        if (rands.Count >= PeopleCount) rands = new List<int>();

                        if (Names.Count != 0)
                        {
                            outputs.Add(Names[rand - 1]);
                            outputString += Names[rand - 1] + Environment.NewLine;
                        }
                        else
                        {
                            outputs.Add(rand.ToString());
                            outputString += rand.ToString() + Environment.NewLine;
                        }
                    }
                    LabelOutput.Content = outputString.ToString().Trim();
                    BorderBtnRandCover.Visibility = Visibility.Collapsed;
                });
            })).Start();
        }

        private void BorderBtnRand_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // 按钮按下效果，可选实现
        }

        private void BorderBtnMinus_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // 按钮按下效果
        }

        private void BorderBtnAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // 按钮按下效果
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadNames();
            LabelNumberCount.Text = TotalCount.ToString();
        }

        private void BorderBtnHelp_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // 打开名单输入窗口
            NamesInputWindow namesInputWindow = new NamesInputWindow(this);
            namesInputWindow.ShowDialog();
            LoadNames(); // 重新加载名单
            LabelNumberCount.Text = TotalCount.ToString(); // 重新设置抽取人数显示
        }

        public void LoadNames()
        {
            Names = new List<string>();
            if (File.Exists("Names.txt"))
            {
                string[] fileNames = File.ReadAllLines("Names.txt");

                // 过滤空行
                foreach (string str in fileNames)
                {
                    string s = str.Trim();
                    if (!string.IsNullOrEmpty(s))
                    {
                        Names.Add(s);
                    }
                }

                PeopleCount = Names.Count;
                if (PeopleCount == 0)
                {
                    PeopleCount = 50; // 如果没有导入名单，则使用默认人数
                    TextBlockPeopleCount.Text = "点击此处以导入名单";
                }
                else
                {
                    TextBlockPeopleCount.Text = $"共{PeopleCount}人";
                }
                
                // 重置抽取人数，确保不超过总人数
                if (TotalCount > PeopleCount)
                {
                    TotalCount = 1;
                }
            }
            else
            {
                PeopleCount = 50; // 如果没有名单文件，则使用默认人数
                TextBlockPeopleCount.Text = "点击此处以导入名单";
            }
            
            // 更新抽取人数显示
            if (LabelNumberCount != null)
            {
                LabelNumberCount.Text = TotalCount.ToString();
            }
        }

        private void BtnClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}