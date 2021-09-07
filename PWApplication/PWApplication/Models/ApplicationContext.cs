using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PWApplication.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Transaction> Transactions { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)       
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Transaction>(Transaction =>
            {
                Transaction.HasOne(x => x.User)
                    .WithMany(x => x.Transactions)
                    .HasForeignKey(x => x.UserId)
                    .HasConstraintName("FK_dbo.Transactions_dbo.Users_UserId");
                Transaction.HasOne(x => x.Correspondent)
                   .WithMany(x => x.CorrespondentTransactions)
                   .HasForeignKey(x => x.CorrespondentId)
                   .HasConstraintName("FK_dbo.Transactions_dbo.Users_CorrespondentId");
            });
        }
    }
}
