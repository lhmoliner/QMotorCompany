using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace QuadrusMotorCompany.Models
{
    public class QuadrusDatabaseContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public QuadrusDatabaseContext() : base("QuadrusMotorCompany")
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<File> Files { get; set; }
    }
}