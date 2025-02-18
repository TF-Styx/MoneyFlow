using MoneyFlow.Application.UseCaseInterfaces.BankCaseInterfaces;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.BankCases
{
    public class DeleteBankUseCase : IDeleteBankUseCase
    {
        private readonly IBanksRepository _banksRepository;

        public DeleteBankUseCase(IBanksRepository banksRepository)
        {
            _banksRepository = banksRepository;
        }

        public async Task DeleteAsyncBank(int idBank)
        {
            await _banksRepository.DeleteAsync(idBank); // TODO : Сделать проверку на существование элемента
        }
        public void DeleteBank(int idBank)
        {
            _banksRepository.Delete(idBank); // TODO : Сделать проверку на существование элемента
        }
    }
}
