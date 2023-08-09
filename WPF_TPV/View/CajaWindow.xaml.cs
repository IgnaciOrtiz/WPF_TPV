using System.Windows;
using System.Windows.Input;

namespace WPF_TPV.View
{
    /// <summary>
    /// Lógica de interacción para CajaWindow.xaml
    /// </summary>
    public partial class CajaWindow : Window
    {
        public CajaWindow()
        {
            InitializeComponent();
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
    }
}
