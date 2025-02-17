using MoneyFlow.Application.UseCaseInterfaces.UserCaseInterfaces;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.UserCases
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IUsersRepository _usersRepository;

        public UpdateUserUseCase(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<int> UpdateUser(int idUser, string? userName, byte[]? avatar,
                                      string password, int? idGender)
        {
            var existUser = await _usersRepository.Get(idUser);

            if (existUser == null)
            {
                throw new Exception("Данного пользователя не существует!!");
            }

            return await _usersRepository.Update(idUser, userName, avatar, password, idGender);
        }
    }
}
