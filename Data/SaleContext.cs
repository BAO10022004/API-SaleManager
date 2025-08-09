using Microsoft.EntityFrameworkCore;
using SaleManagerWebAPI.Models.Entities;

namespace SaleManagerWebAPI.Data
{
    public class SaleContext : DbContext
    {
        public SaleContext(DbContextOptions<SaleContext> options) : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<CodeVetify> CodeVetifies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWID()");

                entity.HasIndex(e => e.Username)
                    .IsUnique()
                    .HasDatabaseName("IX_account_username");

                entity.HasIndex(e => e.Email)
                    .IsUnique()
                    .HasDatabaseName("IX_account_email");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.FullName)
                    .HasMaxLength(100);

                entity.Property(e => e.LastLogin)
                    .HasColumnType("datetime2");
            });
            modelBuilder.Entity<AuthToken>(entity =>
            {
                entity.ToTable("auth_tokens");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWID()");

                entity.HasIndex(e => e.Token)
                    .IsUnique()
                    .HasDatabaseName("IX_auth_tokens_token");

                entity.Property(e => e.Token)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.TokenType)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.ExpiresAt)
                    .HasColumnType("datetime2")
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.LastUsed)
                    .HasColumnType("datetime2");

                entity.Property(e => e.DeviceInfo)
                    .HasMaxLength(255);

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(45);

                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AuthTokens)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_auth_tokens_account");

                entity.HasCheckConstraint("CK_auth_tokens_token_type",
                    "token_type IN ('login', 'refresh', 'api')");
            });
            modelBuilder.Entity<CodeVetify>(entity =>
            {
                entity.ToTable("code_vetify");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWID()");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.CodeVetifies)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_code_vetify_account");


                entity.HasIndex(e => e.Code)
                    .IsUnique()
                    .HasDatabaseName("IX_code_vetify_code");

                entity.Property(e => e.ExpiresAt)
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired();


                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired();

                entity.Property(e => e.VerifiedAt)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

                entity.Property(e => e.DeviceInfo)
                    .HasMaxLength(255);

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(45);

                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true);



            });
        }
    }
}
