using System.Text;
using Microsoft.AspNetCore.Mvc;
using Task5WebApplication.Data.Entities;

namespace Task5WebApplication.Models;

public class IndexViewModel
{
    [BindProperty(SupportsGet = true)]
    public int Cursor { get; set; } = 11;
    public int Page { get; set; } = 1;
    public int UserSeed { get; set; } = 1;
    public double Errors { get; set; }
    public List<List<StringBuilder>> PeopleInformation { get; set; }
    public List<Country> Countries { get; set; }
    public int CountryId { get; set; }
}
