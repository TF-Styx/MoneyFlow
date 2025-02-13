using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.UseCaseInterfaces.GenderCaseInterfaces
{
    public interface ICreateGenderUseCase
    {
        Task<(GenderDTO GenderDTO, string Message)> CreateGender(string genderName);
    }
}