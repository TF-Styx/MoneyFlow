namespace MoneyFlow.Application.UseCaseInterfaces.GenderCaseInterfaces
{
    public interface IUpdateGenderUseCase
    {
        Task<int> UpdateGender(int idGender, string genderName);
    }
}