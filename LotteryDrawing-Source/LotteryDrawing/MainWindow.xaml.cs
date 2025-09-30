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

        public int PeopleCount = 50; // 设置总人数为50
        public List<string> Names = new List<string>();

        private void BorderBtnRand_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // 执行单次抽奖逻辑
            Random random = new Random();
            string outputString = "";

            BorderBtnRandCover.Visibility = Visibility.Visible;

            new Thread(new ThreadStart(() => {
                // 抽奖动画效果
                for (int i = 0; i < 15; i++) // 增加动画效果的循环次数
                {
                    int rand = random.Next(1, PeopleCount + 1);
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

                    Thread.Sleep(80); // 调整动画速度
                }

                // 最终结果
                int finalRand = random.Next(1, PeopleCount + 1);
                Application.Current.Dispatcher.Invoke(() => {
                    if (Names.Count != 0)
                    {
                        outputString = Names[finalRand - 1];
                    }
                    else
                    {
                        outputString = finalRand.ToString();
                    }
                    LabelOutput.Content = outputString;
                    BorderBtnRandCover.Visibility = Visibility.Collapsed;
                });
            })).Start();
        }

        private void BorderBtnRand_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // 按钮按下效果，可选实现
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadNames();
        }

        private void BorderBtnHelp_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // 打开名单输入窗口
            NamesInputWindow namesInputWindow = new NamesInputWindow(this);
            namesInputWindow.ShowDialog();
            LoadNames(); // 重新加载名单
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
            }
            else
            {
                PeopleCount = 50; // 如果没有名单文件，则使用默认人数
                TextBlockPeopleCount.Text = "点击此处以导入名单";
            }
        }

        private void BtnClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}