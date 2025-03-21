using MoneyFlow.Application.UseCaseInterfaces.FinancialRecordCaseInterfaces;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.FinancialRecordCases
{
    public class DeleteFinancialRecordUseCase : IDeleteFinancialRecordUseCase
    {
        private readonly IFinancialRecordRepository _financialRecordRepository;

        public DeleteFinancialRecordUseCase(IFinancialRecordRepository financialRecordRepository)
        {
            _financialRecordRepository = financialRecordRepository;
        }

        public async Task DeleteAsync(int idFinancialRecord)
        {
            await _financialRecordRepository.DeleteAsync(idFinancialRecord);
        }
        public void Delete(int idFinancialRecord)
        {
            _financialRecordRepository.Delete(idFinancialRecord);
        }
    }
}
