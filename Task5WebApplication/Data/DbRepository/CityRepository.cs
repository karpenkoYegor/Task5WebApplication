using Microsoft.EntityFrameworkCore;
using Task5WebApplication.Data;
using Task5WebApplication.Data.DbRepository;
using Task5WebApplication.Data.Entities;
using Task5WebApplication.Data.DbRepository.Interface;

namespace Task5WebApplication.Data.DbRepository
{
    public class CityRepository : RepositoryBase<City>, ICityRepository
    {
        public CityRepository(AppDbContext context) : base(context)
        {
        }

        public City GetRandomCity(int countryId)
        {
            //var city = Context.City.Include(c => c.Region).Include(c => c.TypeCity)
            //    .Where(c => c.Region.CountryId == countryId).ToList();
            return null;
        }
    }
}
