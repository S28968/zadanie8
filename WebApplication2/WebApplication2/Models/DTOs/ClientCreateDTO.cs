using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models.DTOs;

public class ClientCreateDTO
{
    [MaxLength(120)]
    public String FirstName { get; set; }
    
    [MaxLength(120)]
    public String LastName { get; set; }
    
    [MaxLength(120)]
    public String Email { get; set; }
    
    [MaxLength(120)]
    public String Telephone { get; set; }
    
    [MaxLength(120)]
    public String Pesel { get; set; }
}