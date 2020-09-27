using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace TxtCPU
{
    class DictionContext : DbContext
    {
        public DbSet<Diction> Dict { get; set; }

        public DictionContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = ConfigurationManager.AppSettings.Get("connectionString");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
