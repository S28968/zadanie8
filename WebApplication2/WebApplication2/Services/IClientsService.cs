using WebApplication2.Models.DTOs;

namespace WebApplication2.Services;

public interface IClientsService
{
    Task<List<ClientDTO>> GetClientsAsync(int clientId);
}