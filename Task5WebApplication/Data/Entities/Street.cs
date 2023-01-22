using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task5WebApplication.Data.Entities;

public class Street
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string StreetName { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }

    public Street(string streetName, int countryId)
    {
        StreetName = streetName;
        CountryId = countryId;
    }
}