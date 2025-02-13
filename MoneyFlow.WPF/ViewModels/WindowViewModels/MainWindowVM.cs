using MoneyFlow.WPF.Commands;
using MoneyFlow.WPF.Enums;
using MoneyFlow.WPF.Interfaces;

namespace MoneyFlow.WPF.ViewModels.WindowViewModels
{
    internal class MainWindowVM : BaseViewModel ,IUpdatable
    {
        private readonly INavigationPages _navigationPages;
        private readonly INavigationWindows _navigationWindows;

        public MainWindowVM(INavigationPages navigationPages, INavigationWindows navigationWindows)
        {
            _navigationPages = navigationPages;
            _navigationWindows = navigationWindows;
        }

        public void Update(object parameter, TypeParameter typeParameter = TypeParameter.None)
        {
            //Counter += (int)parameter;
        }

        //private int _counter;
        //public int Counter
        //{
        //    get => _counter;
        //    set
        //    {
        //        _counter = value;
        //        OnPropertyChanged();
        //    }
        //}

        private RelayCommand _openAddBaseInformationWindowCommand;
        public RelayCommand OpenAddBaseInformationWindowCommand 
        { 
            get => _openAddBaseInformationWindowCommand ??= new(obj => 
            { 
                _navigationWindows.OpenWindow(TypeWindow.AddBaseInformationWindow); 
            }); 
        }
    }
}
