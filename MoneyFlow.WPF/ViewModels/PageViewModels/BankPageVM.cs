using MoneyFlow.Application.UseCaseInterfaces.BankCaseInterfaces;
using MoneyFlow.WPF.Enums;
using MoneyFlow.WPF.Interfaces;

namespace MoneyFlow.WPF.ViewModels.PageViewModels
{
    internal class BankPageVM : IUpdatable
    {
        private ICreateBankUseCase _createBankUseCase;
        private IDeleteBankUseCase _deleteBankUseCase;
        private IGetBankUseCase    _getBankUseCase;
        private IUpdateBankUseCase _updateBankUseCase;

        public BankPageVM(ICreateBankUseCase createBankUseCase,
                          IDeleteBankUseCase deleteBankUseCase,
                          IGetBankUseCase    getBankUseCase,
                          IUpdateBankUseCase updateBankUseCase)
        {
            _createBankUseCase = createBankUseCase;
            _deleteBankUseCase = deleteBankUseCase;
            _getBankUseCase    = getBankUseCase;
            _updateBankUseCase = updateBankUseCase;
        }

        public void Update(object parameter, TypeParameter typeParameter = TypeParameter.None)
        {
            
        }
    }
}
