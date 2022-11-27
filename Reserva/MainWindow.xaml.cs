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

namespace Reserva
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnProfesores_Click(object sender, RoutedEventArgs e)
        {
            frameP.NavigationService.Navigate(new Pages.PaginaProf());
        }

        private void btnAulas_Click(object sender, RoutedEventArgs e)
        {
            frameP.NavigationService.Navigate(new Pages.Aulas());
        }

        private void btnReservas_Click(object sender, RoutedEventArgs e)
        {
            frameP.NavigationService.Navigate(new Pages.Reservas());
        }
    }
}
