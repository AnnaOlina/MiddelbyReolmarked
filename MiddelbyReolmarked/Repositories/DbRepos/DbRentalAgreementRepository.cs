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
    public class DbRentalAgreementRepository : IRentalAgreementRepository
    {
        private readonly string _cs;

        public DbRentalAgreementRepository(string connectionString)
        {
            _cs = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }


        public void AddRentalAgreement(RentalAgreement rental)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var sql = @"INSERT INTO RENTALAGREEMENT 
                            (StartDate, EndDate, CustomerId, RentalStatusId, RackId) 
                            VALUES (@Price, @StartDate, @CustomerId, @RentalStatusId, @RackId)";  // Rettet ReolId til RackId
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@StartDate", rental.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", rental.EndDate);
                    cmd.Parameters.AddWithValue("@CustomerId", rental.CustomerId);
                    cmd.Parameters.AddWithValue("@RentalStatus", rental.RentalStatus);
                    cmd.Parameters.AddWithValue("@RackId", rental.RackId);  // Rettet ReolId til RackId
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public RentalAgreement GetRentalAgreementById(int rentalAgreementId)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var sql = @"SELECT RentalAgreementId, StartDate, EndDate, CustomerId, RentalStatusId, RackId 
                            FROM RentalAgreements WHERE RentalAgreementId = @Id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", rentalAgreementId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new RentalAgreement
                            {
                                RentalAgreementId = reader.GetInt32(0),
                                StartDate = reader.GetDateTime(1),
                                EndDate = reader.GetDateTime(2),
                                CustomerId = reader.GetInt32(3),
                                RentalStatus = (RentalStatus)reader.GetInt32(4),
                                RackId = reader.GetInt32(5)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public IEnumerable<RentalAgreement> GetAllRentalAgreements()
        {
            var list = new List<RentalAgreement>();
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var sql = @"SELECT RentalAgreementId, StartDate, EndDate, CustomerId, RentalStatusId, RackId 
                    FROM RENTALAGREEMENT";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RentalAgreement rental = new RentalAgreement();
                            rental.RentalAgreementId = reader.GetInt32(0);
                            rental.StartDate = reader.GetDateTime(1);
                            rental.EndDate = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2);
                            rental.CustomerId = reader.GetInt32(3);
                            rental.RentalStatus = (RentalStatus)reader.GetInt32(4);
                            rental.RackId = reader.GetInt32(5);
                            list.Add(rental);
                        }
                    }
                }
            }
            return list;
        }

        public void UpdateRentalAgreement(RentalAgreement rental)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var sql = @"UPDATE RENTALAGREEMENT SET 
                            StartDate = @StartDate, EndDate = @EndDate, CustomerId = @CustomerId, 
                            RentalStatusId = @RentalStatusId, RackId = @RackId 
                            WHERE RentalAgreementId = @Id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", rental.RentalAgreementId);
                    cmd.Parameters.AddWithValue("@StartDate", rental.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", rental.EndDate);
                    cmd.Parameters.AddWithValue("@CustomerId", rental.CustomerId);
                    cmd.Parameters.AddWithValue("@RentalStatus", rental.RentalStatus);
                    cmd.Parameters.AddWithValue("@RackId", rental.RackId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteRentalAgreement(int rentalAgreementId)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var sql = "DELETE FROM RENTALAGREEMENT WHERE RentalAgreementId = @Id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", rentalAgreementId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
