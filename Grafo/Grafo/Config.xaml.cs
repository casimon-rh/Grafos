using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace Grafo
{
    public partial class Config : MetroWindow
    {
        Dispatcher dis;
        public RutinaConfiguracion rutina;
        public Config()
        {
            InitializeComponent();
            Loaded += Config_Loaded;
            dis = this.Dispatcher;
        }

        void Config_Loaded(object sender, RoutedEventArgs e)
        {
            rutina = Resources["Rutina"] as RutinaConfiguracion;
        }

        private void Relations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public delegate void Confirma(MessageDialogResult res, CancelEventArgs e);
        async void pregunta(CancelEventArgs e)
        {
            MessageDialogResult mdr = await this.ShowMessageAsync("Grafo", "¿Ésta es la configuracion deseada?", MessageDialogStyle.AffirmativeAndNegative).ConfigureAwait(false);
            Confirma evento = new Confirma(efectua);
            try
            {
                dis.BeginInvoke(evento, mdr, e);
            }
            catch (Exception ex)
            {
                this.ShowMessageAsync("Grafo", "Ocurrió un error:\n" + ex);
            }
        }
        public void efectua(MessageDialogResult res, CancelEventArgs e)
        {
            if (res == MessageDialogResult.Affirmative)
                this.Hide();
            else
                e.Cancel = true;
            
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            pregunta(e);
        }

        private void Vertices_LostFocus(object sender, RoutedEventArgs e)
        {
            rutina.Num =Convert.ToInt32((sender as NumericUpDown).Value??0);
        }
    }
}
