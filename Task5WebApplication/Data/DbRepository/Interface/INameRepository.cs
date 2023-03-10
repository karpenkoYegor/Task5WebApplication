using Task5WebApplication.Data.DbRepository.Interface;
using Task5WebApplication.Data.Entities;

namespace Task5WebApplication.Data.DbRepository.Interface
{
    public interface INameRepository : IRepositoryBase<Name>
    {
        Name GetRandomName(int countryId, bool isMale);
    }
}
