using Task5WebApplication.Data.DbRepository.Interface;
using Task5WebApplication.Data.Entities;

namespace Task5WebApplication.Data.DbRepository.Interface
{
    public interface ISurnameRepository : IRepositoryBase<Surname>
    {
        Surname GetRandomSurname(int countryId, bool isMale);
    }
}
