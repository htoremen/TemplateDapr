using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<LoginHistory> LoginHistories { get; set; } = null!;
        public virtual DbSet<Parameter> Parameters { get; set; }
        public virtual DbSet<ParameterType> ParameterTypes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-2LN0LUU;Initial Catalog=TemplateIdentity;Integrated Security=True; TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshToken");

                entity.Property(e => e.RefreshTokenId).HasMaxLength(50);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedByIp).HasMaxLength(50);

                entity.Property(e => e.Expires).HasColumnType("datetime");

                entity.Property(e => e.ReasonRevoked).HasMaxLength(50);

                entity.Property(e => e.ReplacedByToken).HasMaxLength(550);

                entity.Property(e => e.Revoked).HasColumnType("datetime");

                entity.Property(e => e.RevokedByIp).HasMaxLength(50);

                entity.Property(e => e.Token).HasMaxLength(550);

                entity.Property(e => e.UserId).HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_RefreshToken_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.PasswordHash).HasMaxLength(550);

                entity.Property(e => e.Username).HasMaxLength(50);
            });
        }
    }
}