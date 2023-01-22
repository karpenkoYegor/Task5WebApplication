using Microsoft.EntityFrameworkCore;
using Task5WebApplication.Data.DbRepository.Interface;
using Task5WebApplication.Data.DbRepository;

namespace Task5WebApplication.Data.DbRepository;

public class RepositoryWrapper : IRepositoryWrapper
{
    private AppDbContext _context;
    private ICityRepository _cityRepository;
    private ICountryRepository _countryRepository;
    private INameRepository _nameRepository;
    private IRegionRepository _regionRepository;
    private IStreetRepository _streetRepository;
    private ISurnameRepository _surnameRepository;
    private ITypeCityRepository _typeCityRepository;
    private IMiddleNameRepository _middleNameRepository;
    public RepositoryWrapper(AppDbContext context)
    {
        _context = context;
    }

    public ICityRepository City
    {
        get
        {
            if( _cityRepository == null )
                _cityRepository = new CityRepository(_context);
            return _cityRepository;
        }
    }

    public ICountryRepository Country
    {
        get
        {
            if(_countryRepository == null)
                _countryRepository = new CountryRepository(_context);
            return _countryRepository;
        }
    }

    public INameRepository Name
    {
        get
        {
            if(_nameRepository == null)
                _nameRepository = new NameRepository(_context);
            return _nameRepository;
        }
    }

    public IRegionRepository Region
    {
        get
        {
            if(_regionRepository == null)
                _regionRepository = new RegionRepository(_context);
            return _regionRepository;
        }
    }

    public IStreetRepository Street
    {
        get
        {
            if(_streetRepository == null)
                _streetRepository = new StreetRepository(_context);
            return _streetRepository;
        }
    }

    public ISurnameRepository Surname
    {
        get
        {
            if(_surnameRepository==null)
                _surnameRepository= new SurnameRepository(_context);
            return _surnameRepository;
        }
    }

    public IMiddleNameRepository MiddleName {
        get
        {
            if (_middleNameRepository == null)
                _middleNameRepository = new MiddlenameRepository(_context);
            return _middleNameRepository;
        }
    }

    public ITypeCityRepository TypeCity
    {
        get
        {
            if(_typeCityRepository == null)
                _typeCityRepository= new TypeCityRepository(_context);
            return _typeCityRepository;
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}