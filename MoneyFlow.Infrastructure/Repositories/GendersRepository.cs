using Microsoft.EntityFrameworkCore;
using MoneyFlow.Domain.DomainModels;
using MoneyFlow.Domain.Interfaces.Repositories;
using MoneyFlow.Infrastructure.Context;
using MoneyFlow.Infrastructure.EntityModel;

namespace MoneyFlow.Infrastructure.Repositories
{
    public class GendersRepository : IGendersRepository
    {
        private readonly Func<ContextMF> _factory;

        public GendersRepository(Func<ContextMF> factor)
        {
            _factory = factor;
        }

        public async Task<int> CreateAsync(string genderName)
        {
            using (var context = _factory())
            {
                var genderEntity = new Gender
                {
                    GenderName = genderName,
                };

                await context.AddAsync(genderEntity);
                await context.SaveChangesAsync();

                return context.Genders.FirstOrDefault(x => x.GenderName == genderName).IdGender;
            }
        }
        public int Create(string genderName)
        {
            using (var context = _factory())
            {
                var genderEntity = new Gender
                {
                    GenderName = genderName,
                };

                context.Add(genderEntity);
                context.SaveChanges();

                return context.Genders.FirstOrDefault(x => x.GenderName == genderName).IdGender;
            }
        }

        public async Task<List<GenderDomain>> GetAllAsync()
        {
            using (var context = _factory())
            {
                var genderList = new List<GenderDomain>();
                var genderEntities = await context.Genders.ToListAsync();

                foreach (var item in genderEntities)
                {
                    genderList.Add(GenderDomain.Create(item.IdGender, item.GenderName).GenderDomain);
                }

                return genderList;
            }
        }
        public List<GenderDomain> GetAll()
        {
            using (var context = _factory())
            {
                var genderList = new List<GenderDomain>();
                var genderEntities = context.Genders.ToList();

                foreach (var item in genderEntities)
                {
                    genderList.Add(GenderDomain.Create(item.IdGender, item.GenderName).GenderDomain);
                }

                return genderList;
            }
        }

        public async Task<GenderDomain> GetAsync(int idGender)
        {
            using (var context = _factory())
            {
                var genderEntity = await context.Genders.FirstOrDefaultAsync(x => x.IdGender == idGender);
                var genderDomain = GenderDomain.Create(genderEntity.IdGender, genderEntity.GenderName).GenderDomain;

                return genderDomain;
            }
        }
        public GenderDomain Get(int idGender)
        {
            using (var context = _factory())
            {
                var genderEntity = context.Genders.FirstOrDefault(x => x.IdGender == idGender);
                var genderDomain = GenderDomain.Create(genderEntity.IdGender, genderEntity.GenderName).GenderDomain;

                return genderDomain;
            }
        }

        public async Task<GenderDomain> GetAsync(string genderName)
        {
            using (var context = _factory())
            {
                var genderEntity = await context.Genders.FirstOrDefaultAsync(x => x.GenderName.ToLower() == genderName.ToLower());

                if (genderEntity == null)
                {
                    return null;
                }

                var genderDomain = GenderDomain.Create(genderEntity.IdGender, genderEntity.GenderName).GenderDomain;

                return genderDomain;
            }
        }
        public GenderDomain Get(string genderName)
        {
            using (var context = _factory())
            {
                var genderEntity = context.Genders.FirstOrDefault(x => x.GenderName.ToLower() == genderName.ToLower());

                if (genderEntity == null)
                {
                    return null;
                }

                var genderDomain = GenderDomain.Create(genderEntity.IdGender, genderEntity.GenderName).GenderDomain;

                return genderDomain;
            }
        }

        public async Task<int> UpdateAsync(int idGender, string genderName)
        {
            using (var context = _factory())
            {
                var entity = await context.Genders.FirstOrDefaultAsync(x => x.IdGender == idGender);
                entity.GenderName = genderName;

                context.Update(entity);
                context.SaveChanges();

                return entity.IdGender;
            }
        }
        public int Update(int idGender, string genderName)
        {
            using (var context = _factory())
            {
                var entity = context.Genders.FirstOrDefault(x => x.IdGender == idGender);
                entity.GenderName = genderName;

                context.Update(entity);
                context.SaveChanges();

                return entity.IdGender;
            }
        }

        public async Task DeleteAsync(int idGender)
        {
            using (var context = _factory())
            {
                await context.Genders.Where(x => x.IdGender == idGender).ExecuteDeleteAsync();
            }
        }
        public void Delete(int idGender)
        {
            using (var context = _factory())
            {
                context.Genders.Where(x => x.IdGender == idGender).ExecuteDelete();
            }
        }
    }
}
