using Microsoft.Data.SqlClient;
using WebApplication2.Models.DTOs;

namespace WebApplication2.Services;

public class TripsService : ITripsService
{
    
    //private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=apbd;Integrated Security=True;";
    private readonly string _connectionString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;Trust Server Certificate=True";
    //private readonly string _connectionString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;";
    
    public async Task<List<TripDTO>> GetTripsAsync()
    {
        var trips = new List<TripDTO>();
        
        //var cmdText = @"select IdTrip, Name from Trip";

        var cmdText = @"SELECT t.IdTrip, t.Name, Description, DateFrom, DateTo, MaxPeople FROM Trip t";
        
        using (SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(cmdText, conn))
        {
            await conn.OpenAsync();

            var reader = await cmd.ExecuteReaderAsync();
            
            //int idTripOrdinal = reader.GetOrdinal("IdTrip");

            while (await reader.ReadAsync())
            {
                trips.Add(new TripDTO()
                {
                    IdTrip = reader.GetInt32(0), //idTripOrdinal
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    DateFrom = reader.GetDateTime(3),
                    DateTo = reader.GetDateTime(4),
                    MaxPeople = reader.GetInt32(5),
                    Countries = new List<CountryDTO>()
                });
                
            }
            
            await reader.CloseAsync();
            
            foreach (var trip in trips)
            {
                var cmdGetCountryNameByTripId = @"SELECT c.Name FROM Trip t
                                                INNER JOIN Country_Trip ct on t.IdTrip = ct.IdTrip
                                                INNER JOIN Country c on ct.IdCountry = c.IdCountry
                                                where t.idtrip = @IdTrip";
            
                SqlCommand cmdCountry = new SqlCommand(cmdGetCountryNameByTripId, conn);
                cmdCountry.Parameters.AddWithValue("@IdTrip", trip.IdTrip);
                using var countryReader = await cmdCountry.ExecuteReaderAsync();
                while (await countryReader.ReadAsync())
                {
                    trip.Countries.Add(new CountryDTO()
                        {
                            Name = countryReader.GetString(0)
                        });
                }

                //await countryReader.CloseAsync();
            }
            
        }
        
        
        return trips;
    }
    
}