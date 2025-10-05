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
    public class DbRentalAgreementRackRepository : IRentalAgreementRackRepository
    {
        private readonly string _cs;

        public DbRentalAgreementRackRepository(string connectionString)
        {
            _cs = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void AddRentalAgreementRack(RentalAgreementRack rar)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var sql = "INSERT INTO RENTALAGREEMENT_RACK (RentalAgreementId, RackId) VALUES (@RentalAgreementId, @RackId)";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RentalAgreementId", rar.RentalAgreementId);
                    cmd.Parameters.AddWithValue("@RackId", rar.RackId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Hent alle Rack tilknyttet en specifik RentalAgreementId
        public IEnumerable<RentalAgreementRack> GetByRentalAgreementId(int rentalAgreementId)
        {
            var result = new List<RentalAgreementRack>();
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var sql = "SELECT RentalAgreementId, RackId FROM RENTALAGREEMENT_RACK WHERE RentalAgreementId = @RentalAgreementId";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RentalAgreementId", rentalAgreementId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new RentalAgreementRack
                            {
                                RentalAgreementId = reader.GetInt32(0),
                                RackId = reader.GetInt32(1)
                            });
                        }
                    }
                }
            }
            return result;
        }

        // Hent alle RentalAgreement tilknyttet en specifik RackId
        public IEnumerable<RentalAgreementRack> GetByRackId(int rackId)
        {
            var result = new List<RentalAgreementRack>();
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var sql = "SELECT RentalAgreementId, RackId FROM RENTALAGREEMENT_RACK WHERE RackId = @RackId";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RackId", rackId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new RentalAgreementRack
                            {
                                RentalAgreementId = reader.GetInt32(0),
                                RackId = reader.GetInt32(1)
                            });
                        }
                    }
                }
            }
            return result;
        }

        public void DeleteRentalAgreementRack(int rentalAgreementId, int rackId)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                var sql = "DELETE FROM RENTALAGREEMENT_RACK WHERE RentalAgreementId = @RentalAgreementId AND RackId = @RackId";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RentalAgreementId", rentalAgreementId);
                    cmd.Parameters.AddWithValue("@RackId", rackId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
