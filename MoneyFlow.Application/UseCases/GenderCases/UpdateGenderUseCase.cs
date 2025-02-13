using MoneyFlow.Application.UseCaseInterfaces.GenderCaseInterfaces;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.GenderCases
{
    public class UpdateGenderUseCase : IUpdateGenderUseCase
    {
        private readonly IGendersRepository _gendersRepository;

        public UpdateGenderUseCase(IGendersRepository gendersRepository)
        {
            _gendersRepository = gendersRepository;
        }

        public async Task<int> UpdateGender(int idGender, string genderName)
        {
            if (string.IsNullOrWhiteSpace(genderName))
            {
                throw new Exception("Данного пола не существует!!");
            }

            var existGender = await _gendersRepository.Get(idGender);

            if (existGender == null)
            {
                throw new Exception("Данного пола не существует!!");
            }

            return await _gendersRepository.Update(idGender, genderName);
        }
    }
}
