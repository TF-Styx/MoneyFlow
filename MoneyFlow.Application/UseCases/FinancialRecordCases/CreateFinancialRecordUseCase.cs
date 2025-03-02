using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Mappers;
using MoneyFlow.Application.UseCaseInterfaces.FinancialRecordCaseInterfaces;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.FinancialRecordCases
{
    public class CreateFinancialRecordUseCase : ICreateFinancialRecordUseCase
    {
        private readonly IFinancialRecordRepository _financialRecordRepository;

        public CreateFinancialRecordUseCase(IFinancialRecordRepository financialRecordRepository)
        {
            _financialRecordRepository = financialRecordRepository;
        }

        public async Task<(FinancialRecordDTO FinancialRecordDTO, string Message)> CreateAsyncFinancialRecord(string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date)
        {
            var message = string.Empty;

            if (string.IsNullOrWhiteSpace(recordName))
            {
                return (null, "Вы не заполнили поля!!");
            }

            var id = await _financialRecordRepository.CreateAsync(recordName, amount, description, idTransactionType, idUser, idCategory, idAccount, date);
            var domain = await _financialRecordRepository.GetAsync(id);

            return (domain.ToDTO().FinancialRecordDTO, message);
        }
        public (FinancialRecordDTO FinancialRecordDTO, string Message) CreateFinancialRecord(string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date)
        {
            var message = string.Empty;

            if (string.IsNullOrWhiteSpace(recordName))
            {
                return (null, "Вы не заполнили поля!!");
            }

            var id = _financialRecordRepository.Create(recordName, amount, description, idTransactionType, idUser, idCategory, idAccount, date);
            var domain = _financialRecordRepository.Get(id);

            return (domain.ToDTO().FinancialRecordDTO, message);
        }
    }
}
