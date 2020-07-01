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

namespace Fitness_App
{
    /// <summary>
    /// Interaction logic for SetLocale.xaml
    /// </summary>
    public partial class SetLocale : UserControl
    {
        public SetLocale()
        {
            InitializeComponent();

        }

        private void RussianOnClick(object sender, MouseButtonEventArgs e)
        {
            (Application.Current.Windows[0] as MainWindow).locale.Type = "ru";

        }

        private void EnglishOnClick(object sender, MouseButtonEventArgs e)
        {
            (Application.Current.Windows[0] as MainWindow).locale.Type = "en";
        }
    }
}
