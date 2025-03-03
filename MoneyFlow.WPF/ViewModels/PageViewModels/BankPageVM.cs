using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Services.Abstraction;
using MoneyFlow.WPF.Enums;
using MoneyFlow.WPF.Interfaces;
using System.Collections.ObjectModel;

namespace MoneyFlow.WPF.ViewModels.PageViewModels
{
    internal class BankPageVM : BaseViewModel ,IUpdatable
    {
        private readonly IAuthorizationService _authorizationService;

        private IBankService _bankService;

        public BankPageVM(IAuthorizationService authorizationService, IBankService bankService)
        {
            _authorizationService = authorizationService;

            _bankService = bankService;

            CurrentUser = _authorizationService.CurrentUser;

            GetBank();
        }

        public void Update(object parameter, ParameterType typeParameter = ParameterType.None)
        {
            
        }

        private UserDTO _currentUser;
        public UserDTO CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<BankDTO> Banks { get; set; } = [];

        private async void GetBank()
        {
            Banks.Clear();

            var list = await _bankService.GetAllAsyncBank();

            foreach (var item in list)
            {
                Banks.Add(item);
            }
        }
    }
}
