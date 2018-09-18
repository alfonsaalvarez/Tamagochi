using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace WpfApp4
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Storyboard sbIzq;
        static Storyboard sbDer;
        static Storyboard comer;
        static Storyboard paso1;
        static Storyboard dormir;
        static Storyboard parpadeo;
        static Storyboard plantas;
        static Storyboard paso2;
        static Storyboard paso3;
        static Storyboard muerte;

        MediaPlayer fondoMain;
        MediaPlayer fondoBoton;


        Avatar Groot= new Avatar(100,100,100);
        DispatcherTimer temporizador;
        int intervalo;
        static string pathDirectory = Environment.CurrentDirectory.Replace("\\bin\\Debug", "");



        public MainWindow()
        {
            InitializeComponent();
            ReadDB();
            InicializarAnimaciones();
            intervalo = 1000;
            temporizador = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(intervalo)
            };
            temporizador.Tick += TickConsumoHandler;
            temporizador.Start();
            string musicaPrincipal = pathDirectory + "\\MusicaJackson.mp3";
            fondoMain = new MediaPlayer();
            fondoMain.Open(new Uri(musicaPrincipal));
            fondoMain.Volume = 0.30;
            fondoMain.Play();
        }
        
        public void ModificarAvatar()
        {
            Groot.apetito = 100;
            Groot.diversion = 100;
            Groot.energia = 100;                            
        }
        public void GuiñoOjoIzquierdo(object sender, MouseButtonEventArgs e)
        { 
            sbIzq.Begin();
        }

        public void GuiñoOjoDerecho(object sender, MouseButtonEventArgs e)
        {
            sbDer.Begin();
        }
        private void Parpadear(object sender, EventArgs e)
        {
            parpadeo.Begin(this);
        }

        private void AnimacionCuerpo(object sender, EventArgs e)
        {
            string musicaBoton = pathDirectory + "\\bebe-groot.mp3";
            fondoBoton = new MediaPlayer();
            fondoBoton.Open(new Uri(musicaBoton));
            fondoBoton.Volume = 0.50;
            fondoBoton.Play();

            plantas.Begin(this);
        }

        private void StoryboardPaso1_Completed(object sender, EventArgs e)
        {
            paso1.Stop();
            paso2.Begin(this);
        }

        private void StoryboardPaso2_Completed(object sender, EventArgs e)
        {
            paso2.Stop();
            paso3.Begin(this);
        }

        private void Muerte_completada(object sender, EventArgs e)
        {
            Muerte m = new Muerte(this);
            m.Show();
            Hide();
        }

        public void InicializarAnimaciones()
        {
            sbIzq = (Storyboard)this.window.Resources["Guiño_Izquierdo"];
            paso2 = (Storyboard)this.Resources["Paso2"];
            paso3 = (Storyboard)this.Resources["Paso3"];
            sbDer = (Storyboard)this.window.Resources["Guiño_Derecho"];
            parpadeo = (Storyboard)this.Resources["Parpadear"];
            paso1 = (Storyboard)this.Resources["Paso1"];
            comer = (Storyboard)this.Resources["Comer"];
            plantas = (Storyboard)this.Resources["Planta"];
            muerte = (Storyboard)this.Resources["Muerte"];
            dormir = (Storyboard)this.Resources["Dormir"];
        }

        public void PararAnimaciones()
        {
            sbIzq.Stop();
            sbDer.Stop();
            comer.Stop();
            paso1.Stop();
            dormir.Stop();
            plantas.Stop();
            paso2.Stop();
            paso3.Stop();
            muerte.Stop();
        }

        public void TickConsumoHandler(object sender, EventArgs e)
        {
            Double aux;

            if (Groot.diversion < 60 || Groot.apetito < 60 || Groot.energia < 60)
            {
                if (Groot.diversion == 0 || Groot.apetito == 0 || Groot.energia == 0)
                {
                    MuerteGroot();
                    temporizador.Stop();
                }
                else { 
                    Groot.energia = (aux = Groot.energia - this.AleatorizarRapido(9)) < 0 ? 0 : aux;
                    Groot.apetito = (aux = Groot.apetito - this.AleatorizarRapido(6)) < 0 ? 0 : aux;
                    Groot.diversion = (aux = Groot.diversion - this.AleatorizarRapido(5)) < 0 ? 0 : aux; }
            }else  {
                Groot.energia = (aux = Groot.energia - this.Aleatorizar(3)) < 0 ? 0 : aux;
                Groot.apetito = (aux = Groot.apetito - this.Aleatorizar(2)) < 0 ? 0 : aux;
                Groot.diversion = (aux = Groot.diversion - this.Aleatorizar(1)) < 0 ? 0 : aux;

            }
                this.PBApetito.Value = Groot.apetito;
                this.PBEnergia.Value = Groot.energia;
                this.PBDiversion.Value = Groot.diversion;
   
        }

        public int Aleatorizar(int max)
        {
            Random generadorAleatorio = new Random();
            return 2 + generadorAleatorio.Next(max);
        }

        public int AleatorizarRapido(int max)
        {
            Random generadorAleatorio = new Random();
            return 5 + generadorAleatorio.Next(max);
        }

        public void BApetito_Click(object sender, RoutedEventArgs e)
         {

            intervalo -= 10;
            intervalo = intervalo0(intervalo);
            temporizador.Interval = TimeSpan.FromMilliseconds(intervalo);
            Groot.apetito += Aleatorizar(30);

            paso1.Stop(this);
            paso2.Stop(this);
            paso3.Stop(this);
            dormir.Stop(this);

            if (Groot.apetito < 100)
            {
                this.PBApetito.Value = Groot.apetito;
                comer.Begin();
            }
            else{
                Groot.apetito = 100;
                this.PBApetito.Value = Groot.apetito;
                comer.Begin();
            }

        }
     

     


        public void BDiversion_Click(object sender, RoutedEventArgs e) {
           
            intervalo -= 10;
            intervalo = intervalo0(intervalo);
            temporizador.Interval = TimeSpan.FromMilliseconds(intervalo);
            Groot.diversion += 20 + Aleatorizar(20);

            PararAnimaciones();
            comer.Stop(this);
          
            dormir.Stop(this);
            if (Groot.diversion < 100)
            {
                this.PBDiversion.Value = Groot.diversion;
                paso1.Begin();
            }
            else
            {
                Groot.diversion = 100;
                this.PBDiversion.Value = Groot.diversion;
                paso1.Begin();
            }
        }

        public int intervalo0(int intervalo)
        {
            if (intervalo <200)
            {
                intervalo = 1000;
            }
            return intervalo;
        }

         public void BEnergia_Click(object sender, RoutedEventArgs e)
        {
            intervalo -= 10;
            intervalo = intervalo0(intervalo);
            temporizador.Interval = TimeSpan.FromMilliseconds(intervalo);
            Groot.energia += Aleatorizar(30);
            PararAnimaciones();
            comer.Stop(this);
            paso1.Stop(this);
            paso2.Stop(this);
            paso3.Stop(this);
            if (Groot.energia < 100)
            {
                this.PBEnergia.Value = Groot.energia;
                dormir.Begin();
            }
            else
            {
                Groot.energia = 100;
                this.PBEnergia.Value = Groot.energia;
                PararAnimaciones();
                dormir.Begin();
            }
        }
        public void MuerteGroot()
        {
            BEnergia.IsEnabled = false;
            BApetito.IsEnabled = false;
            BDiversion.IsEnabled = false;
            BMicro.IsEnabled = false;
            BMiniJuego.IsEnabled = false;
            muerte.Begin();
            fondoMain.Stop();
        }
    
        private void ReadDB()
        {
            // BBDD Access
            OleDbConnection myconect;
            OleDbCommand mycommand;

            myconect = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + pathDirectory + "\\Atributos.accdb");
            mycommand = myconect.CreateCommand();
            mycommand.CommandText = "SELECT * FROM Atributos WHERE Id=1";
            mycommand.CommandType = CommandType.Text;

            myconect.Open();
            OleDbDataReader DBreader = mycommand.ExecuteReader();
            while (DBreader.Read())
            {
                Groot.energia = Convert.ToDouble(DBreader["Energia"].ToString());
                Groot.apetito = Convert.ToDouble(DBreader["Apetito"].ToString());
                Groot.diversion = Convert.ToDouble(DBreader["Diversion"].ToString());
                
            }
            myconect.Close();
        }
        private void WriteDB()
        {
            OleDbConnection myconect;
            OleDbCommand mycommand;

            myconect = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + pathDirectory + "\\Atributos.accdb");
            mycommand = myconect.CreateCommand();

            mycommand.CommandText = "UPDATE Atributos SET Energia='" + Groot.energia + "', Apetito='" + Groot.apetito + "', Diversion='" + Groot.diversion + "' WHERE Id=1;";
            mycommand.CommandType = CommandType.Text;

            myconect.Open();
            mycommand.ExecuteNonQuery();
            myconect.Close();
        }

        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            fondoMain.Stop();
            WriteDB();
        }

        public DispatcherTimer GetTemporizador()
        {
            return temporizador;
        }
        public MediaPlayer GetMusica()
        {
            return fondoMain;
        }

        public void BMicro_click(object sender, RoutedEventArgs e)
        {
            temporizador.Stop();
            Voz v = new Voz(this);
            Hide();
            v.Show();
            fondoMain.Pause();
        }

        private void btnMiniJuego_Click(object sender, RoutedEventArgs e)
        {
            Minijuego mini = new Minijuego(this);
            Hide();
            temporizador.Stop();
            LaunchCommandLineApp();
            mini.Show();
            fondoMain.Pause();
        }

        static void LaunchCommandLineApp()
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.UseShellExecute = true;
            info.FileName = "Minijuego.exe";
            info.WorkingDirectory = pathDirectory;
            Process.Start(info);
        }
    }
}
