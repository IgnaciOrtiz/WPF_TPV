using WPF_TPV.Model;

namespace WPF_TPV.ViewModel
{
    public class ClienteViewModel : ViewModelBase
    {
        private ClienteModel _cliente;

        public ClienteModel Cliente
        {
            get { return _cliente; }
            set
            {
                _cliente = value;
                OnPropertyChanged(nameof(Cliente));
            }
        }

        public string Nombre
        {
            get { return _cliente.Nombre; }
            set
            {
                _cliente.Nombre = value;
                OnPropertyChanged(nameof(Nombre));
            }
        }

        public string Apellidos
        {
            get { return _cliente.Apellidos; }
            set
            {
                _cliente.Apellidos = value;
                OnPropertyChanged(nameof(Apellidos));
            }
        }

        public string Direccion
        {
            get { return _cliente.Direccion; }
            set
            {
                _cliente.Direccion = value;
                OnPropertyChanged(nameof(Direccion));
            }
        }

        public string Telefono
        {
            get { return _cliente.Telefono; }
            set
            {
                _cliente.Telefono = value;
                OnPropertyChanged(nameof(Telefono));
            }
        }

        public string Email
        {
            get { return _cliente.Email; }
            set
            {
                _cliente.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string DNI
        {
            get { return _cliente.DNI; }
            set
            {
                _cliente.DNI = value;
                OnPropertyChanged(nameof(DNI));
            }
        }

        public ClienteViewModel()
        {
            _cliente = new ClienteModel();
        }
    }
}
