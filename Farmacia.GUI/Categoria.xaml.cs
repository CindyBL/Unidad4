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

namespace Farmacia.GUI
{
    /// <summary>
    /// Lógica de interacción para Categoria.xaml
    /// </summary>
    public partial class Categoria : Window
    {
        public Categoria()
        {
            InitializeComponent();
            HabilitarCajas(false);
            HabilitarBotones(true);
        }

        private void HabilitarCajas(bool habilitadas)
        {
            txbNombreCategoria.Clear();
            txbNombreCategoria.IsEnabled = habilitadas;
        }

        private void HabilitarBotones(bool habilitados)
        {
            btnNuevo.IsEnabled = habilitados;
            btnEliminar.IsEnabled = habilitados;
            btnEditar.IsEnabled = habilitados;
            btnGuardar.IsEnabled = !habilitados;
            btnCancelar.IsEnabled = !habilitados;
        }

        private void txbNombreCategoria_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(true);
            HabilitarBotones(false);
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Productos elegir = new Productos();
            elegir.Owner = this;
            elegir.Show();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
