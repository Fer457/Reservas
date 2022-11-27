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
    public partial class Aulas : Page
    {
        public Aulas()
        {
            InitializeComponent();
            CargarDatos();
        }

        static readonly HttpClient cliente = new HttpClient();
        int pos = 0;

        static async Task<List<Aula>> GetAulas(string path)
        {

            HttpResponseMessage msg = await cliente.GetAsync(path);

            List<Aula> aula = null;


            if (msg.IsSuccessStatusCode)
            {
                var salida = await msg.Content.ReadAsStringAsync();
                aula = JsonConvert.DeserializeObject<List<Aula>>(salida);
            }
            
            return aula;
        }

        private async Task<Aula> PostAula(Aula aula, String path)
        {
            Aula au = null;
            var json = JsonConvert.SerializeObject(aula);
            var cabeceras = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage msg = await cliente.PostAsync(path, cabeceras);
            if (msg.IsSuccessStatusCode)
            {
                var salida = await msg.Content.ReadAsStringAsync();
                au = JsonConvert.DeserializeObject<Aula>(salida);

            }

            CargarDatos();
            return au;
        }

        private async void CargarDatos()
        {
            List<Aula> aulas = null;
            try
            {
                aulas = await GetAulas("http://localhost:3000/aulas");
            }
            catch
            {

            }
            grid.ItemsSource = aulas;
            grid.Items.Refresh();
        }

        private async Task<Boolean> DelAula(String path)
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
            if (string.IsNullOrWhiteSpace(tbnombre.Text) ||
                string.IsNullOrWhiteSpace(tbcap.Text) ||
                string.IsNullOrWhiteSpace(tbplanta.Text) ||
                string.IsNullOrWhiteSpace(tbubi.Text))
            {
                MessageBox.Show("Hay campos sin rellenar");
                return;
            }

            Aula aula = new Aula(
                tbnombre.Text.ToString(),
                tbubi.Text.ToString(),
                tbplanta.Text.ToString(),
                tbcap.Text.ToString());

            await PostAula(aula, "http://localhost:3000/aulas");

            tbnombre.Text = "";
            tbcap.Text = "";
            tbplanta.Text = "";
            tbubi.Text = "";

        }

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            Aula fila = grid.SelectedItem as Aula;
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
                await DelAula("http://localhost:3000/aulas/" + pos);
                pos= 0;
        }
    }
}
