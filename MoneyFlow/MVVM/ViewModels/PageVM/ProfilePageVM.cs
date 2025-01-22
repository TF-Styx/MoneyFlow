using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoneyFlow.MVVM.Models.DB_MSSQL;
using MoneyFlow.MVVM.ViewModels.BaseVM;
using MoneyFlow.Utils.Commands;
using MoneyFlow.Utils.Helpers;
using MoneyFlow.Utils.Services.AuthorizationVerificationServices;
using MoneyFlow.Utils.Services.DataBaseServices;
using MoneyFlow.Utils.Services.NavigationServices;
using System.Collections.ObjectModel;
using System.Windows.Media.Animation;

namespace MoneyFlow.MVVM.ViewModels.PageVM
{
    internal class ProfilePageVM : BaseViewModel, IUpdatable
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly IAuthorizationVerificationService _authorizationVerificationService;
        private readonly IDataBaseService _dataBaseService;

        private readonly LastRecordHelper _lastRecordHelper;

        public ProfilePageVM(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _authorizationVerificationService = _serviceProvider.GetService<IAuthorizationVerificationService>();
            _dataBaseService = _serviceProvider.GetService<IDataBaseService>();

            _lastRecordHelper = _serviceProvider.GetRequiredService<LastRecordHelper>();

            GetAccountTypesData();
        }

        public void Update(object parameter)
        {
            CurrentUser = _authorizationVerificationService.CurrentUser;

            GetCategoriesData();
            GetAccountsData();
        }

        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        private string _categoryName;
        public string CategoryName
        {
            get => _categoryName;
            set
            {
                _categoryName = value;
                OnPropertyChanged();
            }
        }

        private string _subcategoryName;
        public string SubcategoryName
        {
            get => _subcategoryName;
            set
            {
                _subcategoryName = value;
                OnPropertyChanged();
            }
        }

        private int? _numberAccount;
        public int? NumberAccount
        {
            get => _numberAccount;
            set
            {
                _numberAccount = value;
                OnPropertyChanged();
            }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;

                if (value == null) { return; }

                CategoryName = value.CategoryName;
                SubcategoryName = string.Empty;

                GetSubcategoriesData();
                OnPropertyChanged();
            }
        }

        private Subcategory _selectedSubcategory;
        public Subcategory SelectedSubcategory
        {
            get => _selectedSubcategory;
            set
            {
                _selectedSubcategory = value;

                if (value == null) { return; }

                SubcategoryName = value.SubcategoryName;

                OnPropertyChanged();
            }
        }

        private Account _selectedAccount;
        public Account SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;
                NumberAccount = value.NumberAccount;
                SelectedAccountTypes = AccountTypes.FirstOrDefault(x => x.IdAccountType == value.IdAccountType);
                OnPropertyChanged();
            }
        }

        private AccountType _selectedAccountTypes;
        public AccountType SelectedAccountTypes
        {
            get => _selectedAccountTypes;
            set
            {
                _selectedAccountTypes = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Category> Categories { get; set; } = [];
        public ObservableCollection<Subcategory> Subcategories { get; set; } = [];
        public ObservableCollection<Account> Accounts { get; set; } = [];
        public ObservableCollection<AccountType> AccountTypes { get; set; } = [];


        private RelayCommand _categoryAddCommand;
        public RelayCommand CategoryAddCommand { get => _categoryAddCommand ??= new(obj => { CategoryAdd(); }); }

        private RelayCommand _subcategoryAddCommand;
        public RelayCommand SubcategoryAddCommand { get => _subcategoryAddCommand ??= new(obj => { SubcategoryAdd(); }); }

        private RelayCommand _accountAddCommand;
        public RelayCommand AccountAddCommand { get => _accountAddCommand ??= new(obj => { AccountAdd(); }); }


        private async void CategoryAdd()
        {
            Category category = new Category()
            {
                CategoryName = CategoryName,
                IdUser = _authorizationVerificationService.CurrentUser.IdUser,
            };

            await _dataBaseService.AddAsync(category);
            Categories.Add(_lastRecordHelper.LastRecordCategory(CurrentUser));
        }

        private async void SubcategoryAdd()
        {
            Subcategory category = new Subcategory()
            {
                SubcategoryName = SubcategoryName,
                IdCategory = SelectedCategory.IdCategory,
            };

            await _dataBaseService.AddAsync(category);
            Subcategories.Add(_lastRecordHelper.LastRecordSubcategory(SelectedCategory));
        }

        private async void AccountAdd()
        {
            Account account = new Account()
            {
                NumberAccount = NumberAccount,
                IdUser = _authorizationVerificationService.CurrentUser.IdUser,
                IdAccountType = SelectedAccountTypes.IdAccountType,
            };

            await _dataBaseService.AddAsync(account);
            Accounts.Add(_lastRecordHelper.LastRecordAccount(CurrentUser));
        }

        private async void GetCategoriesData()
        {
            Categories.Clear();

            var categories = await _dataBaseService.GetDataTableAsync<Category>(x => x.Where(x => x.IdUser == CurrentUser.IdUser));

            foreach (var item in categories)
            {
                Categories.Add(item);
            }
        }

        private async void GetSubcategoriesData()
        {
            Subcategories.Clear();

            var subcategories = await _dataBaseService.GetDataTableAsync<Subcategory>(x => x.Where(x => x.IdCategory == SelectedCategory.IdCategory));

            foreach (var item in subcategories)
            {
                Subcategories.Add(item);
            }
        }

        private async void GetAccountsData()
        {
            Accounts.Clear();

            var account = await _dataBaseService.GetDataTableAsync<Account>(x => x.Where(x => x.IdUser == CurrentUser.IdUser));

            foreach (var item in account)
            {
                Accounts.Add(item);
            }
        }

        private async void GetAccountTypesData()
        {
            AccountTypes.Clear();

            var accountType = await _dataBaseService.GetDataTableAsync<AccountType>();

            foreach (var item in accountType)
            {
                AccountTypes.Add(item);
            }
        }
    }
}
