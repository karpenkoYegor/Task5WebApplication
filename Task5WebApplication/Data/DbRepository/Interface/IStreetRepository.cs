using Task5WebApplication.Data.DbRepository.Interface;
using Task5WebApplication.Data.Entities;

namespace Task5WebApplication.Data.DbRepository.Interface
{
    public interface IStreetRepository : IRepositoryBase<Street>
    {
        Street GetRandomStreet(int countryId);
    }
}
