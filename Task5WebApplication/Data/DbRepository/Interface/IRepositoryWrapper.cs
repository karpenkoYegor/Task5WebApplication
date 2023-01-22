using Task5WebApplication.Data.DbRepository.Interface;

namespace Task5WebApplication.Data.DbRepository.Interface;

public interface IRepositoryWrapper
{
    ICityRepository City { get; }
    ICountryRepository Country { get; }
    INameRepository Name { get; }
    IRegionRepository Region { get; }
    IStreetRepository Street { get; }
    ISurnameRepository Surname { get; }
    IMiddleNameRepository MiddleName { get; }
    ITypeCityRepository TypeCity { get; }
    void Save();
}