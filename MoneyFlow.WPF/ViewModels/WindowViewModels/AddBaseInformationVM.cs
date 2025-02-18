using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Services.Abstraction;
using MoneyFlow.Application.UseCaseInterfaces.BankCaseInterfaces;
using MoneyFlow.Application.UseCaseInterfaces.GenderCaseInterfaces;
using MoneyFlow.WPF.Commands;
using MoneyFlow.WPF.Enums;
using MoneyFlow.WPF.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace MoneyFlow.WPF.ViewModels.WindowViewModels
{
    internal class AddBaseInformationVM : BaseViewModel, IUpdatable
    {
        private readonly IBankService _bankService;
        private readonly IGenderService _genderService;

        //private readonly ICreateBankUseCase _createBankUseCase;
        //private readonly IDeleteBankUseCase _deleteBankUseCase;
        //private readonly IGetBankUseCase    _getBankUseCase;
        //private readonly IUpdateBankUseCase _updateBankUseCase;

        //private readonly ICreateGenderUseCase _createGenderUseCase;
        //private readonly IDeleteGenderUseCase _deleteGenderUseCase;
        //private readonly IGetGenderUseCase    _getGenderUseCase;
        //private readonly IUpdateGenderUseCase _updateGenderUseCase;

        //ICreateBankUseCase createBankUseCase, IDeleteBankUseCase deleteBankUseCase, IGetBankUseCase getBankUseCase, IUpdateBankUseCase updateBankUseCase,
        //ICreateGenderUseCase createGenderUseCase, IDeleteGenderUseCase deleteGenderUseCase, IGetGenderUseCase getGenderUseCase, IUpdateGenderUseCase updateGenderUseCase
        public AddBaseInformationVM(IBankService bankService, IGenderService genderService)
        {
            _bankService = bankService;
            _genderService = genderService;
            GetBanks();
            GetGenders();
        }

        public void Update(object parameter, TypeParameter typeParameter = TypeParameter.None)
        {
            
        }



        #region Банк || string:BankName || Observable:Banks || BankDTO:SeletedBank

        private string _bankName;
        public string BankName
        {
            get => _bankName;
            set
            {
                _bankName = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<BankDTO> Banks { get; set; } = [];

        private async void GetBanks()
        {
            Banks.Clear();

            var list = await _bankService.GetAllBank();

            foreach (var item in list)
            {
                Banks.Add(item);
            }
        }

        private BankDTO _selectedBank;
        public BankDTO SelectedBank
        {
            get => _selectedBank;
            set
            {
                _selectedBank = value;
                if (value == null) { BankName = string.Empty; return; }

                BankName = value.BankName;

                OnPropertyChanged();
            }
        }


        private RelayCommand _bankAddCommand;
        public RelayCommand BankAddCommand
        {
            get => _bankAddCommand ??= new(async obj =>
            {
                var newBank = await _bankService.CreateAsyncBank(BankName);
                if (newBank.Message != string.Empty)
                {
                    MessageBox.Show(newBank.Message);
                    return;
                }
                Banks.Add(newBank.BankDTO);
            });
        }

        private RelayCommand _bankUpdateCommand;
        public RelayCommand BankUpdateCommand
        {
            get => _bankUpdateCommand ??= new(async obj =>
            {
                var idUpdatableBank = await _bankService.UpdateBank(SelectedBank.IdBank, BankName);
                var updatableBank = Banks.FirstOrDefault(x => x.IdBank == SelectedBank.IdBank)
                                         .SetProperty(x => { x.IdBank = idUpdatableBank; x.BankName = BankName; });
                var index = Banks.IndexOf(updatableBank);

                Banks.RemoveAt(index);
                Banks.Insert(index, updatableBank);
            });
        }

        private RelayCommand _bankDeleteCommand;
        public RelayCommand BankDeleteCommand
        {
            get => _bankDeleteCommand ??= new(async obj =>
            {
                await _bankService.DeleteAsyncBank(SelectedBank.IdBank);
                Banks.Remove(SelectedBank);
            });
        }

        #endregion


        #region Gender || string:GenderName || Observable:Genders || GenderDTO:SeletedGender

        private string _genderName;
        public string GenderName
        {
            get => _genderName;
            set
            {
                _genderName = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<GenderDTO> Genders { get; set; } = [];

        private async void GetGenders()
        {
            Genders.Clear();

            var list = await _genderService.GetAllAsyncGender();

            foreach (var item in list)
            {
                Genders.Add(item);
            }
        }

        private GenderDTO _selectedGender;
        public GenderDTO SelectedGender
        {
            get => _selectedGender;
            set
            {
                _selectedGender = value;
                if (value == null) { GenderName = string.Empty; return; }

                GenderName = value.GenderName;

                OnPropertyChanged();
            }
        }


        private RelayCommand _GenderAddCommand;
        public RelayCommand GenderAddCommand
        {
            get => _GenderAddCommand ??= new(async obj =>
            {
                var newGender = await _genderService.CreateAsyncGender(GenderName);
                if (newGender.Message != string.Empty)
                {
                    MessageBox.Show(newGender.Message);
                    return;
                }
                Genders.Add(newGender.GenderDTO);
            });
        }

        private RelayCommand _genderUpdateCommand;
        public RelayCommand GenderUpdateCommand
        {
            get => _genderUpdateCommand ??= new(async obj =>
            {
                var idUpdatableGender = await _genderService.UpdateAsyncGender(SelectedGender.IdGender, GenderName);
                var updatableGender = Genders.FirstOrDefault(x => x.IdGender == SelectedGender.IdGender)
                                             .SetProperty(x => { x.IdGender = idUpdatableGender; x.GenderName = GenderName; });
                var index = Genders.IndexOf(updatableGender);

                Genders.RemoveAt(index);
                Genders.Insert(index, updatableGender);
            });
        }

        private RelayCommand _GenderDeleteCommand;
        public RelayCommand GenderDeleteCommand
        {
            get => _GenderDeleteCommand ??= new(async obj =>
            {
                await _genderService.DeleteAsyncGender(SelectedGender.IdGender);
                Genders.Remove(SelectedGender);
            });
        }

        #endregion
    }
}