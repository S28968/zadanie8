namespace WebApplication2.Services;

public interface IClientTripService
{
    Task<int> PutClientTrip(int id, int tripId);
}