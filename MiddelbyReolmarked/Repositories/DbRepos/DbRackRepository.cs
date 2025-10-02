using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.IRepos;
{
    
}

namespace MiddelbyReolmarked.Repositories.DbRepos
{
    public class DbRackRepository : IRackRepository
    {
        private readonly string _cs;

        public DbRackRepository(string connectionString)
        {
            _cs = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void AddRack(Rack rack)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var sql = "INSERT INTO RACK (RackNumber, AmountShelves, HangerBar) VALUES (@RackNumber, @AmountShelves, @HangerBar)";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RackNumber", rack.RackNumber);
                    cmd.Parameters.AddWithValue("@AmountShelves", rack.AmountShelves);
                    cmd.Parameters.AddWithValue("@HangerBar", rack.HangerBar);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Rack GetRackById(int id)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var sql = "SELECT RackId, RackNumber, AmountShelves, HangerBar FROM RACK WHERE RackId = @Id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Rack rack = new Rack();
                            rack.RackId = reader.GetInt32(0);
                            rack.RackNumber = reader.GetString(1);
                            rack.AmountShelves = reader.GetInt32(2);
                            rack.HangerBar = reader.GetBoolean(3);
                            return rack;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public IEnumerable<Rack> GetAllRacks()
        {
            List<Rack> racks = new List<Rack>();
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var sql = "SELECT RackId, RackNumber, AmountShelves, HangerBar FROM RACK ORDER BY RackId";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Rack rack = new Rack();
                            rack.RackId = reader.GetInt32(0);
                            rack.RackNumber = reader.GetString(1);
                            rack.AmountShelves = reader.GetInt32(2);
                            rack.HangerBar = reader.GetBoolean(3);
                            racks.Add(rack);
                        }
                    }
                }
            }
            return racks;
        }

        public void UpdateRack(Rack rack)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var sql = "UPDATE RACK SET RackNumber = @RackNumber, AmountShelves = @AmountShelves, HangerBar = @HangerBar WHERE RackId = @RackId";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RackId", rack.RackId);
                    cmd.Parameters.AddWithValue("@RackNumber", rack.RackNumber);
                    cmd.Parameters.AddWithValue("@AmountShelves", rack.AmountShelves);
                    cmd.Parameters.AddWithValue("@HangerBar", rack.HangerBar);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteRack(int id)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var sql = "DELETE FROM RACK WHERE RackId = @RackId";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RackId", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public Rack GetRackByNumber(string rackNumber)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var sql = "SELECT RackId, RackNumber, AmountShelves, HangerBar FROM RACK WHERE RackNumber = @RackNumber";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RackNumber", rackNumber);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Rack rack = new Rack();
                            rack.RackId = reader.GetInt32(0);
                            rack.RackNumber = reader.GetString(1);
                            rack.AmountShelves = reader.GetInt32(2);
                            rack.HangerBar = reader.GetBoolean(3);
                            return rack;
                        }
                    }
                }
            }
            return null;
        }

        public List<int> ListAvailableRackIds()
        {
            List<int> availableIds = new List<int>();
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var sql = "SELECT RackId FROM RACK WHERE ErUdlejet = 0";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            availableIds.Add(reader.GetInt32(0));
                        }
                    }
                }
            }
            return availableIds;
        }

        public Customer GetRenterByRackNumber(string rackNumber)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                // Find RackId ud fra rackNumber
                var sqlRack = "SELECT RackId FROM RACK WHERE RackNumber = @RackNumber";
                int rackId = -1;
                using (var cmdRack = new SqlCommand(sqlRack, conn))
                {
                    cmdRack.Parameters.AddWithValue("@RackNumber", rackNumber);
                    using (var readerRack = cmdRack.ExecuteReader())
                    {
                        if (readerRack.Read())
                        {
                            rackId = readerRack.GetInt32(0);
                        }
                        else
                        {
                            return null; // Fandt ikke reolnummer
                        }
                    }
                }

                // Find RentalAgreement, hvor RackId matcher og udlejning ikke er slut
                var sqlRental = "SELECT CustomerId FROM RENTALAGREEMENT WHERE RackId = @RackId AND RentalEnd IS NULL";
                int customerId = -1;
                using (var cmdRental = new SqlCommand(sqlRental, conn))
                {
                    cmdRental.Parameters.AddWithValue("@RackId", rackId);
                    using (var readerRental = cmdRental.ExecuteReader())
                    {
                        if (readerRental.Read())
                        {
                            customerId = readerRental.GetInt32(0);
                        }
                        else
                        {
                            return null; // Ingen aktiv udlejning på denne reol
                        }
                    }
                }

                // Find Customer ud fra customerId
                var sqlCustomer = "SELECT CustomerId, Name FROM CUSTOMER WHERE CustomerId = @CustomerId";
                using (var cmdCustomer = new SqlCommand(sqlCustomer, conn))
                {
                    cmdCustomer.Parameters.AddWithValue("@CustomerId", customerId);
                    using (var readerCustomer = cmdCustomer.ExecuteReader())
                    {
                        if (readerCustomer.Read())
                        {
                            Customer customer = new Customer();
                            customer.CustomerId = readerCustomer.GetInt32(0);
                            customer.CustomerName = readerCustomer.GetString(1);
                            return customer;
                        }
                        else
                        {
                            return null; // Fandt ikke kunde
                        }
                    }
                }
            }
        }
    }
}
