using Microsoft.EntityFrameworkCore;
using ExoApi.Models;

namespace ExoApi.Contexts
{
    public class ExoContext : DbContext
    {
        public ExoContext() { }
        public ExoContext(DbContextOptions<ExoContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=ExoApi;Integrated Security=True;TrustServerCertificate=True;");
            }
        }

        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
