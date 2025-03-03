using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.UseCaseInterfaces.BankCaseInterfaces
{
    public interface IGetBankUseCase
    {
        Task<List<BankDTO>> GetAllAsyncBank();
        List<BankDTO> GetAllBank();

        Task<BankDTO> GetAsyncBank(int idBank);
        BankDTO GetBank(int idBank);

        Task<BankDTO> GetAsyncBank(string nameBank);
        BankDTO GetBank(string nameBank);

        Task<UserBanksDTO> GetByIdUserAsync(int idUser);
        UserBanksDTO GetByIdUser(int idUser);

    }
}