using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Mappers;
using MoneyFlow.Application.UseCaseInterfaces.GenderCaseInterfaces;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.GenderCases
{
    public class GetGenderUseCase : IGetGenderUseCase
    {
        private readonly IGendersRepository _gendersRepository;

        public GetGenderUseCase(IGendersRepository gendersRepository)
        {
            _gendersRepository = gendersRepository;
        }

        public async Task<List<GenderDTO>> GetAllGender()
        {
            var genders = await _gendersRepository.GetAll();
            var gendersDTO = genders.ToListDTO();

            return gendersDTO;
        }

        public async Task<GenderDTO> GetGender(int id)
        {
            var gender = await _gendersRepository.Get(id);

            if (gender == null) { return null; } // TODO : Сделать исключение

            var genderDTO = gender.ToDTO();

            return genderDTO.GenderDTO;
        }
    }
}
