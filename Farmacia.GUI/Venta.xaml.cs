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
using System;

namespace Farmacia.GUI
{
    /// <summary>
    /// Lógica de interacción para Venta.xaml
    /// </summary>
    public partial class Venta : Window
    {
        enum accion
        {
            Nuevo,
            Editar
        }
        IManejadorTicket manejadorTicket;
        IManejadorClientes manejadorClientes;
        IManejadorProducto manejadorProducto;
        IManejadorEmpleado manejadorEmpleado;
        Ticket ticket;
        accion accionTicket;
        public Venta()
        {
            InitializeComponent();
            manejadorTicket = new ManejadorDeTicket(new RepositorioDeTicket());
            manejadorClientes = new ManejadorDeClientes(new RepositorioClientes());
            manejadorProducto = new ManejadorDeProducto(new RepositorioDeProducto());
            manejadorEmpleado = new ManejadorDeEmpleado(new RepositorioEmpleado());
            HabilitarCajas(false);
            HabilitarBotones(true);
            actualizarTabla();
        }

        private void actualizarTabla()
        {
            dtgTicket.ItemsSource = null;
            dtgTicket.ItemsSource = manejadorTicket.Listar;
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow elegir = new MainWindow();
            elegir.Owner = this;
            elegir.Show();
        }
        private void HabilitarCajas(bool habilitadas)
        {
            cmbNombre.ItemsSource = null;
            cmbNombre.ItemsSource = manejadorClientes.Listar;
            cmbProducto.ItemsSource = null;
            cmbProducto.ItemsSource = manejadorProducto.Listar;
            cmbEmpleado.ItemsSource = null;
            cmbEmpleado.ItemsSource = manejadorEmpleado.Listar;
            txtCantidad.Clear();
            cmbNombre.IsEnabled = habilitadas;
            cmbProducto.IsEnabled = habilitadas;
            cmbEmpleado.IsEnabled = habilitadas;
            txtCantidad.IsEnabled = habilitadas;
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
            accionTicket = accion.Nuevo;

        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            Ticket pro = dtgTicket.SelectedItem as Ticket;
            if (pro != null)
            {
                HabilitarCajas(true);
                cmbNombre.Text = pro.Cliente;
                cmbProducto.Text = pro.Productos;
                cmbEmpleado.Text = pro.Encargado;
                txtCantidad.Text = pro.cantidad;
                dtpFecha.SelectedDate = pro.FechaHora;
                accionTicket = accion.Editar;
                HabilitarBotones(false);
            }
            else
            {
                MessageBox.Show("Seleccione la venta que desea editar", "Productos", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }

        private void btnAddProducto_Click(object sender, RoutedEventArgs e)
        {
            Producto p = cmbProducto.SelectedItem as Producto;
            if (p != null)
            {
                //ticket.Productos(p);
                //actualizarTabla();
            }
            //actualizarListaDeProductos();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accionTicket == accion.Nuevo)
            {
                Ticket pro = new Ticket()
                {
                   Cliente=cmbNombre.Text,
                   FechaHora=dtpFecha.SelectedDate.Value,
                   Productos=cmbProducto.Text,
                   Encargado=cmbEmpleado.Text,
                   cantidad=txtCantidad.Text,
                   Total=txtCantidad.Text,
                };
                if (manejadorTicket.Agregar(pro))
                {
                    MessageBox.Show("La venta se ha realizado exitosamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
                    actualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("La venta no se ha podido realizar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Ticket pro = dtgTicket.SelectedItem as Ticket;
                //pro.Cliente = cmbNombre.Text;
                //pro.FechaHora = dtpFecha.SelectedDate.Value;
                //pro.Productos = cmbProducto.Text;
                //pro.Encargado = cmbEmpleado.Text;
                pro.cantidad = txtCantidad.Text;
                pro.Total = txtCantidad.Text;
                if (manejadorTicket.Modificar(pro))
                {
                    MessageBox.Show("Producto modificado correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
                    actualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("El producto no se pudo actualizar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
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
            Ticket pro = dtgTicket.SelectedItem as Ticket;
            if (pro != null)
            {
                if (MessageBox.Show("Realmente deseas eliminar esta venta?", "Farmacia", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorTicket.Eliminar(pro.Id))
                    {
                        MessageBox.Show("La venta ha sido eliminado correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
                        actualizarTabla();
                    }
                    else
                    {
                        MessageBox.Show("La venta no se pudo eliminar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
        }
    }
}
