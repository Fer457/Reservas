using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace Reserva.Pages
{
    /// <summary>
    /// Lógica de interacción para Reservas.xaml
    /// </summary>
    public partial class Reservas : Page
    {
        public Reservas()
        {
            InitializeComponent();
        }
        static readonly HttpClient cliente = new HttpClient();
        int pos = 0;

        static async Task<List<Rsrv>> GetReservas(string path)
        {

            HttpResponseMessage msg = await cliente.GetAsync(path);

            List<Rsrv> reserva = null;


            if (msg.IsSuccessStatusCode)
            {
                var salida = await msg.Content.ReadAsStringAsync();
                reserva = JsonConvert.DeserializeObject<List<Rsrv>>(salida);
            }

            return reserva;
        }

        private async Task<Rsrv> PostReserva(Rsrv reserva, String path)
        {
            Rsrv rsv = null;
            var json = JsonConvert.SerializeObject(reserva);
            var cabeceras = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage msg = await cliente.PostAsync(path, cabeceras);
            if (msg.IsSuccessStatusCode)
            {
                var salida = await msg.Content.ReadAsStringAsync();
                rsv = JsonConvert.DeserializeObject<Rsrv>(salida);

            }

            CargarDatos();
            return rsv;
        }

        private async void CargarDatos()
        {
            List<Rsrv> reservas = null;
            try
            {
                reservas = await GetReservas("http://localhost:3000/reservas");
            }
            catch
            {

            }
            grid.ItemsSource = reservas;
            grid.Items.Refresh();
        }

        private async Task<Boolean> DelReserva(String path)
        {
            Boolean ProReturn = false;

            HttpResponseMessage msg = await cliente.DeleteAsync(path);

            if (msg.IsSuccessStatusCode)
            {
                ProReturn = true;

            }
            CargarDatos();
            return ProReturn;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(dtfecha.Text.ToString()))
            {
                MessageBox.Show("Hay campos sin rellenar");
                return;
            }

            Rsrv reserva = new Rsrv(
                dtfecha.Text.ToString(),
                cbaula.Text.ToString(),
                cbprof.Text.ToString());

            await PostReserva(reserva, "http://localhost:3000/reservas");

            dtfecha.Text = "";
            cbaula.SelectedIndex = 0;
            cbprof.SelectedIndex = 0;

        }

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            Rsrv fila = grid.SelectedItem as Rsrv;
            if (fila != null)
            {
                pos = fila.id;
            }
        }

        private async void Button_Click_1Async(object sender, RoutedEventArgs e)
        {
            if (pos == 0)
            {
                MessageBox.Show("Selecciona un aula");
            }
            await DelReserva("http://localhost:3000/reservas/" + pos);
            pos = 0;
        }
    }
}
