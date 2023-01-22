using Task5WebApplication.Data.DbRepository.Interface;
using Task5WebApplication.Data.Entities;

namespace Task5WebApplication.Data.DbRepository.Interface
{
    public interface ICountryRepository : IRepositoryBase<Country>
    {
        List<Country> GetAllCounties();
    }
}
