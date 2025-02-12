using MoneyFlow.Application.UseCaseInterfaces.BankCaseInterfaces;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.BankCases
{
    public class UpdateBankUseCase : IUpdateBankUseCase
    {
        private readonly IBanksRepository _banksRepository;

        public UpdateBankUseCase(IBanksRepository banksRepository)
        {
            _banksRepository = banksRepository;
        }

        public async Task<int> UpdateBank(int idBank, string bankName)
        {
            if (string.IsNullOrWhiteSpace(bankName))
            {
                throw new Exception("Данного банка не существует!!");
            }

            var exist = await _banksRepository.Get(idBank);

            if (exist == null)
            {
                throw new Exception("Данного банка не существует!!");
            }

            return await _banksRepository.Update(idBank, bankName);
        }
    }
}
