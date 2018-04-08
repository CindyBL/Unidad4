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
    /// Lógica de interacción para Categoria.xaml
    /// </summary>
    public partial class Categoria : Window
    {
        enum accion
        {
            Nuevo,
            Editar
        }

        IManejadorCategorias manejadorCategorias;
        accion accionCategorias;
        public Categoria()
        {
            InitializeComponent();
            manejadorCategorias = new ManejadorDeCategorias(new RepositorioDeCategorias());
            HabilitarCajas(false);
            HabilitarBotones(true);
            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            dtgCategoria.ItemsSource = null;
            dtgCategoria.ItemsSource = manejadorCategorias.Listar;
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
            accionCategorias = accion.Nuevo;
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
            Categorias cat = dtgCategoria.SelectedItem as Categorias;
            if (cat != null)
            {
                HabilitarCajas(true);
                txbCategoriaId.Text = cat.Id;
                txbNombreCategoria.Text = cat.nombreCategoria;
                accionCategorias = accion.Editar;
                HabilitarBotones(false);
            }
            else
            {
                MessageBox.Show("Seleccione la categoria que desea editar", "Categorias", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(false);
            HabilitarBotones(true);
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Categorias cat = dtgCategoria.SelectedItem as Categorias;
            if (cat != null)
            {
                if (MessageBox.Show("Realmente deseas eliminar a esta categoria?", "Farmacia", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorCategorias.Eliminar(cat.Id))
                    {
                        MessageBox.Show("La categoria ha sido eliminada correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTabla();
                    }
                    else
                    {
                        MessageBox.Show("La categoria no se pudo eliminar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accionCategorias == accion.Nuevo)
            {
                Categorias cat = new Categorias()
                {
                    nombreCategoria = txbNombreCategoria.Text,
                };
                if (manejadorCategorias.Agregar(cat))
                {
                    MessageBox.Show("Categoria agregada correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("La categoria no se pudo agregar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Categorias cat = dtgCategoria.SelectedItem as Categorias;
                cat.nombreCategoria = txbNombreCategoria.Text;
                if (manejadorCategorias.Modificar(cat))
                {
                    MessageBox.Show("Categoria modificada correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("La Categoria no se pudo actualizar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
