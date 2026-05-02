using Microsoft.EntityFrameworkCore;
using Ztracena_Tlapka_Backend.Domain.Entities;

namespace Ztracena_Tlapka_Backend.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("users");
            e.Property(u => u.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");
            e.Property(u => u.FirstName).HasColumnName("first_name").HasMaxLength(80);
            e.Property(u => u.LastName).HasColumnName("last_name").HasMaxLength(80);
            e.Property(u => u.Email).HasColumnName("email").HasMaxLength(255);
            e.HasIndex(u => u.Email).IsUnique();
            e.Property(u => u.PasswordHash).HasColumnName("password_hash");
            e.Property(u => u.Phone).HasColumnName("phone").HasMaxLength(30);
            e.HasIndex(u => u.Phone).IsUnique();
            e.Property(u => u.Region).HasColumnName("region").HasMaxLength(100);
            e.Property(u => u.District).HasColumnName("district").HasMaxLength(100);
            e.Property(u => u.City).HasColumnName("city").HasMaxLength(100);
            e.Property(u => u.Street).HasColumnName("street").HasMaxLength(200);
            e.Property(u => u.PostalCode).HasColumnName("postal_code").HasMaxLength(10);
            e.Property(u => u.NewsletterSubscribed).HasColumnName("newsletter_subscribed").HasDefaultValue(false);
            e.Property(u => u.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("NOW()");
            e.Property(u => u.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("NOW()");
        });
    }
}
