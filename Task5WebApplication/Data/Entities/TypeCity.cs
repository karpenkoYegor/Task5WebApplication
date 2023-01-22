using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Task5WebApplication.Data.Entities;

namespace Task5WebApplication.Data.Entities;

public class TypeCity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    public TypeCity(string name)
    {
        Name = name;
    }
}