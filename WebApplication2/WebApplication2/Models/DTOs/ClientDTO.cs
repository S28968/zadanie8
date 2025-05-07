namespace WebApplication2.Models.DTOs;

public class ClientDTO
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime DateFrom { get; set; }
    
    public DateTime DateTo { get; set; }

    public int RegisteredAt { get; set; }
    
    public int PaymentDate { get; set; }
    
}