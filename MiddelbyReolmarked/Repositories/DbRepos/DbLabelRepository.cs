using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.IRepos;

namespace MiddelbyReolmarked.Repositories.DbRepos
{
    public class DbLabelRepository : ILabelRepository
    {
        private readonly string _cs;

        public DbLabelRepository(string connectionString)
        {
            _cs = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void AddLabel(Label label)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = @"INSERT INTO LABEL (ProductPrice, BarCode, Sold, CreatedAt, RackId)
                        VALUES (@ProductPrice, @BarCode, @Sold, @CreatedAt, @RackId)";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ProductPrice", label.ProductPrice);
            cmd.Parameters.AddWithValue("@BarCode", label.BarCode);
            cmd.Parameters.AddWithValue("@Sold", (object)label.Sold ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedAt", label.CreatedAt);
            cmd.Parameters.AddWithValue("@RackId", label.RackId);
            cmd.ExecuteNonQuery();
        }

        public Label GetLabelById(int id)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = @"SELECT LabelId, ProductPrice, BarCode, Sold, CreatedAt, RackId FROM LABEL WHERE LabelId = @Id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Label
                {
                    LabelId = reader.GetInt32(0),
                    ProductPrice = reader.GetDecimal(1),
                    BarCode = reader.GetString(2),
                    Sold = reader.IsDBNull(3) ? default : reader.GetDateTime(3),
                    CreatedAt = reader.GetDateTime(4),
                    RackId = reader.GetInt32(5)
                };
            }
            return null;
        }

        public IEnumerable<Label> GetAllLabels()
        {
            var list = new List<Label>();
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = @"SELECT LabelId, ProductPrice, BarCode, Sold, CreatedAt, RackId FROM LABEL";
            using var cmd = new SqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Label
                {
                    LabelId = reader.GetInt32(0),
                    ProductPrice = reader.GetDecimal(1),
                    BarCode = reader.GetString(2),
                    Sold = reader.IsDBNull(3) ? default : reader.GetDateTime(3),
                    CreatedAt = reader.GetDateTime(4),
                    RackId = reader.GetInt32(5)
                });
            }
            return list;
        }

        public void UpdateLabel(Label label)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = @"UPDATE LABEL SET ProductPrice = @ProductPrice, BarCode = @BarCode, Sold = @Sold, CreatedAt = @CreatedAt, RackId = @RackId WHERE LabelId = @Id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", label.LabelId);
            cmd.Parameters.AddWithValue("@ProductPrice", label.ProductPrice);
            cmd.Parameters.AddWithValue("@BarCode", label.BarCode);
            cmd.Parameters.AddWithValue("@Sold", (object)label.Sold ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedAt", label.CreatedAt);
            cmd.Parameters.AddWithValue("@RackId", label.RackId);
            cmd.ExecuteNonQuery();
        }

        public void DeleteLabel(int id)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = @"DELETE FROM LABEL WHERE LabelId = @Id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}