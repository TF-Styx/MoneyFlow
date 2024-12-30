using MoneyFlow.MVVM.ViewModels.BaseVM;
using MoneyFlow.Utils.Services.NavigationServices;

namespace MoneyFlow.MVVM.ViewModels.WindowVM
{
    public class AuthWndVM : BaseViewModel, IUpdatable
    {
        private IServiceProvider _serviceProvider;

        public AuthWndVM(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void Update(object parameter)
        {
            
        }
    }
}
