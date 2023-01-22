using Task5WebApplication.Data;
using Task5WebApplication.Data.DbRepository;
using Task5WebApplication.Data.Entities;
using Task5WebApplication.Data.DbRepository.Interface;

namespace Task5WebApplication.Data.DbRepository
{
    public class TypeCityRepository : RepositoryBase<TypeCity>, ITypeCityRepository
    {
        public TypeCityRepository(AppDbContext context) : base(context)
        {
        }
    }
}
