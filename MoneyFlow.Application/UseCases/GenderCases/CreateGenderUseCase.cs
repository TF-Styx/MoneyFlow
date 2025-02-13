using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Mappers;
using MoneyFlow.Application.UseCaseInterfaces.GenderCaseInterfaces;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.GenderCases
{
    public class CreateGenderUseCase : ICreateGenderUseCase
    {
        private readonly IGendersRepository _genderRepository;

        public CreateGenderUseCase(IGendersRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public async Task<(GenderDTO GenderDTO, string Message)> CreateGender(string genderName)
        {
            string message = string.Empty;

            if (string.IsNullOrWhiteSpace(genderName))
            {
                return (null, "Вы не указали наименование пола!!");
            }

            var existGender = await _genderRepository.Get(genderName);

            if (existGender != null)
            {
                return (null, "Пол с таким именем уже есть!!");
            }

            var idGender = await _genderRepository.CreateAsync(genderName);
            var genderDomain = await _genderRepository.Get(idGender);

            return (genderDomain.ToDTO().GenderDTO, message);
        }
    }
}
