using Lab3.Entity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Repository
{
    internal class ProductRepo
    {
        private readonly string connectString = "Server=(localdb)\\MSSQLLocalDB;Database=Lab3DB;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";

        public async Task AddAsync(product pr)
        {
            using (SqlConnection connection = new SqlConnection(connectString))
            {
                string sqlQuery = @"Insert Into Products(Name,Qunatity,Price,CreatedAt) 
                                    values(@name,@quantity,@Price,@date)"; // Parameterized Query

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@name", pr.Name);
                command.Parameters.AddWithValue("@quantity", pr.Quantity);
                command.Parameters.AddWithValue("@Price", pr.Price);
                command.Parameters.AddWithValue("@date", pr.CreatedAt);


                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync(); // For Insert, Update, Delete
            }
        }


        public async Task UpdateAsync(product pr)
        {
            using (SqlConnection connection = new SqlConnection(connectString))
            {
                string sqlQuery = @"Update Products Set Qunatity=@quantity
                                    ,Price=@price Where Id=@id"; 
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                //Got id from old proudct 
                command.Parameters.AddWithValue("@id", pr.Id);
                //Change old values to updated values
                command.Parameters.AddWithValue("@quantity", pr.Quantity);
                command.Parameters.AddWithValue("@Price", pr.Price);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync(); 
            }
        }



        public async Task DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectString))
            {
                string sqlQuery = @"Delete from Products where Id = @id"; 
                
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync(); 
            }
        }


        public async Task<List<product>> GetAllAsync()
        {
            List<product> products = new List<product>();
            using (SqlConnection connection = new SqlConnection(connectString))
            {
                string sqlQuery = "Select * from Products";
                SqlCommand command = new SqlCommand(sqlQuery,connection);

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    products.Add(
                        new product 
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Quantity = reader.GetInt32(2),
                            Price = reader.GetInt32(3),
                            CreatedAt = reader.GetDateTime(4)
                        }
                        );
                }
            }
            return products;
        }




        public async Task<product> GetByIdAsync(int id)
        {
            product pr = new();
            using (SqlConnection connection = new SqlConnection(connectString))
            {
                string sqlQuery = "Select * from Products";
                SqlCommand command = new SqlCommand(sqlQuery,connection);
                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    if (reader.GetInt32(0) == id)
                    {
                        pr.Id = reader.GetInt32(0);
                        pr.Name = reader.GetString(1);
                        pr.Quantity = reader.GetInt32(2);
                        pr.Price = reader.GetInt32(3);
                        pr.CreatedAt = reader.GetDateTime(4);
                    }
                }
            }
            return pr;

        }
    }
}
