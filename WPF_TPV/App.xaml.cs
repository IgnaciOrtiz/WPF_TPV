using System.Windows;
using WPF_TPV.View;

namespace WPF_TPV
{

    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var loginWindow = new LoginWindow();
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
