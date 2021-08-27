
using DL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DL.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<Subscriber> Subscribers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=testDB.db;");
        }
    }
}
