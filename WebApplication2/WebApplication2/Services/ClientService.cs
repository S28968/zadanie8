using Microsoft.Data.SqlClient;
using WebApplication2.Models.DTOs;

namespace WebApplication2.Services;

public class ClientService : IClientsService
{
    
    private readonly string _connectionString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;Trust Server Certificate=True";
    public async Task<List<ClientDTO>> GetClientsAsync(int clientId)
    {
        
        var clients = new List<ClientDTO>();
        

        var cmdText = @"SELECT c.IdClient, t.Name, t.Description, t.DateFrom, t.DateTo, ct.RegisteredAt, ct.PaymentDate FROM Client c
                        INNER JOIN Client_Trip ct on c.IdClient=ct.IdClient
                        INNER JOIN Trip t on ct.IdTrip=t.IdTrip
                        WHERE c.IdClient=@clientId";
        
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                await conn.OpenAsync();
                
                cmd.Parameters.AddWithValue("@clientId", clientId);
                
                var reader = await cmd.ExecuteReaderAsync();
                

                while (await reader.ReadAsync())
                {
                    
                        clients.Add(new ClientDTO()
                        {
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            DateFrom = reader.GetDateTime(3),
                            DateTo = reader.GetDateTime(4),
                            RegisteredAt = reader.GetInt32(5),
                            PaymentDate = reader.GetInt32(6)

                        });
                    

                }


            }
        }

        
        
        return clients;
    }
}