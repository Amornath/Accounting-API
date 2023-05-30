using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScopoERP.Helpers;
using System.Security.Claims;

namespace ScopoERP.Models
{
    public partial class ERPContext : IdentityDbContext<User>
    {
        public ERPContext()
        {
            Database.Migrate();
        }

        public ERPContext(DbContextOptions<ERPContext> options)
            : base(options)
        {
        }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<ParentAccount> ParentAccounts { get; set; }
        public virtual DbSet<SubsidiaryAccount> SubsidiaryAccounts { get; set; }
        public virtual DbSet<AccountBalance> AccountBalances { get; set; }
        public virtual DbSet<CostCenter> CostCenters { get; set; }
        public virtual DbSet<FinancialYear> FinancialYears { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionDetails> TransactionDetails { get; set; }
        public virtual DbSet<VoucherType> VoucherTypes { get; set; }
        public virtual DbSet<ReferenceNo> References { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //SeedData.SeedDBData(modelBuilder);


            modelBuilder.Entity<IdentityRole>().HasData(
               new IdentityRole() { Name = "Administrator", Id = "1", NormalizedName = "ADMINISTRATOR".Normalize(), ConcurrencyStamp = Guid.NewGuid().ToString() }
           );

            base.OnModelCreating(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        }

        internal void ExecuteSqlCommand(string v, SqlParameter name)
        {
            throw new NotImplementedException();
        }
    }
}



