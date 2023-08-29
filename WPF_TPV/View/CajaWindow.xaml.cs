using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using WPF_TPV.Model;
using WPF_TPV.Repositories;
using WPF_TPV.ViewModel;

namespace WPF_TPV.View
{
    public partial class CajaWindow : Window
    {
        private DispatcherTimer timer;
        private FamiliaRepository familiaRepository = new FamiliaRepository();
        private FacturaRepository facturaRepository = new FacturaRepository();
        private TicketViewModel _ticketViewModel;
        private List<ProductoViewModel> productosEnTicketTemp = new List<ProductoViewModel>();
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

            //CargarTicket
            _ticketViewModel = new TicketViewModel();
            listBoxTicket.DataContext = _ticketViewModel;
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

            DateTime horaActual = DateTime.Now;
            horaTextBlock.Text = horaActual.ToString("HH:mm:ss");
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
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
                btnFamilia.Style = (Style)FindResource("SeccionButtonStyle");
                btnFamilia.Click += (sender, e) => MostrarProductosDeFamilia(familiaVM.Familia.idFamiliaProd);
                wrapPanelFamilias.Children.Add(btnFamilia);
            }
        }
        private ObservableCollection<ProductoViewModel> productosEnTicket = new ObservableCollection<ProductoViewModel>();
        private decimal totalTicket = 0;

        public ObservableCollection<ProductoViewModel> ProductosEnTicket
        {
            get { return productosEnTicket; }
            set
            {
                productosEnTicket = value;
                OnPropertyChanged(nameof(ProductosEnTicket));
            }
        }

        private DependencyPropertyChangedEventArgs nameof(ObservableCollection<ProductoViewModel> productosEnTicket)
        {
            throw new NotImplementedException();
        }

        private void MostrarProductosDeFamilia(int idFamilia)
        {
            ProductoRepository productoRepository = new ProductoRepository();
            var productos = productoRepository.GetProductosByFamilia(idFamilia);

            wrapPanelProductos.Children.Clear(); // Limpia los botones anteriores

            foreach (var producto in productos)
            {
                ProductoViewModel productoVM = new ProductoViewModel { Producto = producto };
                decimal precioProducto = productoRepository.GetPrecioProducto(producto.idProducto);
                productoVM.Producto.Precio = precioProducto;
                Button btnProducto = new Button();
                btnProducto.Content = productoVM.Producto.Nombre;
                btnProducto.Style = (Style)FindResource("ProductoButtonStyle");
                btnProducto.Click += (sender, e) => AgregarProductoAlTicket(productoVM);
                wrapPanelProductos.Children.Add(btnProducto);
            }
        }
        private void AgregarProductoAlTicket(ProductoViewModel producto)
        {
            _ticketViewModel.AgregarProductoAlTicket(producto.Producto);
            productosEnTicketTemp.Add(producto);
            totalTicket += producto.Producto.Precio;
            ActualizarTotalTicket();
        }
        private void CerrarTicket_Click(object sender, RoutedEventArgs e)
        {
            CerrarTicket();
        }
        private void CerrarTicket()
        {
            foreach (var producto in productosEnTicketTemp)
            {
                _ticketViewModel.AgregarProductoAlTicket(producto.Producto); // Agrega productos al ViewModel del ticket
            }

            // Crear una instancia de FacturaModel
            FacturaModel factura = new FacturaModel
            {
                NumFactura = facturaRepository.ObtenerNuevoNumeroFactura(),
                Fecha = DateTime.Now,
                IdCliente = facturaRepository.ObtenerIdCliente(),
                Total = totalTicket
            };

            int facturaId = facturaRepository.GuardarFactura(factura); // Guardar la factura

            foreach (var producto in productosEnTicketTemp)
            {
                LineaFacturaModel lineaFactura = new LineaFacturaModel
                {
                    Cantidad = 1,
                    Precio = producto.Producto.Precio,
                    IdProducto = producto.Producto.idProducto
                };
                facturaRepository.GuardarLineaFactura(facturaId, lineaFactura); // Guardar la línea de factura
            }


            productosEnTicketTemp.Clear(); // Limpia la lista temporal de productos
            _ticketViewModel.ProductosEnTicket.Clear(); // Limpia los productos del ViewModel del ticket
            totalTicket = 0; // Reinicia el total a cero

            ActualizarTotalTicket();
        }
        private void ActualizarTotalTicket()
        {
            // Actualizar el total en la vista
            totalTextBlock.Text = $"Total: {totalTicket:C}";
        }
    }
}
