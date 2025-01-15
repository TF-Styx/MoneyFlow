using Microsoft.Extensions.DependencyInjection;
using MoneyFlow.MVVM.Models.MSSQL_DB;
using MoneyFlow.MVVM.ViewModels.BaseVM;
using MoneyFlow.Utils.Commands;
using MoneyFlow.Utils.Helpers;
using MoneyFlow.Utils.Services.AuthorizationVerificationServices;
using MoneyFlow.Utils.Services.DataBaseServices;
using MoneyFlow.Utils.Services.NavigationServices;
using MoneyFlow.Utils.Services.NavigationServices.PageNavigationsService;
using System.Collections.ObjectModel;
using System.Windows;

namespace MoneyFlow.MVVM.ViewModels.WindowVM
{
    class FinancialRecordAddVM : BaseViewModel, IUpdatable
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly IAuthorizationVerificationService _authorizationVerificationService;
        private readonly IDataBaseService _dataBaseService;
        private readonly IPageNavigationService _pageNavigationService;

        public FinancialRecordAddVM(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _authorizationVerificationService = _serviceProvider.GetService<IAuthorizationVerificationService>();
            _dataBaseService = _serviceProvider.GetService<IDataBaseService>();
            _pageNavigationService = _serviceProvider.GetService<IPageNavigationService>();
        }

        public void Update(object parameter)
        {
            if (parameter is User user)
            {
                CurrentUser = user;

                GetCategoriesData();
                GetSubCategoriesData();
            }
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

        private string _recordName;
        public string RecordName
        {
            get => _recordName;
            set
            {
                _recordName = value;
                OnPropertyChanged();
            }
        }

        private decimal _amount;
        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        private DateTime _date = DateTime.Now;
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
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
                OnPropertyChanged();
            }
        }

        private Subcategory _selectedSubCategory;
        public Subcategory SelectedSubCategory
        {
            get => _selectedSubCategory;
            set
            {
                _selectedSubCategory = value;
                OnPropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Category> Categories { get; set; } = [];
        public ObservableCollection<Subcategory> Subcategories { get; set; } = [];


        private RelayCommand _addFinancialRecordCommand;
        public RelayCommand AddFinancialRecordCommand { get => _addFinancialRecordCommand ??= new(obj => { AddFinancialRecord(); }); }


        private async void AddFinancialRecord()
        {
            if (string.IsNullOrEmpty(RecordName) && Amount == 0 && SelectedCategory == null)
            {
                MessageBox.Show("Вы не заполнили поля!!");
                return;
            }

            FinancialRecord financialRecord = new FinancialRecord
            {
                RecordName = RecordName,
                Amount = Amount,
                Description = Description,
                Date = Date,
                IdCategory = SelectedCategory.IdCategory,
                IdSubcategory = SelectedSubCategory?.IdSubcategory,
            };

            await _dataBaseService.AddAsync(financialRecord);

            _pageNavigationService.RefreshData("FinanceJournal");

            //GetFinancialRecordData();
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

        private async void GetSubCategoriesData()
        {
            Subcategories.Clear();

            var subCategories = await DataBaseHelper.GetSubcategoriesByUserIdAsync(CurrentUser.IdUser);

            foreach (var item in subCategories)
            {
                Subcategories.Add(item);
            }
        }
    }
}
