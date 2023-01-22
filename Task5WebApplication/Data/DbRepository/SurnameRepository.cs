using Task5WebApplication.Data;
using Task5WebApplication.Data.DbRepository;
using Task5WebApplication.Data.Entities;
using Task5WebApplication.Data.DbRepository.Interface;

namespace Task5WebApplication.Data.DbRepository
{
    public class SurnameRepository : RepositoryBase<Surname>, ISurnameRepository
    {
        public SurnameRepository(AppDbContext context) : base(context)
        {
        }

        public Surname GetRandomSurname(int countryId, bool isMale)
        {
            isMale = countryId == 2 ? true : isMale;
            var surname = Context
                .Surname
                .Where(n => n.CountryId == countryId)
                .Where(n => n.IsMale == isMale)
                .ToList();
            return surname.PickRandom();
        }
    }
}
