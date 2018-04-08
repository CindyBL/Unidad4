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
    /// Lógica de interacción para NuevoProducto.xaml
    /// </summary>
    public partial class NuevoProducto : Window
    {
        enum accion
        {
            Nuevo,
            Editar
        }

        IManejadorProducto manejadorProducto;
        IManejadorCategorias manejadorCategorias;
        accion accionProducto;
        public NuevoProducto()
        {
            InitializeComponent();
            manejadorProducto = new ManejadorDeProducto(new RepositorioDeProducto());
            manejadorCategorias = new ManejadorDeCategorias(new RepositorioDeCategorias());
            HabilitarCajas(false);
            HabilitarBotones(true);
            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            dtgProductos.ItemsSource = null;
            dtgProductos.ItemsSource = manejadorProducto.Listar;
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
            cbxCategoria.ItemsSource = null;
            cbxCategoria.ItemsSource = manejadorCategorias.Listar;
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
            accionProducto = accion.Nuevo;
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
            Producto pro = dtgProductos.SelectedItem as Producto;
            if (pro != null)
            {
                HabilitarCajas(true);
                txbProductoId.Text = pro.Id;
                txbNombre.Text = pro.NombreProducto;
                txbDescripcion.Text = pro.Descripcion;
                txbPrecioCompra.Text = pro.PrecioCompra;
                txbPrecioVenta.Text = pro.PrecioVenta;
                txbPresentacion.Text = pro.Presentacion;
                cbxCategoria.Text = pro.Categoria;
                accionProducto = accion.Editar;
                HabilitarBotones(false);
            }
            else
            {
                MessageBox.Show("Seleccione el producto que desea editar", "Productos", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(false);
            HabilitarBotones(true);
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Producto pro = dtgProductos.SelectedItem as Producto;
            if (pro != null)
            {
                if (MessageBox.Show("Realmente deseas eliminar este producto?", "Farmacia", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorProducto.Eliminar(pro.Id))
                    {
                        MessageBox.Show("El producto ha sido eliminado correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTabla();
                    }
                    else
                    {
                        MessageBox.Show("El producto no se pudo eliminar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accionProducto == accion.Nuevo)
            {
                Producto pro = new Producto()
                {
                    NombreProducto = txbNombre.Text,
                    Descripcion=txbDescripcion.Text,
                    PrecioCompra=txbPrecioCompra.Text,
                    PrecioVenta=txbPrecioVenta.Text,
                    Presentacion=txbPresentacion.Text,
                    Categoria=cbxCategoria.Text,
                };
                if (manejadorProducto.Agregar(pro))
                {
                    MessageBox.Show("Producto agregado correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("El Producto no se pudo agregar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Producto pro = dtgProductos.SelectedItem as Producto;
                pro.NombreProducto = txbNombre.Text;
                pro.Descripcion = txbDescripcion.Text;
                pro.PrecioCompra = txbPrecioCompra.Text;
                pro.PrecioVenta = txbPrecioVenta.Text;
                pro.Presentacion = txbPresentacion.Text;
                pro.Categoria = cbxCategoria.Text;
                if (manejadorProducto.Modificar(pro))
                {
                    MessageBox.Show("Producto modificado correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("El producto no se pudo actualizar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
