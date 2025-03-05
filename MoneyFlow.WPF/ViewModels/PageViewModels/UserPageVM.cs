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
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly ISubcategoryService _subcategoryService;

        private readonly INavigationPages _navigationPages;

        public UserPageVM(IAuthorizationService authorizationService, 
                          IAccountService accountService, 
                          IAccountTypeService accountTypeService, 
                          IBankService bankService, 
                          IUserService userService,
                          ICategoryService categoryService,
                          ISubcategoryService subcategoryService,
                          INavigationPages navigationPages)
        {
            _authorizationService = authorizationService;
            _accountService = accountService;
            _accountTypeService = accountTypeService;
            _bankService = bankService;
            _userService = userService;
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;

            _navigationPages = navigationPages;

            CurrentUser = _authorizationService.CurrentUser;
            UserTotalInfo = _userService.GetUserInfo(CurrentUser.IdUser);

            //GetAccountType();
            //GetBank();

            GetAccount();
            GetUserAccountTypes();
            GetUserBanks();
            GetCategory();

            GetIdUserAllSubcategory();
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

        private UserTotalInfoDTO _userTotalInfo;
        public UserTotalInfoDTO UserTotalInfo
        {
            get => _userTotalInfo;
            set
            {
                _userTotalInfo = value;
                OnPropertyChanged();
            }
        }

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


        #region Категории пользователя

        private CategoryDTO _selectedCategory;
        public CategoryDTO SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;

                GetIdUserIdCategorySubcategory();

                OnPropertyChanged();
            }
        }

        public ObservableCollection<CategoryDTO> Categories { get; set; } = [];

        private async void GetCategory()
        {
            Categories.Clear();

            var list = await _categoryService.GetCatAsyncCategory(CurrentUser.IdUser);

            foreach (var item in list)
            {
                Categories.Add(item);
            }
        }

        private RelayCommand _allSubCommand;
        public RelayCommand AllSubCommand
        {
            get => _allSubCommand ??= new(obj =>
            {
                GetIdUserAllSubcategory();
            });
        }

        #endregion


        // ------------------------------------------------------------------------------------------------------------------------------------------------


        #region Подкатегории пользователя

        public ObservableCollection<SubcategoryDTO> Subcategories { get; set; } = [];

        private async void GetIdUserIdCategorySubcategory()
        {
            Subcategories.Clear();

            var list = _subcategoryService.GetIdUserIdCategorySub(CurrentUser.IdUser, SelectedCategory.IdCategory);
            //var list = _subcategoryService.GetAllIdUserSub(CurrentUser.IdUser);

            foreach (var item in list)
            {
                Subcategories.Add(item);
            }
        }

        private async void GetIdUserAllSubcategory()
        {
            Subcategories.Clear();

            //var list = _subcategoryService.GetIdUserIdCategorySub(CurrentUser.IdUser, SelectedCategory.IdCategory);
            var list = _subcategoryService.GetAllIdUserSub(CurrentUser.IdUser);

            foreach (var item in list)
            {
                Subcategories.Add(item);
            }
        }

        #endregion


        // ------------------------------------------------------------------------------------------------------------------------------------------------


        #region Навигация

        private void DoubleClick(object parameter)
        {
            if (parameter is AccountDTO account)
            {
                _navigationPages.OpenPage(PageType.AccountPage, account);
            }
            if (parameter is AccountTypeDTO accountType)
            {
                _navigationPages.OpenPage(PageType.AccountTypePage, accountType);
            }
            if (parameter is BankDTO bank)
            {
                _navigationPages.OpenPage(PageType.BankPage, bank);
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

        private RelayCommand _openCatAndSubPageCommand;
        public RelayCommand OpenCatAndSubPageCommand
        {
            get => _openCatAndSubPageCommand ??= new(obj =>
            {
                _navigationPages.OpenPage(PageType.CatAndSubPage);
            });
        }

        #endregion
    }
}
