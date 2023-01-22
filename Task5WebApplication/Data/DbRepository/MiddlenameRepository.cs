using Task5WebApplication.Data.DbRepository.Interface;
using Task5WebApplication.Data.Entities;

namespace Task5WebApplication.Data.DbRepository;

public class MiddlenameRepository : RepositoryBase<MiddleName>, IMiddleNameRepository
{
    public MiddlenameRepository(AppDbContext context) : base(context)
    {
    }

    public MiddleName GetRandomMiddleName(int countryId, bool isMale)
    {
        var middleNames = Context.MiddleName.Where(n => n.CountryId == countryId).Where(n => n.IsMale == isMale).ToList();
        return middleNames.PickRandom();
    }
}