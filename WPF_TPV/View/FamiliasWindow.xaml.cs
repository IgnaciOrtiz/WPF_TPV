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
    /// Lógica de interacción para FamiliasWindow.xaml
    /// </summary>
    public partial class FamiliasWindow : Window
    {
        private DispatcherTimer timer;
        private FamiliaRepository familiaRepository = new FamiliaRepository();
        private ObservableCollection<FamiliaProdModel> familias = new ObservableCollection<FamiliaProdModel>();

        public FamiliasWindow()
        {
            InitializeComponent();

            // Inicializar el temporizador para actualizarse cada segundo
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            ActualizarHora();

            CargarFamilias();
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
                foreach (FamiliaProdModel familia in familias)
                {
                    if (familia.idFamiliaProd == 0)
                    {
                        familiaRepository.AgregarFamilia(familia);
                    }
                    else
                    {
                        familiaRepository.EditarFamilia(familia);
                    }
                }
                MessageBox.Show("Cambios guardados correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los cambios : {ex.Message}");
            }
            //Actuliza familias
            CargarFamilias();
        }

        private void CancelarCambios_Click(object sender, RoutedEventArgs e)
        {
            CargarFamilias();
            MessageBox.Show("Cambios cancelados.");
        }

        private void EliminarFamilia_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridFamilias.SelectedItem != null && dataGridFamilias.SelectedItem is FamiliaProdModel familia)
            {
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de eliminar esta familia?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    familiaRepository.EliminarFamilia(familia.idFamiliaProd);

                    //Recargar familias
                    CargarFamilias();

                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una familia para eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CargarFamilias()
        {
            familias = familiaRepository.GetFamiliasGrid();
            dataGridFamilias.ItemsSource = familias;
        }
    }
}
