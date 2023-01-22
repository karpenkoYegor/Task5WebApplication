using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace Task5WebApplication.Data.Entities;

public class Surname
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string SurnamePerson { get; set; }
    public bool IsMale { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }

    public Surname(string name, bool isMale, int countryId)
    {
        SurnamePerson = name;
        IsMale = isMale;
        CountryId = countryId;
    }

    public Surname() { }
}