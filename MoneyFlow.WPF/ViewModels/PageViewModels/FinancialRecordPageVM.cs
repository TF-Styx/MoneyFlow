using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Services.Abstraction;
using MoneyFlow.Application.UseCaseInterfaces.FinancialRecordViewingInterfaces;
using MoneyFlow.WPF.Commands;
using MoneyFlow.WPF.Enums;
using MoneyFlow.WPF.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;

namespace MoneyFlow.WPF.ViewModels.PageViewModels
{
    internal class FinancialRecordPageVM : BaseViewModel, IUpdatable
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IAccountService _accountService;
        private readonly ICategoryService _categoryService;
        private readonly ISubcategoryService _subcategoryService;
        private readonly ITransactionTypeService _transactionTypeService;
        private readonly IFinancialRecordService _financialRecordService;
        private readonly IGetFinancialRecordViewingUseCase _getFinancialRecordViewingUseCase;

        private readonly INavigationPages _navigationPages;

        public FinancialRecordPageVM(   IAuthorizationService authorizationService, 
                                        IAccountService accountService,
                                        ICategoryService categoryService,
                                        ISubcategoryService subcategoryService,
                                        ITransactionTypeService transactionTypeService,
                                        IFinancialRecordService financialRecordService, 
                                        IGetFinancialRecordViewingUseCase getFinancialRecordViewingUseCase,
                                        INavigationPages navigationPages)
        {
            _authorizationService = authorizationService;
            _accountService = accountService;
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
            _transactionTypeService = transactionTypeService;
            _financialRecordService = financialRecordService;
            _getFinancialRecordViewingUseCase = getFinancialRecordViewingUseCase;

            _navigationPages = navigationPages;

            CurrentUser = _authorizationService.CurrentUser;

            GetFinancialRecord();

            GetTransactionType();
            GetCategory();
            GetAccount();
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

        #region Поля финансовой записи

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

        private decimal? _amount;
        public decimal? Amount
        {
            get => _amount;
            set
            {
                _amount = value;
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

        private TransactionTypeDTO _selectedTransactionType;
        public TransactionTypeDTO SelectedTransactionType
        {
            get => _selectedTransactionType;
            set
            {
                _selectedTransactionType = value;

                if (value == null) { return; }

                OnPropertyChanged();
            }
        }

        private CategoryDTO _selectedCategory;
        public CategoryDTO SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;

                if (value == null) { return; }

                GetSubcategoryByIdCategory();

                OnPropertyChanged();
            }
        }

        private SubcategoryDTO _selectedSubcategory;
        public SubcategoryDTO SelectedSubcategory
        {
            get => _selectedSubcategory;
            set
            {
                _selectedSubcategory = value;

                if (value == null) { return; }

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

                if (value == null) { return; }

                OnPropertyChanged();
            }
        }

        private DateTime? _date;
        public DateTime? Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        #endregion


        private FinancialRecordViewingDTO _selectedFinancialRecord;
        public FinancialRecordViewingDTO SelectedFinancialRecord
        {
            get => _selectedFinancialRecord;
            set
            {
                _selectedFinancialRecord = value;

                if (value == null) { return; }

                GetFinancialRecordById(value.IdFinancialRecord);

                OnPropertyChanged();
            }
        }


        private FinancialRecordDTO _currentSelectedFinancialRecord;


        private async void GetFinancialRecordById(int idFinancialRecord)
        {
            _currentSelectedFinancialRecord = await _financialRecordService.GetAsyncFinancialRecord(idFinancialRecord);

            RecordName = _currentSelectedFinancialRecord.RecordName;
            Amount = _currentSelectedFinancialRecord.Amount;
            Description = _currentSelectedFinancialRecord.Description;
            SelectedTransactionType = TransactionTypes.FirstOrDefault(x => x.IdTransactionType == _currentSelectedFinancialRecord.IdTransactionType);
            SelectedCategory = Categories.FirstOrDefault(x => x.IdCategory == _currentSelectedFinancialRecord.IdCategory);
            //SelectedSubcategory = Subcategories.FirstOrDefault(x => x.IdSubcategory == _currentSelectedFinancialRecord.IdS);
            SelectedAccount = Accounts.FirstOrDefault(x => x.IdAccount == _currentSelectedFinancialRecord.IdAccount);
            Date = _currentSelectedFinancialRecord.Date;
        }



        // ------------------------------------------------------------------------------------------------------------------------------------------------


        #region Типы операций

        public ObservableCollection<TransactionTypeDTO> TransactionTypes { get; set; } = [];

        public async void GetTransactionType()
        {
            TransactionTypes.Clear();

            var list = await _transactionTypeService.GetAllAsyncTransactionType();

            foreach (var item in list)
            {
                TransactionTypes.Add(item);
            }
        }

        #endregion


        // ------------------------------------------------------------------------------------------------------------------------------------------------


        #region Категории

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

        #endregion


        // ------------------------------------------------------------------------------------------------------------------------------------------------


        #region Подкатегории

        public ObservableCollection<SubcategoryDTO> Subcategories { get; set; } = [];

        private async void GetSubcategoryByIdCategory()
        {
            Subcategories.Clear();

            var list = _subcategoryService.GetIdUserIdCategorySub(CurrentUser.IdUser, SelectedCategory.IdCategory);
            //var list = _subcategoryService.GetAllIdUserSub(CurrentUser.IdUser);

            foreach (var item in list)
            {
                Subcategories.Add(item);
            }
        }

        #endregion


        // ------------------------------------------------------------------------------------------------------------------------------------------------


        #region Счета

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


        // ------------------------------------------------------------------------------------------------------------------------------------------------


        #region Финансовые записи

        public ObservableCollection<FinancialRecordViewingDTO> FinancialRecords { get; set; } = [];

        public async void GetFinancialRecord()
        {
            FinancialRecords.Clear();

            var list = await _getFinancialRecordViewingUseCase.GetAllAsyncFinancialRecordViewing(CurrentUser.IdUser);

            foreach (var item in list)
            {
                FinancialRecords.Add(item);
            }
        }

        #endregion


        // ------------------------------------------------------------------------------------------------------------------------------------------------

        #region Команды

        
        private RelayCommand _financialRecordAddCommand;
        public RelayCommand FinancialRecordAddCommand
        {
            get => _financialRecordAddCommand ??= new(async obj =>
            {
                var newRecord = await _financialRecordService.CreateAsyncFinancialRecord(RecordName, Amount, Description, SelectedTransactionType.IdTransactionType, CurrentUser.IdUser, SelectedCategory.IdCategory, SelectedAccount.IdAccount, Date);

                if (newRecord.Message != string.Empty)
                {
                    MessageBox.Show(newRecord.Message);
                    return;
                }

                var record = _getFinancialRecordViewingUseCase.GetById(CurrentUser.IdUser, newRecord.FinancialRecordDTO.IdFinancialRecord, SelectedCategory.IdCategory, SelectedSubcategory.IdSubcategory);

                FinancialRecords.Add(record);

                _navigationPages.TransitObject(PageType.UserPage, record, ParameterType.Add);
            });
        }

        private RelayCommand _financialRecordUpdateCommand;
        public RelayCommand FinancialRecordUpdateCommand
        {
            get => _financialRecordUpdateCommand ??= new(async obj =>
            {
                var idUpdateRecord = await _financialRecordService.UpdateAsyncFinancialRecord
                    (
                        SelectedFinancialRecord.IdFinancialRecord,
                        RecordName,
                        Amount,
                        Description,
                        SelectedTransactionType.IdTransactionType,
                        CurrentUser.IdUser,
                        SelectedCategory.IdCategory,
                        SelectedAccount.IdAccount,
                        Date
                    );

                var updateFinancialRecord = FinancialRecords
                    .FirstOrDefault(x => x.IdFinancialRecord == SelectedFinancialRecord.IdFinancialRecord)
                        .SetProperty(x =>
                        {
                            x.IdFinancialRecord = idUpdateRecord;
                            x.RecordName = RecordName;
                            x.Amount = Amount;
                            x.Description = Description;
                            x.TransactionTypeName = SelectedTransactionType.TransactionTypeName;
                            x.IdUser = CurrentUser.IdUser;
                            x.CategoryName = SelectedCategory.CategoryName;
                            x.AccountNumber = SelectedAccount.NumberAccount;
                            x.Date = Date;
                        });

                var index = FinancialRecords.IndexOf(updateFinancialRecord);

                FinancialRecords.RemoveAt(index);
                FinancialRecords.Insert(index, updateFinancialRecord);

                _navigationPages.TransitObject(PageType.UserPage, updateFinancialRecord, ParameterType.Update);
            });
        }

        private RelayCommand _financialRecordDeleteCommand;
        public RelayCommand FinancialRecordDeleteCommand
        {
            get => _financialRecordDeleteCommand ??= new(async obj =>
            {
                await _financialRecordService.DeleteAsyncFinancialRecord(SelectedFinancialRecord.IdFinancialRecord);

                _navigationPages.TransitObject(PageType.UserPage, SelectedFinancialRecord, ParameterType.Delete);

                RecordName = string.Empty;
                Amount = 0;
                Description = string.Empty;
                SelectedTransactionType = null;
                SelectedCategory = null;
                SelectedAccount = null;
                Date = DateTime.Now;

                FinancialRecords.Remove(SelectedFinancialRecord);
            });
        }

        #endregion


        // ------------------------------------------------------------------------------------------------------------------------------------------------


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
