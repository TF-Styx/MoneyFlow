using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Services.Abstraction;
using MoneyFlow.Application.UseCaseInterfaces.FinancialRecordViewingInterfaces;
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
        private readonly ITransactionTypeService _transactionTypeService;
        private readonly IFinancialRecordService _financialRecordService;
        private readonly IGetFinancialRecordViewingUseCase _getFinancialRecordViewingUseCase;

        private readonly INavigationPages _navigationPages;

        public UserPageVM(IAuthorizationService authorizationService, 
                          IAccountService accountService, 
                          IAccountTypeService accountTypeService, 
                          IBankService bankService, 
                          IUserService userService,
                          ICategoryService categoryService,
                          ISubcategoryService subcategoryService,
                          ITransactionTypeService transactionTypeService,
                          IFinancialRecordService financialRecordService, 
                          IGetFinancialRecordViewingUseCase getFinancialRecordViewingUseCase,
                          INavigationPages navigationPages)
        {
            _authorizationService = authorizationService;
            _accountService = accountService;
            _accountTypeService = accountTypeService;
            _bankService = bankService;
            _userService = userService;
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
            _transactionTypeService = transactionTypeService;
            _financialRecordService = financialRecordService;
            _getFinancialRecordViewingUseCase = getFinancialRecordViewingUseCase;

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

            GetTransactionTypeFilter();
            GetCategoryFilter();
            GetAccountFilter();

            var (Start, End) = DefaultDate();
            DateStartFilter = Start;
            DateEndFilter = End;

            GetFinancialRecord();
        }

        public void Update(object parameter, ParameterType typeParameter = ParameterType.None)
        {
            #region Обновление данных счетов

            if (parameter is AccountDTO accountAdd && typeParameter is ParameterType.Add)
            {
                Accounts.Add(accountAdd);
            }
            if (parameter is AccountDTO accountUpdate && typeParameter is ParameterType.Update)
            {
                var itemForDelete = Accounts.FirstOrDefault(x => x.IdAccount == accountUpdate.IdAccount);
                var index = Accounts.IndexOf(itemForDelete);

                Accounts.Remove(itemForDelete);
                Accounts.Insert(index, accountUpdate);
            }
            if (parameter is AccountDTO accountDelete && typeParameter is ParameterType.Delete)
            {
                Accounts.Remove(Accounts.FirstOrDefault(x => x.IdAccount == accountDelete.IdAccount));
            }

            #endregion

            // -----------------------------------------------------------------------------------------------------------------------------

            #region Обновление финаносых записей

            if (parameter is FinancialRecordViewingDTO financialRecordAdd && typeParameter is ParameterType.Add)
            {
                FinancialRecords.Add(financialRecordAdd);
            }
            if (parameter is FinancialRecordViewingDTO financialRecordUpdate && typeParameter is ParameterType.Update)
            {
                var itemForDelete = FinancialRecords.FirstOrDefault(x => x.IdFinancialRecord == financialRecordUpdate.IdFinancialRecord);
                var index = FinancialRecords.IndexOf(itemForDelete);

                FinancialRecords.Remove(itemForDelete);
                FinancialRecords.Insert(index, itemForDelete);
            }
            if (parameter is FinancialRecordViewingDTO financialRecordDelete && typeParameter is ParameterType.Delete)
            {
                FinancialRecords.Remove(FinancialRecords.FirstOrDefault(x => x.IdFinancialRecord == financialRecordDelete.IdFinancialRecord));
            }

            #endregion

            // -----------------------------------------------------------------------------------------------------------------------------

            #region Обновление данных пользователя

            UserTotalInfo = _userService.GetUserInfo(CurrentUser.IdUser);

            #endregion
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

        // ---------------------------------------------------------------------------------------------------------------------------------


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


        // ---------------------------------------------------------------------------------------------------------------------------------


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


        // ---------------------------------------------------------------------------------------------------------------------------------


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


        // ---------------------------------------------------------------------------------------------------------------------------------


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

        private RelayCommand _userCategoryDoubleClickCommand;
        public RelayCommand UserCategoryDoubleClickCommand => _userCategoryDoubleClickCommand ??= new RelayCommand(DoubleClick);

        #endregion


        // ---------------------------------------------------------------------------------------------------------------------------------


        #region Подкатегории пользователя

        private SubcategoryDTO _selectedSubcategory;
        public SubcategoryDTO SelectedSubcategory
        {
            get => _selectedSubcategory;
            set
            {
                _selectedSubcategory = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SubcategoryDTO> Subcategories { get; set; } = [];

        /// <summary>
        /// Заполняет список с учетом пользователя и категории
        /// </summary>
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

        /// <summary>
        /// Заполняет список всеми подкатегориями пользователя
        /// </summary>
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

        ////private RelayCommand _userSubcategoryTypeDoubleClickCommand;
        //public RelayCommand UserSubcategoryTypeDoubleClickCommand => _userSubcategoryTypeDoubleClickCommand ??= new RelayCommand(DoubleClick);

        #endregion


        // ---------------------------------------------------------------------------------------------------------------------------------


        #region Финансовые записи пользователя

        private FinancialRecordViewingDTO _selectedFinancialRecord;
        public FinancialRecordViewingDTO SelectedFinancialRecord
        {
            get => _selectedFinancialRecord;
            set
            {
                _selectedFinancialRecord = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<FinancialRecordViewingDTO> FinancialRecords { get; set; } = [];

        public async Task GetFinancialRecord()
        {
            FinancialRecords.Clear();

            var filter = new FinancialRecordFilterDTO()
            {
                AmountStart = AmountStartFilter,
                AmountEnd = AmountEndFilter,
                IsConsiderAmount = IsConsiderAmount,

                IdTransactionType = SelectedTransactionTypeFilter?.IdTransactionType == 0 ? null : SelectedTransactionTypeFilter?.IdTransactionType,
                IdCategory = SelectedCategoryFilter?.IdCategory == 0 ? null : SelectedCategoryFilter?.IdCategory,
                IdAccount = SelectedAccountFilter?.IdAccount == 0 ? null : SelectedAccountFilter?.IdAccount,

                DateStart = DateStartFilter,
                DateEnd = DateEndFilter,
                IsConsiderDate = IsConsiderDate,
            };

            var list = await _getFinancialRecordViewingUseCase.GetAllAsyncFinancialRecordViewing(CurrentUser.IdUser, filter);

            foreach (var item in list)
            {
                FinancialRecords.Add(item);
            }
        }

        private RelayCommand _userFinancialRecordDoubleClickCommand;
        public RelayCommand UserFinancialRecordDoubleClickCommand => _userFinancialRecordDoubleClickCommand ??= new RelayCommand(DoubleClick);

        #endregion


        // ---------------------------------------------------------------------------------------------------------------------------------


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
            if (parameter is CategoryDTO category)
            {
                _navigationPages.OpenPage(PageType.CatAndSubPage, category);
            }
            //if (parameter is SubcategoryDTO subcategory)
            //{
            //    _navigationPages.OpenPage(PageType.CatAndSubPage, subcategory);
            //}
            if (parameter is FinancialRecordViewingDTO financialRecordViewing)
            {
                _navigationPages.OpenPage(PageType.FinancialRecordPage, financialRecordViewing);
            }
        }

        // ---------------------------------------------------------------------------------------------------------------------------------

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

        private RelayCommand _openFinancialRecordPageCommand;
        public RelayCommand OpenFinancialRecordPageCommand
        {
            get => _openFinancialRecordPageCommand ??= new(obj =>
            {
                _navigationPages.OpenPage(PageType.FinancialRecordPage);
            });
        }

        #endregion


        // ---------------------------------------------------------------------------------------------------------------------------------


        #region Фильтрация

        private bool _isOpenPopupFilter;
        public bool IsOpenPopupFilter
        {
            get => _isOpenPopupFilter;
            set
            {
                _isOpenPopupFilter = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _openPopupFilterCommand;
        public RelayCommand OpenPopupFilterCommand { get => _openPopupFilterCommand ??= new(async obj => { IsOpenPopupFilter = true; }); }

        // ---------------------------------------------------------------------------------------------------------------------------------

        #region Поля фильрации

        private decimal? _amountStartFilter;
        public decimal? AmountStartFilter
        {
            get => _amountStartFilter;
            set
            {
                _amountStartFilter = value;
                OnPropertyChanged();
            }
        }

        private decimal? _amountEndFilter;
        public decimal? AmountEndFilter
        {
            get => _amountEndFilter;
            set
            {
                _amountEndFilter = value;
                OnPropertyChanged();
            }
        }

        private bool _isConsiderAmount;
        public bool IsConsiderAmount
        {
            get => _isConsiderAmount;
            set
            {
                _isConsiderAmount = value;
                OnPropertyChanged();
            }
        }


        private TransactionTypeDTO? _selectedTransactionTypeFilter;
        public TransactionTypeDTO? SelectedTransactionTypeFilter
        {
            get => _selectedTransactionTypeFilter;
            set
            {
                _selectedTransactionTypeFilter = value;

                if (value == null) { return; }

                OnPropertyChanged();
            }
        }

        private CategoryDTO? _selectedCategoryFilter;
        public CategoryDTO? SelectedCategoryFilter
        {
            get => _selectedCategoryFilter;
            set
            {
                _selectedCategoryFilter = value;

                if (value == null) { return; }

                OnPropertyChanged();
            }
        }

        private AccountDTO? _selectedAccountFilter;
        public AccountDTO? SelectedAccountFilter
        {
            get => _selectedAccountFilter;
            set
            {
                _selectedAccountFilter = value;

                if (value == null) { return; }

                OnPropertyChanged();
            }
        }


        private DateTime? _dateStartFilter;
        public DateTime? DateStartFilter
        {
            get => _dateStartFilter;
            set
            {
                _dateStartFilter = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _dateEndFilter;
        public DateTime? DateEndFilter
        {
            get => _dateEndFilter;
            set
            {
                _dateEndFilter = value;
                OnPropertyChanged();
            }
        }

        private bool _isConsiderDate = true;
        public bool IsConsiderDate
        {
            get => _isConsiderDate;
            set
            {
                _isConsiderDate = value;
                OnPropertyChanged();
            }
        }

        private (DateTime Start, DateTime End) DefaultDate()
        {
            var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

            return (start, end);
        }

        #endregion

        // ---------------------------------------------------------------------------------------------------------------------------------

        public ObservableCollection<TransactionTypeDTO> TransactionTypesFilter { get; set; } = [];
        public async Task GetTransactionTypeFilter()
        {
            TransactionTypesFilter.Clear();

            var list = await _transactionTypeService.GetAllAsyncTransactionType();

            foreach (var item in list)
            {
                TransactionTypesFilter.Add(item);
            }

            TransactionTypesFilter.Insert(0, new TransactionTypeDTO()
            {
                IdTransactionType = 0,
                TransactionTypeName = "<<Не выбрано!!>>",
            });

            SelectedTransactionTypeFilter = TransactionTypesFilter.FirstOrDefault();
        }

        public ObservableCollection<CategoryDTO> CategoriesFilter { get; set; } = [];
        public async Task GetCategoryFilter()
        {
            CategoriesFilter.Clear();

            var list = await _categoryService.GetCatAsyncCategory(CurrentUser.IdUser);

            foreach (var item in list)
            {
                CategoriesFilter.Add(item);
            }

            CategoriesFilter.Insert(0, new CategoryDTO()
            {
                IdCategory = 0,
                CategoryName = "<<Не выбрано!!>>",
            });

            SelectedCategoryFilter = CategoriesFilter.FirstOrDefault();
        }

        public ObservableCollection<AccountDTO> AccountsFilter { get; set; } = [];
        public async Task GetAccountFilter()
        {
            AccountsFilter.Clear();

            var list = await _accountService.GetAllAsyncAccount(CurrentUser.IdUser);

            foreach (var item in list)
            {
                AccountsFilter.Add(item);
            }

            AccountsFilter.Insert(0, new AccountDTO()
            {
                IdAccount = 0,
                NumberAccount = 0,
            });

            SelectedAccountFilter = AccountsFilter.FirstOrDefault();
        }

        // ---------------------------------------------------------------------------------------------------------------------------------

        private RelayCommand _applyCommand;
        public RelayCommand ApplyCommand { get => _applyCommand ??= new(async obj => { await GetFinancialRecord(); }); }

        private RelayCommand _dropCommand;
        public RelayCommand DropCommand 
        { 
            get => _dropCommand ??= new(obj => 
            {
                IsConsiderAmount = false;

                SelectedTransactionTypeFilter = TransactionTypesFilter.FirstOrDefault();
                SelectedCategoryFilter = CategoriesFilter.FirstOrDefault();
                SelectedAccountFilter = AccountsFilter.FirstOrDefault();

                var (Start, End) = DefaultDate();
                DateStartFilter = Start;
                DateEndFilter = End;

                IsConsiderDate = false;

                GetFinancialRecord();
            }); 
        }

        #endregion
    }
}
