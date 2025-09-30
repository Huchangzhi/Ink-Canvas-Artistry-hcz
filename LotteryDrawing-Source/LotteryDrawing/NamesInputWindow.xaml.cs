using System;
using System.IO;
using System.Windows;

namespace LotteryDrawing
{
    /// <summary>
    /// Interaction logic for NamesInputWindow.xaml
    /// </summary>
    public partial class NamesInputWindow : Window
    {
        private MainWindow _mainWindow;
        string originText = "";

        public NamesInputWindow()
        {
            InitializeComponent();
        }

        public NamesInputWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists("Names.txt"))
            {
                TextBoxNames.Text = File.ReadAllText("Names.txt");
                originText = TextBoxNames.Text;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (originText != TextBoxNames.Text)
            {
                var result = MessageBox.Show("是否保存？", "名单导入", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    File.WriteAllText("Names.txt", TextBoxNames.Text);
                    _mainWindow?.LoadNames(); // 通知主窗口重新加载名单
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonImport_Click(object sender, RoutedEventArgs e)
        {
            // 这里可以添加从Excel导入的功能
            MessageBox.Show("Excel导入功能待实现，目前请直接粘贴名单文本。", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}