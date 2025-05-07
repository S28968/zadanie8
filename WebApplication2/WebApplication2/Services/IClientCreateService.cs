using WebApplication2.Models.DTOs;

namespace WebApplication2.Services;

public interface IClientCreateService
{
    public Task<int> PostClientAsync(ClientCreateDTO newClient);
}