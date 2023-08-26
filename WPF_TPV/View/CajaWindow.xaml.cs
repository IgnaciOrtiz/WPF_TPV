using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using WPF_TPV.Repositories;
using WPF_TPV.ViewModel;

namespace WPF_TPV.View
{
    /// <summary>
    /// Lógica de interacción para CajaWindow.xaml
    /// </summary>
    public partial class CajaWindow : Window
    {
        private DispatcherTimer timer;
        private FamiliaRepository familiaRepository = new FamiliaRepository();

        public CajaWindow()
        {
            InitializeComponent();

            // Inicializar el temporizador para actualizarse cada minuto
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            ActualizarHora();

            //Cargar familias para botones
            CargarBotonesFamilias();
        }

        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
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

        private void ActualizarHora()
        {
            // Obtén la hora actual del sistema
            DateTime horaActual = DateTime.Now;

            // Asigna la hora actual al contenido del TextBlock
            horaTextBlock.Text = horaActual.ToString("HH:mm:ss");
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Actualiza la hora cada vez que se dispara el temporizador
            ActualizarHora();
        }
        private void CargarBotonesFamilias()
        {
            var familias = familiaRepository.GetAllFamilias();

            foreach (var familia in familias)
            {
                FamiliaProdViewModel familiaVM = new FamiliaProdViewModel { Familia = familia };
                Button btnFamilia = new Button();
                btnFamilia.Content = familiaVM.Familia.Nombre;
                btnFamilia.Click += (sender, e) => MostrarProductosDeFamilia(familiaVM.Familia.idFamiliaProd);
                wrapPanelFamilias.Children.Add(btnFamilia);
            }
        }

        private void MostrarProductosDeFamilia(int idFamilia)
        {
            ProductoRepository productoRepository = new ProductoRepository();
            var productos = productoRepository.GetProductosByFamilia(idFamilia);

            wrapPanelProductos.Children.Clear(); // Limpia los botones anteriores

            foreach (var producto in productos)
            {
                ProductoViewModel productoVM = new ProductoViewModel { Producto = producto };
                Button btnProducto = new Button();
                btnProducto.Content = productoVM.Producto.Nombre;
                //btnProducto.Click += (sender, e) => AgregarProductoAlTicket(productoVM.Producto.idProducto);
                wrapPanelProductos.Children.Add(btnProducto);
            }
        }
    }
}
