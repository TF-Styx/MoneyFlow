using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Services.Abstraction;
using MoneyFlow.WPF.Enums;
using MoneyFlow.WPF.Interfaces;
using System.Collections.ObjectModel;

namespace MoneyFlow.WPF.ViewModels.PageViewModels
{
    internal class UserPageVM : BaseViewModel, IUpdatable
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IAccountService _accountService;
        private readonly IBankService _bankService;
        private readonly IAccountTypeService _accountTypeService;

        private readonly INavigationPages _navigationPages;

        public UserPageVM(IAuthorizationService authorizationService, IAccountService accountService, IBankService bankService, IAccountTypeService accountTypeService, INavigationPages navigationPages)
        {
            _authorizationService = authorizationService;
            _accountService = accountService;
            _bankService = bankService;
            _accountTypeService = accountTypeService;

            _navigationPages = navigationPages;

            CurrentUser = _authorizationService.CurrentUser;

            GetAccount();
            GetAccountType();
            GetBank();
            GetUserBanks();
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


        public ObservableCollection<AccountDTO> Accounts { get; set; } = [];
        private async void GetAccount()
        {
            Accounts.Clear();

            var list = await _accountService.GetAllAsyncAccount(CurrentUser.IdUser);

            foreach (var item in list)
            {
                Accounts.Add(item);
            }
        }

        public ObservableCollection<AccountTypeDTO> AccountTypes { get; set; } = [];
        private async void GetAccountType()
        {
            AccountTypes.Clear();

            var list = await _accountTypeService.GetAllAsyncAccountType();

            foreach (var item in list)
            {
                AccountTypes.Add(item);
            }
        }

        private UserBanksDTO _userBanks;
        public UserBanksDTO UserBanks
        {
            get => _userBanks;
            set
            {
                _userBanks = value;
                OnPropertyChanged();
            }
        }
        private async void GetUserBanks()
        {
            UserBanks = await _bankService.GetByIdUserAsync(CurrentUser.IdUser);
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
