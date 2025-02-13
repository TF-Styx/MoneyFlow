using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.UseCaseInterfaces.GenderCaseInterfaces
{
    public interface IGetGenderUseCase
    {
        Task<List<GenderDTO>> GetAllGender();
        Task<GenderDTO> GetGender(int id);
    }
}