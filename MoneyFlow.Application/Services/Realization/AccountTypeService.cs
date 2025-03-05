using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Services.Abstraction;
using MoneyFlow.Application.UseCaseInterfaces.AccountTypeCaseInterfaces;

namespace MoneyFlow.Application.Services.Realization
{
    public class AccountTypeService : IAccountTypeService
    {
        private readonly ICreateAccountTypeUseCase _createAccountTypeUseCase;
        private readonly IDeleteAccountTypeUseCase _deleteAccountTypeUseCase;
        private readonly IGetAccountTypeUseCase _getAccountTypeUseCase;
        private readonly IUpdateAccountTypeUseCase _updateAccountTypeUseCase;

        public AccountTypeService(ICreateAccountTypeUseCase createAccountTypeUseCase, IDeleteAccountTypeUseCase deleteAccountTypeUseCase, IGetAccountTypeUseCase getAccountTypeUseCase, IUpdateAccountTypeUseCase updateAccountTypeUseCase)
        {
            _createAccountTypeUseCase = createAccountTypeUseCase;
            _deleteAccountTypeUseCase = deleteAccountTypeUseCase;
            _getAccountTypeUseCase = getAccountTypeUseCase;
            _updateAccountTypeUseCase = updateAccountTypeUseCase;
        }

        public async Task<(AccountTypeDTO AccountTypeDTO, string Message)> CreateAsyncAccountType(string accountTypeName)
        {
            return await _createAccountTypeUseCase.CreateAsyncAccountType(accountTypeName);
        }
        public (AccountTypeDTO AccountTypeDTO, string Message) CreateAccountType(string accountTypeName)
        {
            return _createAccountTypeUseCase.CreateAccountType(accountTypeName);
        }

        public async Task<List<AccountTypeDTO>> GetAllAsyncAccountType()
        {
            return await _getAccountTypeUseCase.GetAllAsyncAccountType();
        }
        public List<AccountTypeDTO> GetAllAccountType()
        {
            return _getAccountTypeUseCase.GetAllAccountType();
        }

        public async Task<AccountTypeDTO> GetAsyncAccountType(int idAccountType)
        {
            return await _getAccountTypeUseCase.GetAsyncAccountType(idAccountType);
        }
        public AccountTypeDTO GetAccountType(int idAccountType)
        {
            return _getAccountTypeUseCase.GetAccountType(idAccountType);
        }

        public async Task<AccountTypeDTO> GetAsyncAccountType(string accountTypeName)
        {
            return await _getAccountTypeUseCase.GetAsyncAccountType(accountTypeName);
        }
        public AccountTypeDTO GetAccountType(string accountTypeName)
        {
            return _getAccountTypeUseCase.GetAccountType(accountTypeName);
        }

        public async Task<UserAccountTypesDTO> GetByIdUserAsync(int idUser)
        {
            return await _getAccountTypeUseCase.GetByIdUserAsync(idUser);
        }
        public UserAccountTypesDTO GetByIdUser(int idUser)
        {
            return _getAccountTypeUseCase.GetByIdUser(idUser);
        }

        public async Task<int> UpdateAsyncAccountType(int idAccountType, string accountTypeName)
        {
            return await _updateAccountTypeUseCase.UpdateAsyncAccountType(idAccountType, accountTypeName);
        }
        public int UpdateAccountType(int idAccountType, string accountTypeName)
        {
            return _updateAccountTypeUseCase.UpdateAccountType(idAccountType, accountTypeName);
        }

        public async Task DeleteAsyncAccountType(int idAccountType)
        {
            await _deleteAccountTypeUseCase.DeleteAsyncAccountType(idAccountType);
        }
        public void DeleteBank(int idAccountType)
        {
            _deleteAccountTypeUseCase.DeleteAccountType(idAccountType);
        }
    }
}
