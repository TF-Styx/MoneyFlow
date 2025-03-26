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
        private readonly INavigationWindows _navigationWindows;

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
                          INavigationPages navigationPages,
                          INavigationWindows navigationWindows)
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
            _navigationWindows = navigationWindows;

            CurrentUser = _authorizationService.CurrentUser;
            UserTotalInfo = _userService.GetUserInfo(CurrentUser.IdUser);

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
            #region Обновление данных счетов и банков

            if (parameter is ValueTuple<AccountDTO, BankDTO> cartageAccountBankAdd && typeParameter is ParameterType.Add)
            {
                Accounts.Add(cartageAccountBankAdd.Item1);
                UserBanks.Banks.Add(cartageAccountBankAdd.Item2);
                UserTotalInfo.BankCount += 1;
            }
            if (parameter is ValueTuple<AccountDTO, BankDTO> cartageAccountBankUpdate && typeParameter is ParameterType.Update)
            {
                var itemForDeleteAccount = Accounts.FirstOrDefault(x => x.IdAccount == cartageAccountBankUpdate.Item1.IdAccount);
                var indexAccount = Accounts.IndexOf(itemForDeleteAccount);

                Accounts.Remove(itemForDeleteAccount);
                Accounts.Insert(indexAccount, cartageAccountBankUpdate.Item1);

                var itemForDeleteBank = UserBanks.Banks.FirstOrDefault(x => x.IdBank == cartageAccountBankUpdate.Item2.IdBank);
                var indexBank = UserBanks.Banks.IndexOf(itemForDeleteBank);

                UserBanks.Banks.Remove(itemForDeleteBank);
                UserBanks.Banks.Insert(indexAccount, cartageAccountBankUpdate.Item2);
            }
            if (parameter is ValueTuple<AccountDTO, BankDTO> cartageAccountBankDelete && typeParameter is ParameterType.Delete)
            {
                Accounts.Remove(Accounts.FirstOrDefault(x => x.IdAccount == cartageAccountBankDelete.Item1.IdAccount));
                UserBanks.Banks.Remove(UserBanks.Banks.FirstOrDefault(x => x.IdBank == cartageAccountBankDelete.Item2.IdBank));
                UserTotalInfo.BankCount -= 1;
            }

            #endregion

            // -----------------------------------------------------------------------------------------------------------------------------

            #region Обновление категорий

            if (parameter is CategoryDTO categoryAdd && typeParameter is ParameterType.Add)
            {
                Categories.Add(categoryAdd);

                UserTotalInfo.CategoryCount += 1;
            }
            if (parameter is CategoryDTO categoryUpdate && typeParameter is ParameterType.Update)
            {
                var itemForDelete = Categories.FirstOrDefault(x => x.IdCategory == categoryUpdate.IdCategory);
                var index = Categories.IndexOf(itemForDelete);

                Categories.Remove(itemForDelete);
                Categories.Insert(index, categoryUpdate);
            }
            if (parameter is CategoryDTO categoryDelete && typeParameter is ParameterType.Delete)
            {
                Categories.Remove(Categories.FirstOrDefault(x => x.IdCategory == categoryDelete.IdCategory));

                UserTotalInfo.AccountCount -= 1;
            }

            #endregion

            // -----------------------------------------------------------------------------------------------------------------------------

            #region Обновление подкатегорий

            if (parameter is SubcategoryDTO subcategoryAdd && typeParameter is ParameterType.Add)
            {
                Subcategories.Add(subcategoryAdd);

                UserTotalInfo.SubcategoryCount += 1;
            }
            if (parameter is SubcategoryDTO subcategoryUpdate && typeParameter is ParameterType.Update)
            {
                var itemForDelete = Subcategories.FirstOrDefault(x => x.IdSubcategory == subcategoryUpdate.IdSubcategory);
                var index = Subcategories.IndexOf(itemForDelete);

                Subcategories.Remove(itemForDelete);
                Subcategories.Insert(index, subcategoryUpdate);
            }
            if (parameter is SubcategoryDTO subcategoryDelete && typeParameter is ParameterType.Delete)
            {
                Subcategories.Remove(Subcategories.FirstOrDefault(x => x.IdSubcategory == subcategoryDelete.IdSubcategory));

                UserTotalInfo.AccountCount -= 1;
            }

            #endregion

            // -----------------------------------------------------------------------------------------------------------------------------

            #region Обновление финаносых записей и пересчет баланса счета, цказанного в записи

            if (parameter is ValueTuple<FinancialRecordViewingDTO, int> cartageFinancialRecordAdd && typeParameter is ParameterType.Add)
            {
                FinancialRecords.Add(cartageFinancialRecordAdd.Item1);

                var accountDTO = _accountService.Get(cartageFinancialRecordAdd.Item1.AccountNumber);

                var account = Accounts.FirstOrDefault(x => x.IdAccount == accountDTO.IdAccount);
                var indexAccount = Accounts.IndexOf(account);

                if (cartageFinancialRecordAdd.Item2 == 1)
                {
                    UserTotalInfo.TotalBalance += cartageFinancialRecordAdd.Item1.Amount;
                }
                else if (cartageFinancialRecordAdd.Item2 == 2)
                {
                    UserTotalInfo.TotalBalance -= cartageFinancialRecordAdd.Item1.Amount;
                }

                Accounts.Remove(account);
                Accounts.Insert(indexAccount, accountDTO);

                UserTotalInfo.AccountCount += 1;
            }
            if (parameter is ValueTuple<FinancialRecordViewingDTO, int> cartageFinancialRecordUpdate && typeParameter is ParameterType.Update)
            {
                var itemForDelete = FinancialRecords.FirstOrDefault(x => x.IdFinancialRecord == cartageFinancialRecordUpdate.Item1.IdFinancialRecord);
                var index = FinancialRecords.IndexOf(itemForDelete);

                if (itemForDelete != null)
                {
                    FinancialRecords.Remove(itemForDelete);
                    FinancialRecords.Insert(index, cartageFinancialRecordUpdate.Item1);

                    var accountDTO = _accountService.Get(cartageFinancialRecordUpdate.Item1.AccountNumber);

                    var account = Accounts.FirstOrDefault(x => x.IdAccount == accountDTO.IdAccount);
                    var indexAccount = Accounts.IndexOf(account);

                    if (cartageFinancialRecordUpdate.Item2 == 1)
                    {
                        UserTotalInfo.TotalBalance += cartageFinancialRecordUpdate.Item1.Amount;
                    }
                    else if (cartageFinancialRecordUpdate.Item2 == 2)
                    {
                        UserTotalInfo.TotalBalance -= cartageFinancialRecordUpdate.Item1.Amount;
                    }

                    Accounts.Remove(account);
                    Accounts.Insert(indexAccount, accountDTO);

                    UserTotalInfo.AccountCount += 1;
                }
                else 
                {
                    var accountDTO = _accountService.Get(cartageFinancialRecordUpdate.Item1.AccountNumber);

                    var account = Accounts.FirstOrDefault(x => x.IdAccount == accountDTO.IdAccount);
                    var indexAccount = Accounts.IndexOf(account);

                    if (cartageFinancialRecordUpdate.Item2 == 1)
                    {
                        UserTotalInfo.TotalBalance += cartageFinancialRecordUpdate.Item1.Amount;
                    }
                    else if (cartageFinancialRecordUpdate.Item2 == 2)
                    {
                        UserTotalInfo.TotalBalance -= cartageFinancialRecordUpdate.Item1.Amount;
                    }

                    Accounts.Remove(account);
                    Accounts.Insert(indexAccount, accountDTO);

                    UserTotalInfo.AccountCount += 1;

                    return; 
                }
            }
            if (parameter is FinancialRecordViewingDTO financialRecordDelete && typeParameter is ParameterType.Delete)
            {
                FinancialRecords.Remove(FinancialRecords.FirstOrDefault(x => x.IdFinancialRecord == financialRecordDelete.IdFinancialRecord));

                UserTotalInfo.AccountCount -= 1;
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

            var list = await _accountService.GetAllAsync(CurrentUser.IdUser);

            foreach (var item in list)
            {
                Accounts.Add(item);
                var index = Accounts.IndexOf(item);
                item.Index = index + 1;
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

                if (value == null) { return; }

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
                var index = Categories.IndexOf(item);
                item.Index = index + 1;
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
                var index = Subcategories.IndexOf(item);
                item.Index = index + 1;
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
                var index = Subcategories.IndexOf(item);
                item.Index = index + 1;
            }
        }

        private RelayCommand _userSubcategoryTypeDoubleClickCommand;
        public RelayCommand UserSubcategoryTypeDoubleClickCommand => _userSubcategoryTypeDoubleClickCommand ??= new RelayCommand(DoubleClick);

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
                IdSubcategory = SelectedSubcategoryFilter?.IdSubcategory == 0 ? null : SelectedSubcategoryFilter?.IdSubcategory,
                IdAccount = SelectedAccountFilter?.IdAccount == 0 ? null : SelectedAccountFilter?.IdAccount,

                DateStart = DateStartFilter,
                DateEnd = DateEndFilter,
                IsConsiderDate = IsConsiderDate,
            };

            var list = await _getFinancialRecordViewingUseCase.GetAllViewingAsync(CurrentUser.IdUser, filter);

            foreach (var item in list)
            {
                FinancialRecords.Add(item);
                var index = FinancialRecords.IndexOf(item);
                item.Index = index + 1;
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
                _navigationWindows.OpenWindow(WindowType.AccountWindow, account);
            }
            if (parameter is AccountTypeDTO accountType)
            {
                _navigationWindows.OpenWindow(WindowType.AccountTypeWindow, accountType);
            }
            if (parameter is BankDTO bank)
            {
                _navigationWindows.OpenWindow(WindowType.BankWindow, bank);
            }
            if (parameter is CategoryDTO category)
            {
                _navigationWindows.OpenWindow(WindowType.CatAndSubWindow, category);
            }
            if (parameter is SubcategoryDTO subcategory)
            {
                _navigationWindows.OpenWindow(WindowType.CatAndSubWindow, subcategory);
            }
            if (parameter is FinancialRecordViewingDTO financialRecordViewing)
            {
                _navigationWindows.OpenWindow(WindowType.FinancialRecordWindow, financialRecordViewing);
            }
        }

        // ---------------------------------------------------------------------------------------------------------------------------------

        private RelayCommand _openAccountWindowCommand;
        public RelayCommand OpenAccountWindowCommand
        {
            get => _openAccountWindowCommand ??= new(obj =>
            {
                _navigationWindows.OpenWindow(WindowType.AccountWindow);
            });
        }

        private RelayCommand _openAccountTypeWindowCommand;
        public RelayCommand OpenAccountTypeWindowCommand
        {
            get => _openAccountTypeWindowCommand ??= new(obj =>
            {
                _navigationWindows.OpenWindow(WindowType.AccountTypeWindow);
            });
        }

        private RelayCommand _openBankWindowCommand;
        public RelayCommand OpenBankWindowCommand
        {
            get => _openBankWindowCommand ??= new(obj =>
            {
                _navigationWindows.OpenWindow(WindowType.BankWindow);
            });
        }

        private RelayCommand _openCatAndSubWindowCommand;
        public RelayCommand OpenCatAndSubWindowCommand
        {
            get => _openCatAndSubWindowCommand ??= new(obj =>
            {
                _navigationWindows.OpenWindow(WindowType.CatAndSubWindow);
            });
        }

        private RelayCommand _openFinancialRecordWindowCommand;
        public RelayCommand OpenFinancialRecordWindowCommand
        {
            get => _openFinancialRecordWindowCommand ??= new(obj =>
            {
                _navigationWindows.OpenWindow(WindowType.FinancialRecordWindow);
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

                GetSubcategoryFilter();

                OnPropertyChanged();
            }
        }

        private SubcategoryDTO? _selectedSubcategoryFilter;
        public SubcategoryDTO? SelectedSubcategoryFilter
        {
            get => _selectedSubcategoryFilter;
            set
            {
                _selectedSubcategoryFilter = value;

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

        #region Заполнение списка финансовых записей

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

        public ObservableCollection<SubcategoryDTO> SubcategoriesFilter { get; set; } = [];
        public async Task GetSubcategoryFilter()
        {
            SubcategoriesFilter.Clear();

            if (SelectedCategoryFilter is null) { return; }

            var list = _subcategoryService.GetIdUserIdCategorySub(CurrentUser.IdUser, SelectedCategoryFilter.IdCategory);

            foreach (var item in list)
            {
                SubcategoriesFilter.Add(item);
            }

            SubcategoriesFilter.Insert(0, new SubcategoryDTO()
            {
                IdSubcategory = 0,
                SubcategoryName = "<<Не выбрано!!>>",
            });

            SelectedSubcategoryFilter = SubcategoriesFilter.FirstOrDefault();
        }

        public ObservableCollection<AccountDTO> AccountsFilter { get; set; } = [];
        public async Task GetAccountFilter()
        {
            AccountsFilter.Clear();

            var list = await _accountService.GetAllAsync(CurrentUser.IdUser);

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

        #endregion

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
                SelectedSubcategoryFilter = SubcategoriesFilter.FirstOrDefault();
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
