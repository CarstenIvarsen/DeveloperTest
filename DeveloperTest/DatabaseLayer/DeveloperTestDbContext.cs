using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DeveloperTest.DatabaseLayer
{
    public partial class DeveloperTestDbContext : DbContext
    {
        public DeveloperTestDbContext()
        {
        }

        public DeveloperTestDbContext(DbContextOptions<DeveloperTestDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TextEngineResponseList> TextEngineResponseLists { get; set; }
        public virtual DbSet<WatchList> WatchLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TextEngineResponseList>(entity =>
            {
                entity.ToTable("TextEngineResponseList");

                entity.Property(e => e.Created).HasDefaultValueSql("(sysdatetime())");
            });

            modelBuilder.Entity<WatchList>(entity =>
            {
                entity.ToTable("WatchList");

                entity.Property(e => e.Created).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Word)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
