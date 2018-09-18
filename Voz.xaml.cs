using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Speech.Recognition;

using System.Windows.Media.Animation;

namespace WpfApp4
{
    /// <summary>
    /// Lógica de interacción para Voz.xaml
    /// </summary>
    public partial class Voz : Window
    {
        string pathDirectory = Environment.CurrentDirectory.Replace("\\bin\\Debug", "");
        private SpeechRecognitionEngine reconocedor = new SpeechRecognitionEngine();
        static Storyboard sbBailar;
        static Storyboard sbIzq;
        static Storyboard sbDer;
        static Storyboard fumar;
     
        static Storyboard parpadeo;
        static Storyboard plantas;
        static Storyboard cabeza;

        static MainWindow principal;
        String palabras;

        public Voz(MainWindow p)
        {
            InitializeComponent();
            InicializarAnimaciones();
            //reconocedor.SetInputToDefaultAudioDevice();

            string fullPath_sonidoFondo = pathDirectory + "\\fondoVoz.wav";
            principal = p;
        }
        public void Bailar(object sender, MouseButtonEventArgs e)
        {
            sbBailar.Begin();
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
            plantas.Begin(this);
        }

        public void InicializarAnimaciones()
        {
            sbBailar = (Storyboard)this.Resources["Bailar"];
            sbIzq = (Storyboard)this.Resources["Guiño_Izquierdo"];
            sbDer = (Storyboard)this.Resources["Guiño_Derecho"];
            parpadeo = (Storyboard)this.Resources["Parpadear"];
            plantas = (Storyboard)this.Resources["Planta"];
            fumar = (Storyboard)this.Resources["Glencho"];
            cabeza = (Storyboard)this.Resources["MovimientoCabeza"];

        }

        private void btnMicrofono_Click (object sender, EventArgs e)
        {
            reconocedor.SetInputToDefaultAudioDevice();
            btnMicrofono.IsEnabled = false;
            reconocedor.LoadGrammar(new DictationGrammar());
            reconocedor.SpeechRecognized +=reconocedor_SpeechRecognized;
            reconocedor.RecognizeAsync(RecognizeMode.Multiple);
        }

        void reconocedor_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            palabras=e.Result.Text;
            foreach (RecognizedWordUnit word in e.Result.Words)
            {
                if (word.Text == "fumar")
                {
                    fumar.Begin();
                }
                else if (word.Text == "cabeza")
                {
                    cabeza.Begin();
                }

                else if (word.Text == "cosquillas")
                {
                    plantas.Begin();
                }
                else if (word.Text == "guiñar")
                {
                    Random ran = new Random();
                    ran.Next(1);
                    if (ran.Equals(0))
                    {
                        sbDer.Begin();
                    }
                    else if (ran.Equals(1))
                    {
                        sbIzq.Begin();
                    }
                }
                else if (word.Text == "bailar")
                {
                    sbBailar.Begin();
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Hide();
            principal.Show();
            principal.GetTemporizador().Start();
            principal.GetMusica().Play();
        }
    }
}
