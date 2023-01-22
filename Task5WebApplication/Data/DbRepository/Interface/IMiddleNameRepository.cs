using Task5WebApplication.Data.Entities;

namespace Task5WebApplication.Data.DbRepository.Interface;

public interface IMiddleNameRepository : IRepositoryBase<MiddleName>
{
    MiddleName GetRandomMiddleName(int countryId, bool isMale);
}