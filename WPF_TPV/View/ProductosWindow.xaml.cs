using System.Windows;
using System.Windows.Input;

namespace WPF_TPV.View
{
    /// <summary>
    /// Lógica de interacción para ProductosWindow.xaml
    /// </summary>
    public partial class ProductosWindow : Window
    {
        public ProductosWindow()
        {
            InitializeComponent();
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
    }
}
