using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.UseCaseInterfaces.BankCaseInterfaces
{
    public interface IGetBankUseCase
    {
        Task<List<BankDTO>> GetAllBank();
        Task<BankDTO> GetBank(int id);
    }
}