using System.Threading;
using WPF_TPV.Model;
using WPF_TPV.Repositories;

namespace WPF_TPV.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //Campos
        private UserAccountModel _currentUserAccount;
        private IUserRepository userRepository;

        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }
            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }
        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();
            LoadCurrentUserData();
        }

        public void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {


                CurrentUserAccount.UserName = user.UserName;
                CurrentUserAccount.DisplayName = $"Bienvenido {user.UserName}. Su id de empleado es: {user.idEmpleado}";
                CurrentUserAccount.ProfilePicture = null;

            }
            else
            {
                CurrentUserAccount.DisplayName = "Usuario o password incorrectos";

            }
        }

    }
}
