using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;

namespace TE1.Schemas.Data
{
    public abstract class BaseTable : DbContext
    {
        protected const String SchemaName = "public";
        protected NpgsqlConnection DbConn = null;

        public BaseTable() : base() =>
            this.DbConn = 환경설정.CreateDbConnection();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql(this.DbConn);
    }
}
