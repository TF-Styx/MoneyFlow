using MoneyFlow.MVVM.ViewModels.BaseVM;
using MoneyFlow.Utils.Services.NavigationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyFlow.MVVM.ViewModels.WindowVM
{
    public class MainWindowVM : BaseViewModel, IUpdatable
    {
        private readonly IServiceProvider _serviceProvider;

        public MainWindowVM(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void Update(object parameter)
        {
            
        }
    }
}
