using Microsoft.EntityFrameworkCore;

namespace ApiServiceForAristocrat1.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Models.Model.Product> Products { get; set; }
        public DbSet<Models.Model.User> Users { get; set; }
        public DbSet<Models.Model.Admin> Admins { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DataForAPIAristokratia2024v1.db");
        }
    }
}
