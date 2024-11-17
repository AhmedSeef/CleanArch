using CleanArch.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infra.Data.Context
{
    public class DomainDataContext : DbContext
    {
        public DomainDataContext(DbContextOptions<DomainDataContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
    }
}
