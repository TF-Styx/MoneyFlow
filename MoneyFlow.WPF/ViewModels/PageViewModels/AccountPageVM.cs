using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Services.Abstraction;
using MoneyFlow.WPF.Commands;
using MoneyFlow.WPF.Enums;
using MoneyFlow.WPF.Interfaces;
using System.Collections.ObjectModel;

namespace MoneyFlow.WPF.ViewModels.PageViewModels
{
    internal class AccountPageVM : BaseViewModel, IUpdatable
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IAccountService _accountService;
        private readonly IAccountTypeService _accountTypeService;
        private readonly IBankService _bankService;

        private readonly INavigationPages _navigationPages;

        public AccountPageVM(IAuthorizationService authorizationService,
                             IAccountService accountService,
                             IAccountTypeService accountTypeService,
                             IBankService bankService,
                             INavigationPages navigationPages)
        {
            _authorizationService = authorizationService;
            _accountService = accountService;
            _accountTypeService = accountTypeService;
            _bankService = bankService;

            _navigationPages = navigationPages;

            CurrentUser = _authorizationService.CurrentUser;

            GetAccount();
            GetBank();
            GetAccountType();
        }
        public void Update(object parameter, ParameterType typeParameter = ParameterType.None)
        {
            if (parameter is AccountDTO account)
            {
                NumberAccount = account.NumberAccount;
                Balance = account.Balance;
                SelectedBank = Banks.FirstOrDefault(x => x.IdBank == account.Bank.IdBank);
                SelectedAccountType = AccountTypes.FirstOrDefault(x => x.IdAccountType == account.AccountType.IdAccountType);
            }
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


        #region Счета пользователя

        private decimal? _numberAccount;
        public decimal? NumberAccount
        {
            get => _numberAccount;
            set
            {
                _numberAccount = value;
                OnPropertyChanged();
            }
        }

        private decimal? _balance;
        public decimal? Balance
        {
            get => _balance;
            set
            {
                _balance = value;
                OnPropertyChanged();
            }
        }

        private AccountDTO _selectedAccount;
        public AccountDTO SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;

                NumberAccount = value.NumberAccount;
                Balance = value.Balance;
                SelectedBank = Banks.FirstOrDefault(x => x.IdBank == value.Bank.IdBank);
                SelectedAccountType = AccountTypes.FirstOrDefault(x => x.IdAccountType == value.AccountType.IdAccountType);

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

        #endregion


        #region Банки

        private BankDTO _selectedBank;
        public BankDTO SelectedBank
        {
            get => _selectedBank;
            set
            {
                _selectedBank = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<BankDTO> Banks { get; set; } = [];
        private void GetBank()
        {
            Banks.Clear();

            var list = _bankService.GetAllBank();

            foreach (var item in list)
            {
                Banks.Add(item);
            }
        }

        #endregion


        #region Тип счета

        private AccountTypeDTO _selectedAccountType;
        public AccountTypeDTO SelectedAccountType
        {
            get => _selectedAccountType;
            set
            {
                _selectedAccountType = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<AccountTypeDTO> AccountTypes { get; set; } = [];
        private void GetAccountType()
        {
            AccountTypes.Clear();

            var list = _accountTypeService.GetAllAccountType();

            foreach (var item in list)
            {
                AccountTypes.Add(item);
            }
        }

        #endregion


        #region Навигация

        private RelayCommand _openProfileUserPageCommand;
        public RelayCommand OpenProfileUserPageCommand
        {
            get => _openProfileUserPageCommand ??= new(obj =>
            {
                _navigationPages.OpenPage(PageType.UserPage);
            });
        }

        #endregion
    }
}
