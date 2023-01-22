using Microsoft.EntityFrameworkCore;
using Task5WebApplication.Data;
using Task5WebApplication.Data.DbRepository;
using Task5WebApplication.Data.Entities;
using Task5WebApplication.Data.DbRepository.Interface;

namespace Task5WebApplication.Data.DbRepository
{
    public class RegionRepository : RepositoryBase<Region>, IRegionRepository
    {
        public RegionRepository(AppDbContext context) : base(context)
        {
        }

        public Region GetRandomRegion(int countryId)
        {
            IQueryable<Region> regions = Context.Region.Where(r => r.CountryId == countryId)
                .Include(r => r.Cities)
                .ThenInclude(c => c.TypeCity);
            //IQueryable<Region> regions = Context.Region.FromSql(
            //    $"SELECT [r].[CountryId], [r].[RegionName], [t0].[Id], [t0].[CityName], [t0].[IndexCity], [t0].[RegionId], [t0].[TypeCityId], [t0].[Id0], [t0].[Name] FROM [Region] AS [r] LEFT JOIN (SELECT [c].[Id], [c].[CityName], [c].[IndexCity], [c].[RegionId], [c].[TypeCityId], [t].[Id] AS [Id0], [t].[Name] FROM [City] AS [c] INNER JOIN [TypeCity] AS [t] ON [c].[TypeCityId] = [t].[Id]) AS [t0] ON [r].[Id] = [t0].[RegionId] WHERE [r].[CountryId] = 2");
            //regions = regions.Include(r => r.Cities).ThenInclude(c => c.TypeCity);
            //var reg = regions.ToList();
            return regions.ToList().PickRandom();
        }

        public IEnumerable<Region> GetRegions(int countryId)
        {
            IQueryable<Region> regions = Context.Region
                .Include(r => r.Cities)
                .ThenInclude(c => c.TypeCity).Where(r => r.CountryId == countryId);
            //IQueryable<Region> regions = Context.Region.FromSql(
            //    $"SELECT [r].[CountryId], [r].[RegionName], [t0].[Id], [t0].[CityName], [t0].[IndexCity], [t0].[RegionId], [t0].[TypeCityId], [t0].[Id0], [t0].[Name] FROM [Region] AS [r] LEFT JOIN (SELECT [c].[Id], [c].[CityName], [c].[IndexCity], [c].[RegionId], [c].[TypeCityId], [t].[Id] AS [Id0], [t].[Name] FROM [City] AS [c] INNER JOIN [TypeCity] AS [t] ON [c].[TypeCityId] = [t].[Id]) AS [t0] ON [r].[Id] = [t0].[RegionId] WHERE [r].[CountryId] = 2");
            //regions = regions.Include(r => r.Cities).ThenInclude(c => c.TypeCity);
            //var reg = regions.ToList();
            return regions.ToList();
        }
    }
}
