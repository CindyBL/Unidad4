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
using Farmacia.BIZ;
using Farmacia.COMMON.Interfaces;
using Farmacia.DAL;
using LiteDB;
using Farmacia.COMMON.Entidades;

namespace Farmacia.GUI
{
    /// <summary>
    /// Lógica de interacción para Cliente.xaml
    /// </summary>
    public partial class Cliente : Window
    {
        enum accion
        {
            Nuevo,
            Editar
        }

        IManejadorClientes manejadorClientes;
        accion accionClientes;
        public Cliente()
        {
            InitializeComponent();
            manejadorClientes = new ManejadorDeClientes(new RepositorioClientes());
            HabilitarCajas(false);
            HabilitarBotones(true);
            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            dtgCliente.ItemsSource = null;
            dtgCliente.ItemsSource = manejadorClientes.Listar;
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
            accionClientes = accion.Nuevo;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
           Clientes cli = dtgCliente.SelectedItem as Clientes;
            if (cli != null)
            {
                HabilitarCajas(true);
                txbNombre.Text = cli.Nombre;
                txbRFC.Text = cli.RFC;
                txbCorreo.Text = cli.Correo;
                txbApellido.Text = cli.Apellido;
                txbDireccion.Text = cli.Direccion;
                txbTelefono.Text = cli.Telefono;
                txbClientesId.Text = cli.Id;
                accionClientes = accion.Editar;
                HabilitarBotones(false);
            }
            else
            {
                MessageBox.Show("Seleccione el cliente que desea editar", "Clientes", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accionClientes == accion.Nuevo)
            {
                Clientes cli = new Clientes()
                {
                    Nombre = txbNombre.Text,
                    RFC=txbRFC.Text,
                    Correo=txbCorreo.Text,
                    Apellido = txbApellido.Text,
                    Direccion=txbDireccion.Text,
                    Telefono=txbTelefono.Text,
                    
                };
                if (manejadorClientes.Agregar(cli))
                {
                    MessageBox.Show("Cliente agregado correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("El cliente no se pudo agregar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Clientes cli = dtgCliente.SelectedItem as Clientes;
                cli.Nombre = txbNombre.Text;
                cli.RFC = txbRFC.Text;
                cli.Correo = txbCorreo.Text;
                cli.Apellido = txbApellido.Text;
                cli.Direccion = txbDireccion.Text;
                cli.Telefono = txbTelefono.Text;
                if (manejadorClientes.Modificar(cli))
                {
                    MessageBox.Show("Cliente modificado correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("El cliente no se pudo actualizar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(false);
            HabilitarBotones(true);
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Clientes cli = dtgCliente.SelectedItem as Clientes;
            if (cli != null)
            {
                if (MessageBox.Show("Realmente deseas eliminar a este Cliente?", "Farmacia", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorClientes.Eliminar(cli.Id))
                    {
                        MessageBox.Show("El cliente ha sido eliminado correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTabla();
                    }
                    else
                    {
                        MessageBox.Show("El cliente no se pudo eliminar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
        }

    }
}
