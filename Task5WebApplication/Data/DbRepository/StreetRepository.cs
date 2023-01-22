using Task5WebApplication.Data;
using Task5WebApplication.Data.DbRepository;
using Task5WebApplication.Data.Entities;
using Task5WebApplication.Data.DbRepository.Interface;

namespace Task5WebApplication.Data.DbRepository
{
    public class StreetRepository : RepositoryBase<Street>, IStreetRepository
    {
        public StreetRepository(AppDbContext context) : base(context)
        {
        }

        public Street GetRandomStreet(int countryId)
        {
            var streets = Context.Street.Where(x => x.CountryId == countryId).ToList();
            return streets.PickRandom();
        }
    }
}
