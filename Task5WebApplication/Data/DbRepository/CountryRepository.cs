using Task5WebApplication.Data;
using Task5WebApplication.Data.DbRepository;
using Task5WebApplication.Data.Entities;
using Task5WebApplication.Data.DbRepository.Interface;

namespace Task5WebApplication.Data.DbRepository
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(AppDbContext context) : base(context)
        {
        }

        public List<Country> GetAllCounties()
        {
            return Context.Country.ToList();
        }
    }
}
