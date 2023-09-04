using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WPF_TPV.Model;
using WPF_TPV.Repositories;

namespace WPF_TPV.View
{
    /// <summary>
    /// Lógica de interacción para ProductosWindow.xaml
    /// </summary>
    public partial class ProductosWindow : Window
    {
        private DispatcherTimer timer;
        private ProductoRepository productoRepository = new ProductoRepository();
        private ObservableCollection<ProductoModel> productos = new ObservableCollection<ProductoModel>();

        public ProductosWindow()
        {
            InitializeComponent();

            // Inicializar el temporizador para actualizarse cada segundo
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            ActualizarHora();

            CargarProductos();
        }

        private void ActualizarHora()
        {
            DateTime horaActual = DateTime.Now;
            horaTextBlock.Text = horaActual.ToString("HH:mm:ss");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ActualizarHora();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void buttonVolver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GuardarCambios_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (ProductoModel producto in productos)
                {
                    if (producto.idProducto == 0)
                    {
                        productoRepository.AgregarProducto(producto);
                    }
                    else
                    {
                        productoRepository.EditarProducto(producto);
                    }
                }
                MessageBox.Show("Cambios guardados correctamente");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los cambios : {ex.Message}");
            }

            //Actualiza productos
            CargarProductos();
        }

        private void CancelarCambios_Click(object sender, RoutedEventArgs e)
        {
            CargarProductos();
            MessageBox.Show("Cambios cancelados.");
        }

        private void EliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProductos.SelectedItem != null && dataGridProductos.SelectedItem is ProductoModel producto)
            {
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de eliminar esta familia?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    productoRepository.EliminarProducto(producto.idProducto);

                    //Recargar productos
                    CargarProductos();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una familia para eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CargarProductos()
        {
            productos = productoRepository.GetProductosGrid();
            dataGridProductos.ItemsSource = productos;
        }
    }
}
