using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task5WebApplication.Data.Entities;

public class Region
{
    [Key]
    public int Id { get; set; }
    public string RegionName { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public List<City> Cities { get; set; }
    public Region(string regionName, int countryId)
    {
        RegionName = regionName;
        CountryId = countryId;
    }
}