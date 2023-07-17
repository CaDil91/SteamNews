using Microsoft.EntityFrameworkCore;

namespace SteamNews;

public partial class SteamNewsDatabaseContext : DbContext
{
    public SteamNewsDatabaseContext()
    {
    }

    public SteamNewsDatabaseContext(DbContextOptions<SteamNewsDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FollowedSteamApp> FollowedSteamApps { get; set; }

    public virtual DbSet<Webhook> Webhooks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("SteamNewsDbConnectionString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FollowedSteamApp>(entity =>
        {
            entity.HasKey(e => e.AppId).HasName("PK__Followed__8E2CF7F912E22E24");

            entity.ToTable("FollowedSteamApps", "SteamNews");

            entity.Property(e => e.AppId).ValueGeneratedNever();
            entity.Property(e => e.Webhook).HasMaxLength(1000);

            entity.HasOne(d => d.WebhookNavigation).WithMany(p => p.FollowedSteamApps)
                .HasForeignKey(d => d.Webhook)
                .HasConstraintName("FollowedSteamApps_fk1");
        });

        modelBuilder.Entity<Webhook>(entity =>
        {
            entity.HasKey(e => e.WebhookUrl).HasName("PK__Webhooks__F8DBAD8F5EA9CB07");

            entity.ToTable("Webhooks", "SteamNews");

            entity.Property(e => e.WebhookUrl).HasMaxLength(1000);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
