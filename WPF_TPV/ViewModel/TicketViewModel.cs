using System.Collections.ObjectModel;
using System.Linq;
using WPF_TPV.Model;

namespace WPF_TPV.ViewModel
{
    public class TicketViewModel : ViewModelBase
    {
        private ObservableCollection<ProductoModel> _productosEnTicket;
        public ObservableCollection<ProductoModel> ProductosEnTicket
        {
            get { return _productosEnTicket; }
            set
            {
                _productosEnTicket = value;
                OnPropertyChanged(nameof(ProductosEnTicket));
                ActualizarTotal();
            }
        }

        private decimal _total;
        public decimal Total
        {
            get { return _total; }
            set
            {
                _total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        public TicketViewModel()
        {
            ProductosEnTicket = new ObservableCollection<ProductoModel>();
            ActualizarTotal();
        }

        public void AgregarProductoAlTicket(ProductoModel producto)
        {
            ProductosEnTicket.Add(producto);
            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            Total = ProductosEnTicket.Sum(producto => producto.Precio);
        }
    }
}