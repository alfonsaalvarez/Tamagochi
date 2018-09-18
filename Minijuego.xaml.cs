using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Lógica de interacción para Minijuego.xaml
    /// </summary>
    public partial class Minijuego : Window
    {
        MainWindow principal;
        MediaPlayer fondoMinijuego;
        static string pathDirectory = Environment.CurrentDirectory.Replace("\\bin\\Debug", "");
        public Minijuego(MainWindow p)
        {
            InitializeComponent();
            principal = p;
            string musicaJuego = pathDirectory + "\\MusicaJuego.mp3";
            fondoMinijuego = new MediaPlayer();
            fondoMinijuego.Open(new Uri(musicaJuego));
            fondoMinijuego.Volume = 0.20;
            fondoMinijuego.Play();
        }

        private void ButtonSi(object sender, RoutedEventArgs e)
        {
            principal.GetTemporizador().Stop();
            LaunchCommandLineApp();
        }
        static void LaunchCommandLineApp()
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.UseShellExecute = true;
            info.FileName = "Minijuego.exe";
            string juegoUnity = pathDirectory;
            info.WorkingDirectory = juegoUnity;
            Process.Start(info);
        }

        private void ButtonNo(object sender, RoutedEventArgs e)
        {
            Hide();
            fondoMinijuego.Stop();
            principal.Show();
            principal.GetTemporizador().Start();
            principal.GetMusica().Play();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Hide();
            fondoMinijuego.Stop();
            principal.Show();
            principal.GetTemporizador().Start();
        }
    }
}
