using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.IRepos;

namespace MiddelbyReolmarked.Repositories.DbRepos
{
    public class DbCustomerRepository : ICustomerRepository
    {
        private readonly string _cs;

        public DbCustomerRepository(string connectionString)
        {
            _cs = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void AddCustomer(Customer customer)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var sql = "INSERT INTO CUSTOMER (CustomerName, CustomerEmail, CustomerPhone) VALUES (@Navn, @Email, @Telefon)";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Navn", customer.CustomerName);
                    cmd.Parameters.AddWithValue("@Email", customer.CustomerEmail);
                    cmd.Parameters.AddWithValue("@Telefon", customer.CustomerPhone);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var list = new List<Customer>();
            const string sql = "SELECT CustomerId, CustomerName, CustomerEmail, CustomerPhone FROM dbo.CUSTOMER ORDER BY CustomerId";

            using var conn = new SqlConnection(_cs);
            conn.Open();
            using var cmd = new SqlCommand(sql, conn);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new Customer
                {
                    CustomerId = r.GetInt32(0),
                    CustomerName = r.GetString(1),
                    CustomerEmail = r.GetString(2),
                    CustomerPhone = r.GetString(3)
                });
            }
            return list;
        }

        public Customer GetCustomerById(int id)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var sql = "SELECT CustomerId, CustomerName, CustomerEmail, CustomerPhone FROM CUSTOMER WHERE CustomerId = @Id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Customer
                            {
                                CustomerId = reader.GetInt32(0),
                                CustomerName = reader.GetString(1),
                                CustomerEmail = reader.GetString(2),
                                CustomerPhone = reader.GetString(3)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void UpdateCustomer(Customer customer)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var sql = "UPDATE CUSTOMER SET CustomerName = @Navn, CustomerEmail = @Email, CustomerPhone = @Telefon WHERE CustomerId = @Id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", customer.CustomerId);
                    cmd.Parameters.AddWithValue("@Navn", customer.CustomerName);
                    cmd.Parameters.AddWithValue("@Email", customer.CustomerEmail);
                    cmd.Parameters.AddWithValue("@Telefon", customer.CustomerPhone);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCustomer(int id)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var sql = "DELETE FROM CUSTOMER WHERE CustomerId = @Id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
