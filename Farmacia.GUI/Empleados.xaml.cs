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
    /// Lógica de interacción para Empleados.xaml
    /// </summary>
    public partial class Empleados : Window
    {
        enum accion
        {
            Nuevo,
            Editar
        }

        IManejadorEmpleado manejadorEmpleado;
        accion accionEmpleados;
        public Empleados()
        {
            InitializeComponent();
            manejadorEmpleado = new ManejadorDeEmpleado(new RepositorioEmpleado());
            HabilitarCajas(false);
            HabilitarBotones(true);
            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            dtgEmpleados.ItemsSource = null;
            dtgEmpleados.ItemsSource = manejadorEmpleado.Listar;
        }

        private void HabilitarCajas(bool habilitadas)
        {
            txbNombre.Clear();
            txbApellido.Clear();
            txbNEmpleado.Clear();
            txbNombre.IsEnabled = habilitadas;
            txbApellido.IsEnabled = habilitadas;
            txbNEmpleado.IsEnabled = habilitadas;
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
            accionEmpleados = accion.Nuevo;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            Empleado emp = dtgEmpleados.SelectedItem as Empleado;
            if (emp != null)
            {
                HabilitarCajas(true);
                txbEmpleadosId.Text = emp.Id;
                txbNEmpleado.Text = emp.NoEmpleado;
                txbNombre.Text = emp.Nombre;
                txbApellido.Text = emp.Apellido;
                accionEmpleados = accion.Editar;
                HabilitarBotones(false);
            }
            else
            {
                MessageBox.Show("Seleccione el empleado que desea editar", "Empleados", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(false);
            HabilitarBotones(true);
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Empleado emp = dtgEmpleados.SelectedItem as Empleado;
            if (emp != null)
            { 
               if(MessageBox.Show("Realmente deseas eliminar a este empleado?", "Farmacia", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorEmpleado.Eliminar(emp.Id))
                    {
                        MessageBox.Show("El empleado ha sido eliminado correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTabla();
                    }
                    else
                    {
                        MessageBox.Show("El empleado no se pudo eliminar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accionEmpleados == accion.Nuevo)
            {
                Empleado emp = new Empleado()
                {
                    Nombre = txbNombre.Text,
                    Apellido = txbApellido.Text,
                    NoEmpleado = txbNEmpleado.Text,
                };
                if (manejadorEmpleado.Agregar(emp))
                {
                    MessageBox.Show("Empleado agregado correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("Empleado no se pudo agregar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Empleado emp = dtgEmpleados.SelectedItem as Empleado;
                emp.Apellido = txbApellido.Text;
                emp.Nombre = txbNombre.Text;
                emp.NoEmpleado = txbNEmpleado.Text;
                if (manejadorEmpleado.Modificar(emp))
                {
                    MessageBox.Show("Empleado modificado correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("Empleado no se pudo actualizar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
