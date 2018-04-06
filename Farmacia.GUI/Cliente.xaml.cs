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
    /// Lógica de interacción para Cliente.xaml
    /// </summary>
    public partial class Cliente : Window
    {
        public Cliente()
        {
            InitializeComponent();
            HabilitarCajas(false);
            HabilitarBotones(true);
        }
        private void HabilitarCajas(bool habilitadas)
        {
            txbNombre.Clear();
            txbApellido.Clear();
            txbDireccion.Clear();
            txbRFC.Clear();
            txbTelefono.Clear();
            txbCorreo.Clear();
            txbNombre.IsEnabled = habilitadas;
            txbApellido.IsEnabled = habilitadas;
            txbDireccion.IsEnabled = habilitadas;
            txbRFC.IsEnabled = habilitadas;
            txbTelefono.IsEnabled = habilitadas;
            txbCorreo.IsEnabled = habilitadas;
        }

        private void HabilitarBotones(bool habilitados)
        {
            btnNuevo.IsEnabled = habilitados;
            btnEditar.IsEnabled = habilitados;
            btnEliminar.IsEnabled = habilitados;
            btnGuardar.IsEnabled = !habilitados;
            btnCancelar.IsEnabled = !habilitados;
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow elegir = new MainWindow();
            elegir.Owner = this;
            elegir.Show();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(true);
            HabilitarBotones(false);
        }
    }
}
