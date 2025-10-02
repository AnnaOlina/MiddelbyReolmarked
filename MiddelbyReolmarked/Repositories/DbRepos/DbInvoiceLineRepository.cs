using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.IRepos;

namespace MiddelbyReolmarked.Repositories.DbRepos
{
    public class DbInvoiceLineRepository : IInvoiceLineRepository
    {
        private readonly string _cs;

        public DbInvoiceLineRepository(string connectionString)
        {
            _cs = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void AddInvoiceLine(InvoiceLine invoiceLine)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = @"INSERT INTO INVOICELINE (Description, UnitPrice, Quantity, LineTotal, InvoiceId, RentalAgreementId)
                        VALUES (@Description, @UnitPrice, @Quantity, @LineTotal, @InvoiceId, @RentalAgreementId)";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Description", invoiceLine.Description);
            cmd.Parameters.AddWithValue("@UnitPrice", invoiceLine.UnitPrice);
            cmd.Parameters.AddWithValue("@Quantity", invoiceLine.Quantity);
            cmd.Parameters.AddWithValue("@LineTotal", (object)invoiceLine.LineTotal ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@InvoiceId", invoiceLine.InvoiceId);
            cmd.Parameters.AddWithValue("@RentalAgreementId", invoiceLine.RentalAgreementId);
            cmd.ExecuteNonQuery();
        }

        public InvoiceLine GetInvoiceLineById(int id)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();
            //var sql = @"SELECT InvoiceLineId, Description, UnitPrice, Quantity, LineTotal, InvoiceId, RentalAgreementId FROM INVOICELINE WHERE InvoiceLineId = @Id";
            var sql = @"SELECT InvoiceLineId, Description, UnitPrice, Quantity, InvoiceId, RentalAgreementId FROM INVOICELINE WHERE InvoiceLineId = @Id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new InvoiceLine
                {
                    InvoiceLineId = reader.GetInt32(0),
                    Description = reader.GetString(1),
                    UnitPrice = reader.GetDecimal(2),
                    Quantity = reader.GetInt32(3),
                    //LineTotal = reader.IsDBNull(4) ? default : reader.GetDecimal(4),
                    InvoiceId = reader.GetInt32(5),
                    RentalAgreementId = reader.GetInt32(6)
                };
            }
            return null;
        }

        public IEnumerable<InvoiceLine> GetAllInvoiceLines()
        {
            var list = new List<InvoiceLine>();
            using var conn = new SqlConnection(_cs);
            conn.Open();
            //var sql = @"SELECT InvoiceLineId, Description, UnitPrice, Quantity, LineTotal, InvoiceId, RentalAgreementId FROM INVOICELINE";
            var sql = @"SELECT InvoiceLineId, Description, UnitPrice, Quantity, InvoiceId, RentalAgreementId FROM INVOICELINE";
            using var cmd = new SqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new InvoiceLine
                {
                    InvoiceLineId = reader.GetInt32(0),
                    Description = reader.GetString(1),
                    UnitPrice = reader.GetDecimal(2),
                    Quantity = reader.GetInt32(3),
                    //LineTotal = reader.IsDBNull(4) ? default : reader.GetDecimal(4),
                    InvoiceId = reader.GetInt32(5),
                    RentalAgreementId = reader.GetInt32(6)
                });
            }
            return list;
        }

        public void UpdateInvoiceLine(InvoiceLine invoiceLine)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = @"UPDATE INVOICELINE SET Description = @Description, UnitPrice = @UnitPrice, Quantity = @Quantity, LineTotal = @LineTotal, InvoiceId = @InvoiceId, RentalAgreementId = @RentalAgreementId WHERE InvoiceLineId = @Id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", invoiceLine.InvoiceLineId);
            cmd.Parameters.AddWithValue("@Description", invoiceLine.Description);
            cmd.Parameters.AddWithValue("@UnitPrice", invoiceLine.UnitPrice);
            cmd.Parameters.AddWithValue("@Quantity", invoiceLine.Quantity);
            cmd.Parameters.AddWithValue("@LineTotal", (object)invoiceLine.LineTotal ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@InvoiceId", invoiceLine.InvoiceId);
            cmd.Parameters.AddWithValue("@RentalAgreementId", invoiceLine.RentalAgreementId);
            cmd.ExecuteNonQuery();
        }

        public void DeleteInvoiceLine(int id)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = @"DELETE FROM INVOICELINE WHERE InvoiceLineId = @Id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}