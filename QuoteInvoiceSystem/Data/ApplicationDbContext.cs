using Microsoft.EntityFrameworkCore;
using QuoteInvoiceAPI.Models;

namespace QuoteInvoiceAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensure Clients are created before Projects
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Client)
                .WithMany()
                .HasForeignKey(p => p.ClientID)
                .OnDelete(DeleteBehavior.Cascade); // Projects can be deleted when Client is deleted

            // Ensure Clients exist before Quotes reference them
            modelBuilder.Entity<Quote>()
                .HasOne(q => q.Client)
                .WithMany()
                .HasForeignKey(q => q.ClientID)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            // Ensure Projects exist before Quotes reference them
            modelBuilder.Entity<Quote>()
                .HasOne(q => q.Project)
                .WithMany()
                .HasForeignKey(q => q.ProjectID)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            base.OnModelCreating(modelBuilder);
        }

    }
}
