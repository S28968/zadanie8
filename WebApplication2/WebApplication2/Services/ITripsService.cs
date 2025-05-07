using WebApplication2.Models.DTOs;

namespace WebApplication2.Services;

public interface ITripsService
{
    Task<List<TripDTO>> GetTripsAsync();

}