using MoneyFlow.Application.UseCaseInterfaces.BankCaseInterfaces;
using MoneyFlow.WPF.Commands;
using MoneyFlow.WPF.Enums;
using MoneyFlow.WPF.Interfaces;
using System.Windows;

namespace MoneyFlow.WPF.ViewModels.WindowViewModels
{
    internal class AuthWindowVM : IUpdatable
    {
        private readonly Lazy<INavigationPages> _navigationPages;
        private readonly Lazy<INavigationWindows> _navigationWindows;

        private readonly IGetBankUseCase _getBankUseCase;

        public AuthWindowVM(Lazy<INavigationPages> navigationPages, Lazy<INavigationWindows> navigationWindows,
                            IGetBankUseCase getBankUseCase)
        {
            _navigationPages = navigationPages;
            _navigationWindows = navigationWindows;

            _getBankUseCase = getBankUseCase;
        }

        public void Update(object parameter, TypeParameter typeParameter = TypeParameter.None)
        {
            
        }

        private RelayCommand _open;
        public RelayCommand Open 
        { 
            get => _open ??= new(obj => 
            { 
                _navigationWindows.Value.OpenWindow(TypeWindow.MainWindow);
                _navigationWindows.Value.CloseWindow(TypeWindow.AuthWindow);
            }); 
        }

        //private RelayCommand _open;
        //public RelayCommand Open { get => _open ??= new(async obj => 
        //{ 
        //    var list = await _getBankUseCase.GetAllBank();
        //    MessageBox.Show(list.Count.ToString());
        //});}
    }
}
