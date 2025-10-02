using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.IRepos;

namespace MiddelbyReolmarked.Repositories.DbRepos
{
    public class DbInvoiceRepository : IInvoiceRepository
    {
        private readonly string _cs;

        public DbInvoiceRepository(string connectionString)
        {
            _cs = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void AddInvoice(Invoice invoice)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = @"INSERT INTO INVOICE (PeriodStart, PeriodEnd, InvoiceDate, Subtotal, Status, CustomerId)
                        VALUES (@PeriodStart, @PeriodEnd, @InvoiceDate, @Subtotal, @Status, @CustomerId)";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@PeriodStart", invoice.PeriodStart);
            cmd.Parameters.AddWithValue("@PeriodEnd", invoice.PeriodEnd);
            cmd.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
            cmd.Parameters.AddWithValue("@Subtotal", invoice.Subtotal);
            cmd.Parameters.AddWithValue("@Status", invoice.Status);
            cmd.Parameters.AddWithValue("@CustomerId", invoice.CustomerId);
            cmd.ExecuteNonQuery();
        }

        public Invoice GetInvoiceById(int id)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = @"SELECT InvoiceId, PeriodStart, PeriodEnd, InvoiceDate, Subtotal, Status, CustomerId FROM INVOICE WHERE InvoiceId = @Id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Invoice
                {
                    InvoiceId = reader.GetInt32(0),
                    PeriodStart = reader.GetDateTime(1),
                    PeriodEnd = reader.GetDateTime(2),
                    InvoiceDate = reader.GetDateTime(3),
                    Subtotal = reader.GetDecimal(4),
                    Status = reader.GetString(5),
                    CustomerId = reader.GetInt32(6)
                    // InvoiceLines skal hentes separat hvis nødvendigt
                };
            }
            return null;
        }

        public IEnumerable<Invoice> GetAllInvoices()
        {
            var list = new List<Invoice>();
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = @"SELECT InvoiceId, PeriodStart, PeriodEnd, InvoiceDate, Subtotal, Status, CustomerId FROM INVOICE";
            using var cmd = new SqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Invoice
                {
                    InvoiceId = reader.GetInt32(0),
                    PeriodStart = reader.GetDateTime(1),
                    PeriodEnd = reader.GetDateTime(2),
                    InvoiceDate = reader.GetDateTime(3),
                    Subtotal = reader.GetDecimal(4),
                    Status = reader.GetString(5),
                    CustomerId = reader.GetInt32(6)
                    // InvoiceLines skal hentes separat hvis nødvendigt
                });
            }
            return list;
        }

        public void UpdateInvoice(Invoice invoice)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = @"UPDATE INVOICE SET PeriodStart = @PeriodStart, PeriodEnd = @PeriodEnd, InvoiceDate = @InvoiceDate, Subtotal = @Subtotal, Status = @Status, CustomerId = @CustomerId WHERE InvoiceId = @Id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", invoice.InvoiceId);
            cmd.Parameters.AddWithValue("@PeriodStart", invoice.PeriodStart);
            cmd.Parameters.AddWithValue("@PeriodEnd", invoice.PeriodEnd);
            cmd.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
            cmd.Parameters.AddWithValue("@Subtotal", invoice.Subtotal);
            cmd.Parameters.AddWithValue("@Status", invoice.Status);
            cmd.Parameters.AddWithValue("@CustomerId", invoice.CustomerId);
            cmd.ExecuteNonQuery();
        }

        public void DeleteInvoice(int id)
        {
            using var conn = new SqlConnection(_cs);
            conn.Open();
            var sql = @"DELETE FROM INVOICE WHERE InvoiceId = @Id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}