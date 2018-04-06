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
    /// Lógica de interacción para NuevoProducto.xaml
    /// </summary>
    public partial class NuevoProducto : Window
    {
        public NuevoProducto()
        {
            InitializeComponent();
            HabilitarCajas(false);
            HabilitarBotones(true);
        }

        private void HabilitarCajas(bool habilitadas)
        {
            txbNombre.Clear();
            txbDescripcion.Clear();
            txbPrecioCompra.Clear();
            txbPrecioVenta.Clear();
            txbPresentacion.Clear();
            txbNombre.IsEnabled = habilitadas;
            txbDescripcion.IsEnabled = habilitadas;
            txbPrecioCompra.IsEnabled = habilitadas;
            txbPrecioVenta.IsEnabled = habilitadas;
            txbPresentacion.IsEnabled = habilitadas;
            cbxCategoria.IsEnabled = habilitadas;
        }

        private void HabilitarBotones(bool habilitados)
        {
            btnNuevo.IsEnabled = habilitados;
            btnEditar.IsEnabled = habilitados;
            btnEliminar.IsEnabled = habilitados;
            btnGuardar.IsEnabled = !habilitados;
            btnCancelar.IsEnabled = !habilitados;
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
    }
}
