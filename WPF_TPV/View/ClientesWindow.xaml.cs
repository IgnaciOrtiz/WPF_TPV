using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WPF_TPV.Model;
using WPF_TPV.Repositories;

namespace WPF_TPV.View
{
    public partial class ClientesWindow : Window
    {
        private DispatcherTimer timer;
        private ClienteRepository clienteRepository = new ClienteRepository();
        private ObservableCollection<ClienteModel> clientes = new ObservableCollection<ClienteModel>();

        public ClientesWindow()
        {
            InitializeComponent();

            clienteRepository = new ClienteRepository();

            // Inicializar el temporizador para actualizarse cada segundo
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            ActualizarHora();


            // Cargar la lista de clientes en el DataGrid
            CargarClientes();
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

        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void buttonVolver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void GuardarCambios_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                foreach (ClienteModel cliente in clientes)
                {
                    if (cliente.IdCliente == 0)
                    {
                        // Nuevo cliente, agrégalo a la base de datos
                        clienteRepository.AgregarCliente(cliente);
                    }
                    else
                    {
                        // Cliente existente, actualiza la información en la base de datos
                        clienteRepository.EditarCliente(cliente);
                    }
                }

                MessageBox.Show("Cambios guardados correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los cambios: {ex.Message}");
            }
            // Actualiza lista clientes
            CargarClientes();
        }

        private void CancelarCambios_Click(object sender, RoutedEventArgs e)
        {
            // Vuelve a cargar los clientes desde la base de datos para descartar los cambios en el grid
            CargarClientes();
            MessageBox.Show("Cambios cancelados.");
        }

        private void EliminarCliente_Click(object sender, RoutedEventArgs e)
        {
            //si se ha seleccionado un cliente en el DataGrid
            if (dataGridClientes.SelectedItem != null && dataGridClientes.SelectedItem is ClienteModel cliente)
            {
                //confirmación antes de eliminar el cliente.
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de eliminar este cliente?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {

                    clienteRepository.EliminarCliente(cliente.IdCliente);

                    // Recargar la lista de clientes
                    CargarClientes();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un cliente para eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CargarClientes()
        {
            clientes = clienteRepository.GetAllClientes();
            dataGridClientes.ItemsSource = clientes;
        }
    }
}