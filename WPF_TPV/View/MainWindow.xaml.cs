using System.Windows;
using System.Windows.Input;

namespace WPF_TPV.View

{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void buttonOpenCaja_Click(object sender, RoutedEventArgs e)
        {
            CajaWindow cajaWindow = new CajaWindow();
            cajaWindow.Show();
            this.Close();
        }

        private void buttonOpenAjustes_Click(object sender, RoutedEventArgs e)
        {
            AjustesWindow ajustesWindow = new AjustesWindow();
            ajustesWindow.Show();
            this.Close();
        }

        private void buttonOpenClientes_Click(object sender, RoutedEventArgs e)
        {
            ClientesWindow clientesWindow = new ClientesWindow();
            clientesWindow.Show();
            this.Close();
        }

        private void buttonSalir_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Desea cerrar la aplicación?", "Confirmación de salida", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
            else
            {

                var loginWindow = new LoginWindow();
                this.Close();
                loginWindow.Show();
                loginWindow.IsVisibleChanged += (s, ev) =>
                {
                    if (loginWindow.IsVisible == false && loginWindow.IsLoaded)
                    {
                        var mainWindow = new MainWindow();
                        mainWindow.Show();
                        loginWindow.Close();
                    }
                };
            }
        }
    }
}
