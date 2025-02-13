using MoneyFlow.Application.UseCaseInterfaces.GenderCaseInterfaces;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.GenderCases
{
    public class DeleteGenderUseCase : IDeleteGenderUseCase
    {
        private readonly IGendersRepository _gendersRepository;

        public DeleteGenderUseCase(IGendersRepository gendersRepository)
        {
            _gendersRepository = gendersRepository;
        }

        public async Task DeleteGender(int idGender)
        {
            await _gendersRepository.Delete(idGender); // TODO : Сделать проверки на существование элемента
        }
    }
}
