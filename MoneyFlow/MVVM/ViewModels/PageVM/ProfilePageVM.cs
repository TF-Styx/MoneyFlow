using MoneyFlow.MVVM.ViewModels.BaseVM;
using MoneyFlow.Utils.Services.NavigationServices;

namespace MoneyFlow.MVVM.ViewModels.PageVM
{
    internal class ProfilePageVM : BaseViewModel, IUpdatable
    {
        private readonly IServiceProvider _serviceProvider;

        public ProfilePageVM(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Update(object parameter)
        {
            
        }
    }
}
