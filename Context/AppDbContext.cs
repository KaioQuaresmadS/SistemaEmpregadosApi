using Microsoft.EntityFrameworkCore;
using EmpregadoApi.Models;

namespace EmpregadoApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cadastro> cadastroempregado { get; set; }
    }
}
