using First_App.Models.RegistryData;
using First_App.Navigation;
using First_App.Services.Authentication;
using First_App.ViewModels;
using First_App.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace First_App
{
    /// <summary>
    ///     Interaction logic for Chimp.xaml
    /// </summary>
    public partial class Chimp : Window
    {
        public Chimp()
        {
            InitializeComponent();

            ChimpViewModel vm = new();

            DataContext = vm;

            Closing += vm.OnWindowClosing;
        }

    }
}
