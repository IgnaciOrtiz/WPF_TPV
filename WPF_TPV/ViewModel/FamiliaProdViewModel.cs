using WPF_TPV.Model;

namespace WPF_TPV.ViewModel
{
    public class FamiliaProdViewModel : ViewModelBase
    {
        private FamiliaProdModel _familia;
        public FamiliaProdModel Familia
        {
            get { return _familia; }
            set
            {
                _familia = value;
                OnPropertyChanged(nameof(Familia));
            }
        }
    }
}
