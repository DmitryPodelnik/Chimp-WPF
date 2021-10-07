﻿using First_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace First_App.Views
{
    /// <summary>
    /// Interaction logic for LeftSideMenu.xaml
    /// </summary>
    public partial class LeftSideMenu : UserControl
    {
        public LeftSideMenu()
        {
            InitializeComponent();

            DataContext = new LeftSideMenuViewModel();
        }
    }
}
