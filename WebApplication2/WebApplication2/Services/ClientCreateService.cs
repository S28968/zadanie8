using Microsoft.Data.SqlClient;
using WebApplication2.Models.DTOs;

namespace WebApplication2.Services;

public class ClientCreateService : IClientCreateService
{
    
    private readonly string _connectionString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;Trust Server Certificate=True";
    
    public async Task<int> PostClientAsync(ClientCreateDTO newClient)
    {

        int result;
        
        string query = @"
            INSERT INTO Client (FirstName, LastName, Email, Telephone, Pesel)
            VALUES (@FirstName, @LastName, @Email, @Telephone, @Pesel);
            SELECT SCOPE_IDENTITY();
            ";

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {

                cmd.Parameters.AddWithValue("@FirstName", newClient.FirstName);
                cmd.Parameters.AddWithValue("@LastName", newClient.LastName);
                cmd.Parameters.AddWithValue("@Email", newClient.Email);
                cmd.Parameters.AddWithValue("@Telephone", newClient.Telephone);
                cmd.Parameters.AddWithValue("@Pesel", newClient.Pesel);

                    
                await conn.OpenAsync();
                var newId =  (decimal)await cmd.ExecuteScalarAsync();
                result = (int)newId;
            }
                
        }

        return result;
    }
}