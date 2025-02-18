using Microsoft.EntityFrameworkCore;
using MoneyFlow.Domain.DomainModels;
using MoneyFlow.Domain.Interfaces.Repositories;
using MoneyFlow.Infrastructure.Context;
using MoneyFlow.Infrastructure.EntityModel;

namespace MoneyFlow.Infrastructure.Repositories
{
    public class GendersRepository : IGendersRepository
    {
        private readonly ContextMF _context;

        public GendersRepository(ContextMF context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(string genderName)
        {
            var genderEntity = new Gender
            {
                GenderName = genderName,
            };

            await _context.AddAsync(genderEntity);
            await _context.SaveChangesAsync();

            return _context.Genders.FirstOrDefault(x => x.GenderName == genderName).IdGender;
        }
        public int Create(string genderName)
        {
            var genderEntity = new Gender
            {
                GenderName = genderName,
            };

            _context.Add(genderEntity);
            _context.SaveChanges();

            return _context.Genders.FirstOrDefault(x => x.GenderName == genderName).IdGender;
        }

        public async Task<List<GenderDomain>> GetAllAsync()
        {
            var genderList = new List<GenderDomain>();
            var genderEntities = await _context.Genders.ToListAsync();

            foreach (var item in genderEntities)
            {
                genderList.Add(new GenderDomain()
                {
                    IdGender = item.IdGender,
                    GenderName = item.GenderName,
                });
            }

            return genderList;
        }
        public List<GenderDomain> GetAll()
        {
            var genderList = new List<GenderDomain>();
            var genderEntities = _context.Genders.ToList();

            foreach (var item in genderEntities)
            {
                genderList.Add(new GenderDomain()
                {
                    IdGender = item.IdGender,
                    GenderName = item.GenderName,
                });
            }

            return genderList;
        }

        public async Task<GenderDomain> GetAsync(int idGender)
        {
            var genderEntity = await _context.Genders.FirstOrDefaultAsync(x => x.IdGender == idGender);
            var genderDomain = new GenderDomain()
            {
                IdGender = genderEntity.IdGender,
                GenderName = genderEntity.GenderName,
            };

            return genderDomain;
        }
        public GenderDomain Get(int idGender)
        {
            var genderEntity = _context.Genders.FirstOrDefault(x => x.IdGender == idGender);
            var genderDomain = new GenderDomain()
            {
                IdGender = genderEntity.IdGender,
                GenderName = genderEntity.GenderName,
            };

            return genderDomain;
        }

        public async Task<GenderDomain> GetAsync(string genderName)
        {
            var genderEntity = await _context.Genders.FirstOrDefaultAsync(x => x.GenderName.ToLower() == genderName.ToLower());

            if (genderEntity == null)
            {
                return null;
            }

            var genderDomain = new GenderDomain()
            {
                IdGender = genderEntity.IdGender,
                GenderName = genderEntity.GenderName,
            };

            return genderDomain;
        }
        public GenderDomain Get(string genderName)
        {
            var genderEntity = _context.Genders.FirstOrDefault(x => x.GenderName.ToLower() == genderName.ToLower());

            if (genderEntity == null)
            {
                return null;
            }

            var genderDomain = new GenderDomain()
            {
                IdGender = genderEntity.IdGender,
                GenderName = genderEntity.GenderName,
            };

            return genderDomain;
        }

        public async Task<int> UpdateAsync(int idGender, string genderName)
        {
            var entity = await _context.Genders.FirstOrDefaultAsync(x => x.IdGender == idGender);
            entity.GenderName = genderName;

            _context.Update(entity);
            _context.SaveChanges();

            return entity.IdGender;
        }
        public int Update(int idGender, string genderName)
        {
            var entity = _context.Genders.FirstOrDefault(x => x.IdGender == idGender);
            entity.GenderName = genderName;

            _context.Update(entity);
            _context.SaveChanges();

            return entity.IdGender;
        }

        public async Task DeleteAsync(int idGender)
        {
            await _context.Genders.Where(x => x.IdGender == idGender).ExecuteDeleteAsync();
        }
        public void Delete(int idGender)
        {
            _context.Genders.Where(x => x.IdGender == idGender).ExecuteDelete();
        }
    }
}
