using Core.Models.Abstarct;
using Core.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }

        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity baseEntity)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                baseEntity.CreateDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(baseEntity).Property(x => x.CreateDate).IsModified = false;
                                baseEntity.UpdateDate = DateTime.Now;
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if(item.Entity is BaseEntity baseEntity)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                baseEntity.CreateDate = DateTime.Now;
                                break;
                            }
                            case EntityState.Modified:
                            {
                                Entry(baseEntity).Property(x => x.CreateDate).IsModified = false;
                                baseEntity.UpdateDate = DateTime.Now;
                                break;
                            }
                        default:
                            break;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

    }
}
