using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Services.Abstraction;
using MoneyFlow.Application.UseCaseInterfaces.GenderCaseInterfaces;

namespace MoneyFlow.Application.Services.Realization
{
    public class GenderService : IGenderService
    {
        private readonly ICreateGenderUseCase _createGenderUseCase;
        private readonly IDeleteGenderUseCase _deleteGenderUseCase;
        private readonly IGetGenderUseCase    _getGenderUseCase;
        private readonly IUpdateGenderUseCase _updateGenderUseCase;

        public GenderService(ICreateGenderUseCase createGenderUseCase, IDeleteGenderUseCase deleteGenderUseCase, IGetGenderUseCase getGenderUseCase, IUpdateGenderUseCase updateGenderUseCase)
        {
            _createGenderUseCase = createGenderUseCase;
            _deleteGenderUseCase = deleteGenderUseCase;
            _getGenderUseCase    = getGenderUseCase;
            _updateGenderUseCase = updateGenderUseCase;
        }

        public async Task<(GenderDTO GenderDTO, string Message)> CreateGender(string genderName)
        {
            return await _createGenderUseCase.CreateGender(genderName);
        }

        public async Task DeleteGender(int idGender)
        {
            await _deleteGenderUseCase.DeleteGender(idGender);
        }

        public async Task<List<GenderDTO>> GetAllGender()
        {
            return await _getGenderUseCase.GetAllGender();
        }

        public async Task<GenderDTO> GetGender(int id)
        {
            return await _getGenderUseCase.GetGender(id);
        }

        public async Task<int> UpdateGender(int idGender, string genderName)
        {
            return await _updateGenderUseCase.UpdateGender(idGender, genderName);
        }
    }
}
