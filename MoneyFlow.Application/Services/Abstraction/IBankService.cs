using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.Services.Abstraction
{
    public interface IBankService
    {
        Task<(BankDTO BankDTO, string Message)> CreateBank(string bankName);
        Task DeleteBank(int idBank);
        Task<List<BankDTO>> GetAllBank();
        Task<BankDTO> GetBank(int id);
        Task<int> UpdateBank(int idBank, string bankName);
    }
}