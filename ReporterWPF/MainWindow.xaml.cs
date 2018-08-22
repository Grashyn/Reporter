﻿using ReporterWPF.ViewModels;
using System.Windows;

namespace ReporterWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = MainVM.Instance;
        }
    }
}
