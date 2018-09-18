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
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// Lógica de interacción para Muerte.xaml
    /// </summary>
    
    public partial class Muerte : Window
    {
        MainWindow principal;

        public Muerte(MainWindow p)
        {
            InitializeComponent();
            principal = p;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            principal.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            principal.ModificarAvatar();
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            principal.ModificarAvatar();
            Close();
        }
    }
}
