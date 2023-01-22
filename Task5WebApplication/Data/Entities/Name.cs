using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task5WebApplication.Data.Entities;

public class Name
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string NamePerson { get; set; }
    public bool IsMale { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }

    public Name(string name, bool isMale, int countryId)
    {
        NamePerson = name;
        IsMale = isMale;
        CountryId = countryId;
    }

    public Name()
    {
        
    }
}