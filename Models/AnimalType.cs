using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Farm.Models;

[Table("animal_types")]
public class AnimalType
{
    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("name")]
    [MinLength(4, ErrorMessage = "The field Name needs {1} characters at least")]
    public required string Name { get; set; } 
    
    [Column("description")]
    public string? Description { get; set; } 
}
