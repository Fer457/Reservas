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
    public partial class PaginaProf : Page
    {
        public PaginaProf()
        {
            InitializeComponent();
            CargarDatos();
        }

        static readonly HttpClient cliente = new HttpClient();
        int pos = 0;


        static async Task<List<Profesor>> GetProfesor(String path)
        {

            HttpResponseMessage msg = await cliente.GetAsync(path);

            List<Profesor> prof = null;


            if (msg.IsSuccessStatusCode)
            {
                var salida = await msg.Content.ReadAsStringAsync();

                prof = JsonConvert.DeserializeObject<List<Profesor>>(salida);
            }
            return prof;
        }

        private async Task<Profesor> PostProfesor(Profesor profesor, String path)
        {
            Profesor prof = null;
            var json = JsonConvert.SerializeObject(profesor);
            var cabeceras = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage msg = await cliente.PostAsync(path, cabeceras);
            if (msg.IsSuccessStatusCode)
            {
                var salida = await msg.Content.ReadAsStringAsync();
                prof = JsonConvert.DeserializeObject<Profesor>(salida);

            }
            
            CargarDatos();
            return prof;
        }

        private async void CargarDatos()
        {
            List<Profesor> profesores = null;
            try
            {
                profesores = await GetProfesor("http://localhost:3000/profesores");
            }
            catch
            {
                MessageBox.Show("No hay conexión..");//muestra un mensaje
                Environment.Exit(1);//cierra el progra
            }
            grid.ItemsSource = profesores;
            grid.Items.Refresh();
        }

        private async Task<Boolean> DelProf(String path)
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
                string.IsNullOrWhiteSpace(tbtlf.Text) || 
                string.IsNullOrWhiteSpace(tbapellidos.Text) || 
                string.IsNullOrWhiteSpace(tbesp.Text) ||
                string.IsNullOrWhiteSpace(dtfecha.Text))
            {
                MessageBox.Show("Hay campos sin rellenar");
                return;
            }

            Profesor profesor = new Profesor(
                tbnombre.Text.ToString(),
                tbapellidos.Text.ToString(),
                tbesp.Text.ToString(),
                tbtlf.Text.ToString(),
                cbsexo.Text.ToString(),
                dtfecha.Text.ToString());

            await PostProfesor(profesor, "http://localhost:3000/profesores");

            tbnombre.Text = "";
            tbapellidos.Text = "";
            tbesp.Text = "";
            tbtlf.Text = "";
            cbsexo.SelectedIndex= 0;
            dtfecha.Text = "";

        }

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            Profesor fila = grid.SelectedItem as Profesor;
            if (fila != null)
            {
                pos = fila.id;
            }
        }

        private async void Button_Click_1Async(object sender, RoutedEventArgs e)
        {
            if (pos == 0)
            {
                MessageBox.Show("Selecciona un profesor");
                return;
            }
                await DelProf("http://localhost:3000/profesores/" + pos);
                pos = 0;
        }
    }
}