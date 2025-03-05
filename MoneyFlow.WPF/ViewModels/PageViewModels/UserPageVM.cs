using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Services.Abstraction;
using MoneyFlow.WPF.Commands;
using MoneyFlow.WPF.Enums;
using MoneyFlow.WPF.Interfaces;
using System.Collections.ObjectModel;

namespace MoneyFlow.WPF.ViewModels.PageViewModels
{
    internal class UserPageVM : BaseViewModel, IUpdatable
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IAccountService _accountService;
        private readonly IAccountTypeService _accountTypeService;
        private readonly IBankService _bankService;
        private readonly IGenderService _genderService;

        private readonly INavigationPages _navigationPages;

        public UserPageVM(IAuthorizationService authorizationService, 
                          IAccountService accountService, 
                          IAccountTypeService accountTypeService, 
                          IBankService bankService, 
                          IGenderService genderService, 
                          INavigationPages navigationPages)
        {
            _authorizationService = authorizationService;
            _accountService = accountService;
            _bankService = bankService;
            _genderService = genderService;
            _accountTypeService = accountTypeService;

            _navigationPages = navigationPages;

            CurrentUser = _authorizationService.CurrentUser;

            //GetAccountType();
            //GetBank();

            GetAccount();
            GetUserBanks();
            GetUserAccountTypes();
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


// ------------------------------------------------------------------------------------------------------------------------------------------------

        #region Общая информация

        private string _gender;
        public string Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalBalance;
        public decimal TotalBalance
        {
            get => _totalBalance;
            set
            {
                _totalBalance = value;
                OnPropertyChanged();
            }
        }

        private int _bankCount;
        public int BankCount
        {
            get => _bankCount;
            set
            {
                _bankCount = value;
                OnPropertyChanged();
            }
        }

        private int _categoryCount;
        public int CategoryCount
        {
            get => _categoryCount;
            set
            {
                _categoryCount = value;
                OnPropertyChanged();
            }
        }

        private int _subCategoryCount;
        public int SubCategoryCount
        {
            get => _subCategoryCount;
            set
            {
                _subCategoryCount = value;
                OnPropertyChanged();
            }
        }

        private int _financialRecordCount;
        public int FinancialRecordCount
        {
            get => _financialRecordCount;
            set
            {
                _financialRecordCount = value;
                OnPropertyChanged();
            }
        }

        private int _operationCount;
        public int OperationCount
        {
            get => _operationCount;
            set
            {
                _operationCount = value;
                OnPropertyChanged();
            }
        }

        #endregion

// ------------------------------------------------------------------------------------------------------------------------------------------------


        #region Счета пользователя

        private AccountDTO _selectedAccount;
        public AccountDTO SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;
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

        private RelayCommand _accountDoubleClickCommand;
        public RelayCommand AccountDoubleClickCommand => _accountDoubleClickCommand ??= new RelayCommand(DoubleClick);

        #endregion


// ------------------------------------------------------------------------------------------------------------------------------------------------


        #region Тип счета пользователя

        private string _accountTypeName;
        public string AccountTypeName
        {
            get => _accountTypeName;
            set
            {
                _accountTypeName = value;
                OnPropertyChanged();
            }
        }

        private UserAccountTypesDTO _userAccountTypes;
        public UserAccountTypesDTO UserAccountTypes
        {
            get => _userAccountTypes;
            set
            {
                _userAccountTypes = value;
                OnPropertyChanged();
            }
        }

        private AccountTypeDTO _selectedUserAccountType;
        public AccountTypeDTO SelectedUserAccountType
        {
            get => _selectedUserAccountType;
            set
            {
                _selectedUserAccountType = value;

                AccountTypeName = value.AccountTypeName;

                OnPropertyChanged();
            }
        }

        private async Task GetUserAccountTypes()
        {
            UserAccountTypes = await _accountTypeService.GetByIdUserAsync(CurrentUser.IdUser);
        }

        private RelayCommand _userAccountTypeDoubleClickCommand;
        public RelayCommand UserAccountTypeDoubleClickCommand => _userAccountTypeDoubleClickCommand ??= new RelayCommand(DoubleClick);

        #endregion

// ------------------------------------------------------------------------------------------------------------------------------------------------


        #region Банк пользователя

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

        private BankDTO _selectedUserBank;
        public BankDTO SelectedUserBank
        {
            get => _selectedUserBank;
            set
            {
                _selectedUserBank = value;
                OnPropertyChanged();
            }
        }

        private async Task GetUserBanks()
        {
            UserBanks = await _bankService.GetByIdUserAsync(CurrentUser.IdUser);
        }

        private RelayCommand _userBankDoubleClickCommand;
        public RelayCommand UserBankDoubleClickCommand => _userBankDoubleClickCommand ??= new RelayCommand(DoubleClick);

        #endregion


// ------------------------------------------------------------------------------------------------------------------------------------------------


        #region Навигация

        private void DoubleClick(object parameter)
        {
            if (parameter is BankDTO bank)
            {
                _navigationPages.OpenPage(PageType.BankPage, bank);
            }
            if (parameter is AccountDTO account)
            {
                _navigationPages.OpenPage(PageType.AccountPage, account);
            }
            if (parameter is AccountTypeDTO accountType)
            {
                _navigationPages.OpenPage(PageType.AccountTypePage, accountType);
            }
        }

// ------------------------------------------------------------------------------------------------------------------------------------------------

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

        #endregion
    }
}
