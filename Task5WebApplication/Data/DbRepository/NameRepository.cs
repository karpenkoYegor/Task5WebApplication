using System.Linq.Expressions;
using Task5WebApplication.Data;
using Task5WebApplication.Data.DbRepository;
using Task5WebApplication.Data.Entities;
using Task5WebApplication.Data.DbRepository.Interface;

namespace Task5WebApplication.Data.DbRepository
{
    public class NameRepository : RepositoryBase<Name>, INameRepository
    {
        public NameRepository(AppDbContext context) : base(context)
        {
        }

        public Name GetRandomName(int countryId, bool isMale)
        {
            var names = Context.Name.Where(n => n.CountryId == countryId).Where(n => n.IsMale == isMale).ToList();
            return names.PickRandom();
        }
    }
}
