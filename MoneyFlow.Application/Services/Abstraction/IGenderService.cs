using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.Services.Abstraction
{
    public interface IGenderService
    {
        Task<(GenderDTO GenderDTO, string Message)> CreateGender(string genderName);
        Task DeleteGender(int idGender);
        Task<List<GenderDTO>> GetAllGender();
        Task<GenderDTO> GetGender(int id);
        Task<int> UpdateGender(int idGender, string genderName);
    }
}