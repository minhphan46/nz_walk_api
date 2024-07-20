using Microsoft.EntityFrameworkCore;
using UdemyProject1.Entities;

namespace UdemyProject1.Data.DbContexts
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<WalkCategory> WalkCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-many relationship between Walk and Category
            modelBuilder.Entity<WalkCategory>()
                .HasKey(wt => new { wt.WalkId, wt.CategoryId });
            modelBuilder.Entity<WalkCategory>()
                .HasOne(wt => wt.Walk)
                .WithMany(w => w.WalkCategories)
                .HasForeignKey(wt => wt.WalkId);
            modelBuilder.Entity<WalkCategory>()
                .HasOne(wt => wt.Category)
                .WithMany(t => t.WalkCategories)
                .HasForeignKey(wt => wt.CategoryId);

            // One to One relationship between Walk and Difficulty
            modelBuilder.Entity<Walk>()
                .HasOne(w => w.Difficulty);

            // One to One relationship between Walk and Region
            modelBuilder.Entity<Walk>()
                .HasOne(w => w.Region);
        }
    }
}
