using Microsoft.EntityFrameworkCore;
using MoneyFlow.Domain.DomainModels;
using MoneyFlow.Domain.Interfaces.Repositories;
using MoneyFlow.Infrastructure.Context;
using MoneyFlow.Infrastructure.EntityModel;

namespace MoneyFlow.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly Func<ContextMF> _factory;

        public UsersRepository(Func<ContextMF> factory)
        {
            _factory = factory;
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<int> CreateAsync(string userName, string login, string password)
        {
            using (var context = _factory())
            {
                var userEntity = new User
                {
                    UserName = userName,
                    Login = login,
                    Password = password
                };

                await context.AddAsync(userEntity);
                await context.SaveChangesAsync();

                return context.Users.FirstOrDefault(x => x.Login == login).IdUser;
            }
        }
        public int Create(string userName, string login, string password)
        {
            using (var context = _factory())
            {
                var userEntity = new User
                {
                    UserName = userName,
                    Login = login,
                    Password = password
                };

                context.Add(userEntity);
                context.SaveChanges();

                return context.Users.FirstOrDefault(x => x.Login == login).IdUser;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<List<UserDomain>> GetAllAsync()
        {
            using (var context = _factory())
            {
                var userList = new List<UserDomain>();
                var userEntity = await context.Users.Include(x => x.IdGenderNavigation).ToListAsync();

                foreach (var item in userEntity)
                {
                    userList.Add(UserDomain.Create(item.IdUser, item.UserName, item.Avatar, item.Login, item.Password, item.IdGender).UserDomain);
                }

                return userList;
            }
        }
        public List<UserDomain> GetAll()
        {
            using (var context = _factory())
            {
                var userList = new List<UserDomain>();
                var userEntity = context.Users.Include(x => x.IdGenderNavigation).ToList();

                foreach (var item in userEntity)
                {
                    userList.Add(UserDomain.Create(item.IdUser, item.UserName, item.Avatar, item.Login, item.Password, item.IdGender).UserDomain);
                }

                return userList;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<UserDomain> GetAsync(int idUser)
        {
            using (var context = _factory())
            {
                var userEntity = await context.Users.Include(x => x.IdGenderNavigation).FirstOrDefaultAsync(x => x.IdUser == idUser);
                var userDomain = UserDomain.Create(userEntity.IdUser, userEntity.UserName, userEntity.Avatar, userEntity.Login, userEntity.Password, userEntity.IdGender).UserDomain;

                return userDomain;
            }
        }
        public UserDomain Get(int idUser)
        {
            using (var context = _factory())
            {
                var userEntity = context.Users.Include(x => x.IdGenderNavigation).FirstOrDefault(x => x.IdUser == idUser);
                var userDomain = UserDomain.Create(userEntity.IdUser, userEntity.UserName, userEntity.Avatar, userEntity.Login, userEntity.Password, userEntity.IdGender).UserDomain;

                return userDomain;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<UserDomain> GetAsync(string login)
        {
            using (var context = _factory())
            {
                var userEntity = await context.Users.Include(x => x.IdGenderNavigation).FirstOrDefaultAsync(x => x.Login == login);

                if (userEntity == null) { return null; }

                var userDomain = UserDomain.Create(userEntity.IdUser, userEntity.UserName, userEntity.Avatar, userEntity.Login, userEntity.Password, userEntity.IdGender).UserDomain;

                return userDomain;
            }
        }
        public UserDomain Get(string login)
        {
            using (var context = _factory())
            {
                var userEntity = context.Users.Include(x => x.IdGenderNavigation).FirstOrDefault(x => x.Login == login);

                if (userEntity == null) { return null; }

                var userDomain = UserDomain.Create(userEntity.IdUser, userEntity.UserName, userEntity.Avatar, userEntity.Login, userEntity.Password, userEntity.IdGender).UserDomain;

                return userDomain;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public UserTotalInfoDomain GetTotalUserInfo(int idUser)
        {
            using (var context = _factory())
            {
                var gender = context.Users.Include(x => x.IdGenderNavigation).FirstOrDefault(x => x.IdUser == idUser).IdGenderNavigation.GenderName;
                var totalBalance = context.Accounts.Where(x => x.IdUser == idUser).Sum(x => x.Balance);

                var accountCount = context.Accounts.Where(x => x.IdUser == idUser).Count();

                var banksId = context.Accounts.Where(x => x.IdUser == idUser).Select(x => x.IdBank).Distinct().ToList();
                var bankCount = context.Banks.Where(x => banksId.Contains(x.IdBank)).Count();

                var catCount = context.Categories.Where(x => x.IdUser == idUser).Count();

                var subcatCount = context.CatLinkSubs.Where(x => x.IdUser == idUser).Select(x => x.IdSubcategory).Count();

                var financialRecordCount = context.FinancialRecords.Where(x => x.IdUser == idUser).Count();


                var domain = UserTotalInfoDomain.Create(gender, totalBalance, accountCount, bankCount, catCount, subcatCount, financialRecordCount).UserTotalInfoDomain;

                return domain;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<int> UpdateAsync(int idUser, string? userName, byte[]? avatar,
                                      string password, int? idGender)
        {
            using (var context = _factory())
            {
                var entity = await context.Users.FirstOrDefaultAsync(x => x.IdUser == idUser);

                entity.UserName = userName;
                entity.Avatar = avatar;
                entity.Password = password;
                entity.IdGender = idGender;

                context.Users.Update(entity);
                context.SaveChanges();

                return idUser;
            }
        }
        public int Update(int idUser, string? userName, byte[]? avatar,
                                      string password, int? idGender)
        {
            using (var context = _factory())
            {
                var entity = context.Users.FirstOrDefault(x => x.IdUser == idUser);

                entity.UserName = userName;
                entity.Avatar = avatar;
                entity.Password = password;
                entity.IdGender = idGender;

                context.Users.Update(entity);
                context.SaveChanges();

                return idUser;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task DeleteAsync(int idUser)
        {
            using (var context = _factory())
            {
                await context.Users.Where(x => x.IdUser == idUser).ExecuteDeleteAsync();
            }
        }
        public void Delete(int idUser)
        {
            using (var context = _factory())
            {
                context.Users.Where(x => x.IdUser == idUser).ExecuteDelete();
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
