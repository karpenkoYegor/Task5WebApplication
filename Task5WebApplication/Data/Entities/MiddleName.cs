namespace Task5WebApplication.Data.Entities;

public class MiddleName
{
    public int Id { get; set; }
    public string MiddleNamePerson { get; set; }
    public bool IsMale { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }

    public MiddleName(string name, bool isMale, int countryId)
    {
        MiddleNamePerson = name;
        IsMale = isMale;
        CountryId = countryId;
    }

    public MiddleName()
    {
            
    }
}