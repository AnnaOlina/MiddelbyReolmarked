using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.IRepos;

namespace MiddelbyReolmarked.Repositories.DbRepos
{
    public class DbRentalStatusRepository : IRentalStatusRepository
    {
        private readonly string _cs;

        public DbRentalStatusRepository(string connectionString)
        {
            _cs = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void AddRentalStatus(RentalStatus rentalStatus)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = "INSERT INTO RENTALSTATUS (RentalStatusName) VALUES (@Name)";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Name", rentalStatus.RentalStatusName);
            cmd.ExecuteNonQuery();
        }

        public RentalStatus GetRentalStatusById(int id)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = "SELECT RentalStatusId, RentalStatusName FROM RENTALSTATUS WHERE RentalStatusId = @Id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new RentalStatus
                {
                    RentalStatusId = reader.GetInt32(0),
                    RentalStatusName = reader.GetString(1)
                };
            }
            return null;
        }

        public IEnumerable<RentalStatus> GetAllRentalStatuses()
        {
            var list = new List<RentalStatus>();
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = "SELECT RentalStatusId, RentalStatusName FROM RENTALSTATUS";
            using var cmd = new SqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new RentalStatus
                {
                    RentalStatusId = reader.GetInt32(0),
                    RentalStatusName = reader.GetString(1)
                });
            }
            return list;
        }

        public void UpdateRentalStatus(RentalStatus rentalStatus)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = "UPDATE RENTALSTATUS SET RentalStatusName = @Name WHERE RentalStatusId = @Id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", rentalStatus.RentalStatusId);
            cmd.Parameters.AddWithValue("@Name", rentalStatus.RentalStatusName);
            cmd.ExecuteNonQuery();
        }

        public void DeleteRentalStatus(int id)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = "DELETE FROM RENTALSTATUS WHERE RentalStatusId = @Id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}