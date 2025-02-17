using Microsoft.EntityFrameworkCore;
using MoneyFlow.Domain.DomainModels;
using MoneyFlow.Domain.Interfaces.Repositories;
using MoneyFlow.Infrastructure.Context;
using MoneyFlow.Infrastructure.EntityModel;

namespace MoneyFlow.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ContextMF _context;

        public UsersRepository(ContextMF context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(string userName, string login, string password)
        {
            var userEntity = new User
            {
                UserName = userName,
                Login = login,
                Password = password
            };

            await _context.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return _context.Users.FirstOrDefault(x => x.Login == login).IdUser;
        }

        public async Task<List<UserDomain>> GetAll()
        {
            var userList = new List<UserDomain>();
            var userEntity = await _context.Users.ToListAsync();

            foreach (var item in userList)
            {
                userList.Add(new UserDomain()
                {
                    IdUser = item.IdUser,
                    UserName = item.UserName,
                    Avatar = item.Avatar,
                    Login = item.Login,
                    Password = item.Password,
                    IdGender = item.IdGender,
                });
            }

            return userList;
        }

        public async Task<UserDomain> Get(int idUser)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.IdUser == idUser);
            var userDomain = new UserDomain()
            {
                IdUser = userEntity.IdUser,
                UserName = userEntity.UserName,
                Avatar = userEntity.Avatar,
                Login = userEntity.Login,
                Password = userEntity.Password,
                IdGender = userEntity.IdGender,
            };

            return userDomain;
        }

        public async Task<UserDomain> Get(string login)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Login == login);

            if (userEntity == null) { return null; }

            var userDomain = new UserDomain()
            {
                IdUser = userEntity.IdUser,
                UserName = userEntity.UserName,
                Avatar = userEntity.Avatar,
                Login = userEntity.Login,
                Password = userEntity.Password,
                IdGender = userEntity.IdGender,
            };

            return userDomain;
        }

        public async Task<int> Update(int idUser, string? userName, byte[]? avatar,
                                      string password, int? idGender)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(x => x.IdUser == idUser);

            entity.UserName = userName;
            entity.Avatar = avatar;
            entity.Password = password;
            entity.IdGender = idGender;

            _context.Users.Update(entity);
            _context.SaveChanges();

            return idUser;
        }

        public async Task Delete(int idUser)
        {
            await _context.Users.Where(x => x.IdUser == idUser).ExecuteDeleteAsync();
        }
    }
}
