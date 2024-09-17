using System.ComponentModel.DataAnnotations.Schema;

namespace API_Farm.Models;

[Table("animal_types")]
public class AnimalType
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public required string Name { get; set; } 
    
    [Column("description")]
    public string? Description { get; set; } 
}
