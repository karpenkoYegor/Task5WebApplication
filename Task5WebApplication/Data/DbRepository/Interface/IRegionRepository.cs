using Task5WebApplication.Data.DbRepository.Interface;
using Task5WebApplication.Data.Entities;

namespace Task5WebApplication.Data.DbRepository.Interface
{
    public interface IRegionRepository : IRepositoryBase<Region>
    {
        Region GetRandomRegion(int countryId);
        IEnumerable<Region> GetRegions(int countryId);
    }
}
