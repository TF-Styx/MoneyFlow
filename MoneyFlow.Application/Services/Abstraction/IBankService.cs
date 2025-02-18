using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.Services.Abstraction
{
    public interface IBankService
    {
        Task<(BankDTO BankDTO, string Message)> CreateAsyncBank(string bankName);
        (BankDTO BankDTO, string Message) CreateBank(string bankName);

        Task<List<BankDTO>> GetAllAsyncBank();
        List<BankDTO> GetAllBank();

        Task<BankDTO> GetAsyncBank(int idBank);
        BankDTO GetBank(int idBank);

        Task<BankDTO> GetAsyncBank(string bankName);
        BankDTO GetBank(string bankName);

        Task<int> UpdateAsyncBank(int idBank, string bankName);
        int UpdateBank(int idBank, string bankName);

        Task DeleteAsyncBank(int idBank);
        void DeleteBank(int idBank);
    }
}