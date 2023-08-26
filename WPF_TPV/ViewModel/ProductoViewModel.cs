using WPF_TPV.Model;

namespace WPF_TPV.ViewModel
{
    public class ProductoViewModel : ViewModelBase
    {
        private ProductoModel _producto;
        public ProductoModel Producto
        {
            get { return _producto; }
            set
            {
                _producto = value;
                OnPropertyChanged(nameof(Producto));
            }
        }
    }
}
