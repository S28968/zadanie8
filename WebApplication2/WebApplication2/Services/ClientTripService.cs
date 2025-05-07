using System.Runtime.InteropServices;
using Microsoft.Data.SqlClient;
using WebApplication2.Exceptions;

namespace WebApplication2.Services;

public class ClientTripService : IClientTripService
{
    
    private readonly string _connectionString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;Trust Server Certificate=True";
    
    public async Task<int> PutClientTrip(int idClient, int idTrip)
    {

        int result = 1;

        string clientIdQuery = @"
            SELECT c.IdClient FROM Client c WHERE c.IdClient=@idClient;
            ";
        
        string tripIdQuery = @"
                SELECT t.MaxPeople, Count(ct.IdClient) FROM Trip t
                INNER JOIN Client_Trip ct on t.IdTrip=ct.IdTrip
                Where t.IdTrip = @idTrip
				Group by t.MaxPeople;";
        
        string newTripInsert = @"INSERT Into Client_Trip(IdClient, IdTrip, RegisteredAt)
                                VALUES (@idClient, @idTrip, @RegisteredAt);
                                SELECT SCOPE_IDENTITY();";

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(clientIdQuery, conn))
            {

                cmd.Parameters.AddWithValue("@idClient", idClient);
                
                await conn.OpenAsync();

                var resultClientId = await cmd.ExecuteScalarAsync();

                if (resultClientId == null)
                {
                    throw new ValidationException("Client Id Doesn't Exist");
                }
                
                await conn.CloseAsync();
                
            }
            
            using (SqlCommand cmd = new SqlCommand(tripIdQuery, conn))
            {

                cmd.Parameters.AddWithValue("@idTrip", idTrip);
                
                await conn.OpenAsync();

                var reader = await cmd.ExecuteReaderAsync();

                
                if(await reader.ReadAsync()) {
                    int maxPeople = reader.GetInt32(0);
                    int currentPeople = reader.GetInt32(1);
                    if (currentPeople>= maxPeople)
                    {
                        throw new ValidationException("Not Enough Empty Spaces");
                    }
                    
                }
                else
                {
                    throw new ValidationException("Trip Id not found");
                }

                await conn.CloseAsync();

            }

            using (SqlCommand cmd = new SqlCommand(newTripInsert, conn))
            {

                await conn.OpenAsync();
                cmd.Parameters.AddWithValue("@idClient", idClient);
                cmd.Parameters.AddWithValue("@idTrip", idTrip);

                DateTime date = DateTime.Now;
                int dateInt = int.Parse(date.ToString("yyyymmdd"));

                cmd.Parameters.AddWithValue("@RegisteredAt", dateInt);

                await cmd.ExecuteScalarAsync();
                
                
                
                

            }
            
            
        }

        return result;
    }
    
}