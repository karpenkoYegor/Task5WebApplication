using Task5WebApplication.Data.DbRepository.Interface;
using Task5WebApplication.Data.Entities;

namespace Task5WebApplication.Data.DbRepository.Interface
{
    public interface ICityRepository : IRepositoryBase<City>
    {
        City GetRandomCity(int countryId);
    }
}
