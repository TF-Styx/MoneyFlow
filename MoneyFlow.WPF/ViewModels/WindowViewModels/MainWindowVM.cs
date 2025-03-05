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

        public void Update(object parameter, ParameterType typeParameter = ParameterType.None)
        {
            
        }

        private RelayCommand _openAddBaseInformationWindowCommand;
        public RelayCommand OpenAddBaseInformationWindowCommand 
        { 
            get => _openAddBaseInformationWindowCommand ??= new(obj => 
            { 
                _navigationWindows.OpenWindow(WindowType.AddBaseInformationWindow); 
            }); 
        }

        private RelayCommand _openProfileUserPageCommand;
        public RelayCommand OpenProfileUserPageCommand
        {
            get => _openProfileUserPageCommand ??= new(obj =>
            {
                _navigationPages.OpenPage(PageType.UserPage);
            });
        }

        private RelayCommand _openBankPageCommand;
        public RelayCommand OpenBankPageCommand
        { 
            get => _openBankPageCommand ??= new(obj => 
            {
                _navigationPages.OpenPage(PageType.BankPage);
            }); 
        }

        private RelayCommand _openAccountPageCommand;
        public RelayCommand OpenAccountPageCommand
        {
            get => _openAccountPageCommand ??= new(obj =>
            {
                _navigationPages.OpenPage(PageType.AccountPage);
            });
        }

        private RelayCommand _openAccountTypePageCommand;
        public RelayCommand OpenAccountTypePageCommand
        {
            get => _openAccountTypePageCommand ??= new(obj =>
            {
                _navigationPages.OpenPage(PageType.AccountTypePage);
            });
        }
    }
}
