# 抽奖软件 (Lottery Drawing App)

## 概述
这是一个独立的抽奖软件，基于Ink Canvas Artistry项目中的抽奖功能开发。

## 功能特点
- 单次抽奖功能（仅保留单次抽奖，移除了多选功能）
- 支持导入名单（每行一个姓名）
- 抽奖动画效果
- 总人数设置为50人（可导入名单时自动更新人数）
- 简洁直观的用户界面

## 使用方法
1. 运行 LotteryDrawing.exe
2. 点击右下角"点击此处以导入名单"按钮导入参与抽奖的名单
3. 点击"开始抽奖"按钮进行抽奖

## 技术规格
- 平台：.NET 6 Windows
- UI框架：WPF
- 源码语言：C#

## 构建说明
要构建此项目，请执行以下命令：
```
dotnet build -c Release
```

要发布为独立的可执行文件，请执行：
```
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o ./publish
```

## 源码结构
- MainWindow.xaml - 主界面
- MainWindow.xaml.cs - 主界面逻辑
- NamesInputWindow.xaml - 名单输入界面
- NamesInputWindow.xaml.cs - 名单输入逻辑
- App.xaml - 应用程序定义
- AssemblyInfo.cs - 程序集信息
- LotteryDrawing.csproj - 项目文件