using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Services.Abstraction;
using MoneyFlow.Application.UseCaseInterfaces.BankCaseInterfaces;

namespace MoneyFlow.Application.Services.Realization
{
    public class BankService : IBankService
    {
        private readonly ICreateBankUseCase _createBankUseCase;
        private readonly IDeleteBankUseCase _deleteBankUseCase;
        private readonly IGetBankUseCase _getBankUseCase;
        private readonly IUpdateBankUseCase _updateBankUseCase;

        public BankService(ICreateBankUseCase createBankUseCase, IDeleteBankUseCase deleteBankUseCase, IGetBankUseCase getBankUseCase, IUpdateBankUseCase updateBankUseCase)
        {
            _createBankUseCase = createBankUseCase;
            _deleteBankUseCase = deleteBankUseCase;
            _getBankUseCase = getBankUseCase;
            _updateBankUseCase = updateBankUseCase;
        }

        public async Task<(BankDTO BankDTO, string Message)> CreateBank(string bankName)
        {
            return await _createBankUseCase.CreateBank(bankName);
        }

        public async Task DeleteBank(int idBank)
        {
            await _deleteBankUseCase.DeleteBank(idBank);
        }

        public async Task<List<BankDTO>> GetAllBank()
        {
            return await _getBankUseCase.GetAllBank();
        }

        public async Task<BankDTO> GetBank(int id)
        {
            return await _getBankUseCase.GetBank(id);
        }

        public async Task<int> UpdateBank(int idBank, string bankName)
        {
            return await _updateBankUseCase.UpdateBank(idBank, bankName);
        }
    }
}
