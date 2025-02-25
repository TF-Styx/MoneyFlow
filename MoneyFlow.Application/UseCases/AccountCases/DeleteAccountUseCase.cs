using MoneyFlow.Application.UseCaseInterfaces.AccountCaseInterface;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.AccountCases
{
    public class DeleteAccountUseCase : IDeleteAccountUseCase
    {
        private readonly IAccountRepository _accountRepository;

        public DeleteAccountUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task DeleteAsyncAccount(int idAccount)
        {
            await _accountRepository.DeleteAsync(idAccount); // TODO : Сделать проверку на существование элемента
        }
        public void DeleteAccount(int idAccount)
        {
            _accountRepository.Delete(idAccount); // TODO : Сделать проверку на существование элемента
        }
    }
}
