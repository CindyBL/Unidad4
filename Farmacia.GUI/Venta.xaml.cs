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
        IManejadorTicket ManejadorDeTicket;
        IManejadorClientes ManejadorDeClientes;
        IManejadorProducto ManejadorDeProducto;
        IManejadorEmpleado ManejadorDeEmpleado;
        Ticket ticket;
        accion accionTicket;
        public Venta()
        {
            InitializeComponent();
            ManejadorDeTicket = new ManejadorDeTicket(new RepositorioDeTicket());
            ManejadorDeClientes = new ManejadorDeClientes(new RepositorioClientes());
            ManejadorDeProducto = new ManejadorDeProducto(new RepositorioDeProducto());
            ManejadorDeEmpleado = new ManejadorDeEmpleado(new RepositorioEmpleado());
            HabilitarCajas(false);
            HabilitarBotones(true);
            actualizarTabla();
        }

        private void actializarCombos()
        {
            cmbNombre.ItemsSource = null;
            cmbNombre.ItemsSource = ManejadorDeClientes.Listar;

            cmbProducto.ItemsSource = null;
            cmbProducto.ItemsSource = ManejadorDeProducto.Listar;

            cmbEmpleado.ItemsSource = null;
            cmbEmpleado.ItemsSource = ManejadorDeEmpleado.Listar;
        }

        private void actualizarTabla()
        {
            dtgTicket.ItemsSource = null;
            dtgTicket.ItemsSource = ManejadorDeTicket.Listar;
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
            actializarCombos();
            accionTicket = accion.Nuevo;
            //actualizarListaDeProductos();

        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            Ticket emp = dtgTicket.SelectedItem as Ticket;
            if (emp != null)
            {
                HabilitarCajas(true);
                cmbNombre.SelectedItem= emp.Cliente;
                cmbEmpleado.Text = emp.Encargado;
                txtCantidad.Text = emp.cantidad;
                accionTicket= accion.Editar;
                HabilitarBotones(false);
            }
            else
            {
                MessageBox.Show("Seleccione el empleado que desea editar", "Empleados", MessageBoxButton.OK, MessageBoxImage.Question);
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
                ticket.Cliente = cmbNombre.SelectedItem as Clientes;
                ticket.Encargado = cmbEmpleado.SelectedItem as Empleado;
                ticket.FechaHora = dtpFecha.SelectedDate.Value;
                if (ManejadorDeTicket.Agregar(ticket))
                {
                    MessageBox.Show("compra hecha con exito", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
                    actualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("La venta no se pudo realizar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Ticket pro = dtgTicket.SelectedItem as Ticket;
                pro.Cliente = cmbNombre.SelectedItem as Clientes;
                pro.Encargado = cmbEmpleado.SelectedItem as Empleado;
                pro.Productos = cmbProducto.SelectedItem as Producto;
                pro.FechaHora = dtpFecha.SelectedDate.Value;
                if (ManejadorDeTicket.Modificar(pro))
                {
                    MessageBox.Show("la compra fue modificado correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
                    actualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("la compra no se pudo actualizar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    if (ManejadorDeTicket.Eliminar(pro.Id))
                    {
                        MessageBox.Show("La venta ha sido eliminado correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
                        actualizarTabla();
                    }
                    else
                    {
                        MessageBox.Show("El producto no se pudo eliminar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
        }

        ///ticket.Cliente = cmbNombre.SelectedItem as Clientes;
        //        ticket.Encargado = cmbEmpleado.SelectedItem as Empleado;
        //        ticket.FechaHora = dtpFecha.SelectedDate.Value;

        //    Ticket pro = dtgTicket.SelectedItem as Ticket;
        //pro.Cliente = cmbNombre.SelectedItem as Clientes;
        //        pro.Encargado = cmbEmpleado.SelectedItem as Empleado;
        //        pro.Productos = cmbProducto.SelectedItem as Producto;
        //        pro.FechaHora = dtpFecha.SelectedDate.Value;
    }
}
