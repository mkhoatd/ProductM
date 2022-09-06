using Microsoft.EntityFrameworkCore;

namespace ProductM.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Category { get; set; }

        //Chen ngang khi tạo model 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Voi 1 category se co nhieu product
            modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);
        }
    }
}