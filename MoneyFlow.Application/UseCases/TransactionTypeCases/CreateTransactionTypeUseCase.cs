using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Mappers;
using MoneyFlow.Application.UseCaseInterfaces.TransactionTypeCaseInterfaces;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.TransactionTypeCases
{
    public class CreateTransactionTypeUseCase : ICreateTransactionTypeUseCase
    {
        private readonly ITransactionTypeRepository _transactionTypeRepository;

        public CreateTransactionTypeUseCase(ITransactionTypeRepository transactionTypeRepository)
        {
            _transactionTypeRepository = transactionTypeRepository;
        }

        public async Task<(TransactionTypeDTO TransactionTypeDTO, string Message)> CreateAsyncTransactionType(string? transactionTypeName, string? description)
        {
            string message = string.Empty;

            if (string.IsNullOrWhiteSpace(transactionTypeName)) { return (null, "Вы не заполнили поля!!"); }

            var existTransactionType = await _transactionTypeRepository.GetAsync(transactionTypeName);

            if (existTransactionType != null) { return (null, "Такая транзакция уже есть!!"); }

            var idTransactionType = await _transactionTypeRepository.CreateAsync(transactionTypeName, description);
            var transactionTypeDomain = await _transactionTypeRepository.GetAsync(idTransactionType);

            return (transactionTypeDomain.ToDTO().TransactionTypeDTO, message);
        }
        public (TransactionTypeDTO TransactionTypeDTO, string Message) CreateTransactionType(string? transactionTypeName, string? description)
        {
            string message = string.Empty;

            if (string.IsNullOrWhiteSpace(transactionTypeName)) { return (null, "Вы не заполнили поля!!"); }

            var existTransactionType = _transactionTypeRepository.Get(transactionTypeName);

            if (existTransactionType != null) { return (null, "Такая транзакция уже есть!!"); }

            var idTransactionType = _transactionTypeRepository.Create(transactionTypeName, description);
            var transactionTypeDomain = _transactionTypeRepository.Get(idTransactionType);

            return (transactionTypeDomain.ToDTO().TransactionTypeDTO, message);
        }

    }
}
