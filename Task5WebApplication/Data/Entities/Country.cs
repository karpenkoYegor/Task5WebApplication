using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task5WebApplication.Data.Entities;

public class Country
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public int Id { get; set; }
    public string CountryName { get; set; }
    public string CodePhoneNumber { get; set; }
    public List<Name> Names { get; set; }
    public List<Surname> Surnames { get; set; }
    public List<MiddleName> MiddleNames { get; set; }
    public List<Region> Regions { get; set; }
    public List<Street> Streets { get; set; }
    public Country(string countryName, string codePhoneNumber)
    {
        CountryName = countryName;
        CodePhoneNumber = codePhoneNumber;
    }
}