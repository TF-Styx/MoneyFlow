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
        public int Create(string userName, string login, string password)
        {
            var userEntity = new User
            {
                UserName = userName,
                Login = login,
                Password = password
            };

            _context.Add(userEntity);
            _context.SaveChanges();

            return _context.Users.FirstOrDefault(x => x.Login == login).IdUser;
        }

        public async Task<List<UserDomain>> GetAllAsync()
        {
            var userList = new List<UserDomain>();
            var userEntity = await _context.Users.ToListAsync();

            foreach (var item in userEntity)
            {
                userList.Add(UserDomain.Create(item.IdUser, item.UserName, item.Avatar, item.Login, item.Password, item.IdGender).UserDomain);
            }

            return userList;
        }
        public List<UserDomain> GetAll()
        {
            var userList = new List<UserDomain>();
            var userEntity = _context.Users.ToList();

            foreach (var item in userEntity)
            {
                userList.Add(UserDomain.Create(item.IdUser, item.UserName, item.Avatar, item.Login, item.Password, item.IdGender).UserDomain);
            }

            return userList;
        }

        public async Task<UserDomain> GetAsync(int idUser)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.IdUser == idUser);
            var userDomain = UserDomain.Create(userEntity.IdUser, userEntity.UserName, userEntity.Avatar, userEntity.Login, userEntity.Password, userEntity.IdGender).UserDomain;

            return userDomain;
        }
        public UserDomain Get(int idUser)
        {
            var userEntity = _context.Users.FirstOrDefault(x => x.IdUser == idUser);
            var userDomain = UserDomain.Create(userEntity.IdUser, userEntity.UserName, userEntity.Avatar, userEntity.Login, userEntity.Password, userEntity.IdGender).UserDomain;

            return userDomain;
        }

        public async Task<UserDomain> GetAsync(string login)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Login == login);

            if (userEntity == null) { return null; }

            var userDomain = UserDomain.Create(userEntity.IdUser, userEntity.UserName, userEntity.Avatar, userEntity.Login, userEntity.Password, userEntity.IdGender).UserDomain;

            return userDomain;
        }
        public UserDomain Get(string login)
        {
            var userEntity = _context.Users.FirstOrDefault(x => x.Login == login);

            if (userEntity == null) { return null; }

            var userDomain = UserDomain.Create(userEntity.IdUser, userEntity.UserName, userEntity.Avatar, userEntity.Login, userEntity.Password, userEntity.IdGender).UserDomain;

            return userDomain;
        }

        public async Task<int> UpdateAsync(int idUser, string? userName, byte[]? avatar,
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
        public int Update(int idUser, string? userName, byte[]? avatar,
                                      string password, int? idGender)
        {
            var entity = _context.Users.FirstOrDefault(x => x.IdUser == idUser);

            entity.UserName = userName;
            entity.Avatar = avatar;
            entity.Password = password;
            entity.IdGender = idGender;

            _context.Users.Update(entity);
            _context.SaveChanges();

            return idUser;
        }

        public async Task DeleteAsync(int idUser)
        {
            await _context.Users.Where(x => x.IdUser == idUser).ExecuteDeleteAsync();
        }
        public void Delete(int idUser)
        {
            _context.Users.Where(x => x.IdUser == idUser).ExecuteDelete();
        }
    }
}
