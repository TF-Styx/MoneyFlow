using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Services.Abstraction;
using MoneyFlow.Application.UseCaseInterfaces.BankCaseInterfaces;

namespace MoneyFlow.Application.Services.Realization
{
    public class BankService : IBankService
    {
        private readonly ICreateBankUseCase _createBankUseCase;
        private readonly IDeleteBankUseCase _deleteBankUseCase;
        private readonly IGetBankUseCase    _getBankUseCase;
        private readonly IUpdateBankUseCase _updateBankUseCase;

        public BankService(ICreateBankUseCase createBankUseCase, IDeleteBankUseCase deleteBankUseCase, IGetBankUseCase getBankUseCase, IUpdateBankUseCase updateBankUseCase)
        {
            _createBankUseCase = createBankUseCase;
            _deleteBankUseCase = deleteBankUseCase;
            _getBankUseCase    = getBankUseCase;
            _updateBankUseCase = updateBankUseCase;
        }

        public async Task<(BankDTO BankDTO, string Message)> CreateAsyncBank(string bankName)
        {
            return await _createBankUseCase.CreateAsyncBank(bankName);
        }
        public (BankDTO BankDTO, string Message) CreateBank(string bankName)
        {
            return _createBankUseCase.CreateBank(bankName);
        }

        public async Task<List<BankDTO>> GetAllAsyncBank()
        {
            return await _getBankUseCase.GetAllAsyncBank();
        }
        public List<BankDTO> GetAllBank()
        {
            return _getBankUseCase.GetAllBank();
        }

        public async Task<BankDTO> GetAsyncBank(int idBank)
        {
            return await _getBankUseCase.GetAsyncBank(idBank);
        }
        public BankDTO GetBank(int idBank)
        {
            return _getBankUseCase.GetBank(idBank);
        }

        public async Task<BankDTO> GetAsyncBank(string bankName)
        {
            return await _getBankUseCase.GetAsyncBank(bankName);
        }
        public BankDTO GetBank(string bankName)
        {
            return _getBankUseCase.GetBank(bankName);
        }

        public async Task<UserBanksDTO> GetByIdUserAsync(int idUser)
        {
            return await _getBankUseCase.GetByIdUserAsync(idUser);
        }
        public UserBanksDTO GetByIdUser(int idUser)
        {
            return _getBankUseCase.GetByIdUser(idUser);
        }

        public async Task<int> UpdateAsyncBank(int idBank, string bankName)
        {
            return await _updateBankUseCase.UpdateAsyncBank(idBank, bankName);
        }
        public int UpdateBank(int idBank, string bankName)
        {
            return _updateBankUseCase.UpdateBank(idBank, bankName);
        }

        public async Task DeleteAsyncBank(int idBank)
        {
            await _deleteBankUseCase.DeleteAsyncBank(idBank);
        }
        public void DeleteBank(int idBank)
        {
            _deleteBankUseCase.DeleteBank(idBank);
        }
    }
}
