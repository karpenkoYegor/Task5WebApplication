using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Task5WebApplication.Data.Entities;
public class City
{
    public int Id { get; set; }
    public string CityName { get; set; }
    public string IndexCity { get; set; }
    public int TypeCityId { get; set; }
    public TypeCity TypeCity { get; set; }

    public City(string cityName, int regionId, int typeCityId, string indexCity)
    {
        CityName = cityName;
        IndexCity = indexCity;
        TypeCityId = typeCityId;
        if (regionId == 13)
        {
            IndexCity = $"{Extensions.r.Next(100000, 199999)}-{Extensions.r.Next(100000, 199999)}";
        }
        else
        {
            IndexCity = $"{Extensions.r.Next(200000, 299999)}";
        }
    }
    public City () {}
}