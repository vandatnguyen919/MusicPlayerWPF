using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;

namespace Repositories;

public partial class MusicPlayerPrn212assignmentContext : DbContext
{
    public MusicPlayerPrn212assignmentContext()
    {
    }

    public MusicPlayerPrn212assignmentContext(DbContextOptions<MusicPlayerPrn212assignmentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MediaEntity> MediaEntities { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(local);Database=MusicPlayerPRN212Assignment;UID=sa;PWD=12345;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MediaEntity>(entity =>
        {
            entity.HasKey(e => e.MediaId).HasName("PK__MediaEnt__B2C2B5CFC5FB2BF8");

            entity.ToTable("MediaEntity");

            entity.Property(e => e.Album).HasMaxLength(255);
            entity.Property(e => e.Artist).HasMaxLength(255);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.Genre).HasMaxLength(100);
            entity.Property(e => e.TrackName).HasMaxLength(255);
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.PlaylistId).HasName("PK__Playlist__B30167A0475F903E");

            entity.ToTable("Playlist");

            entity.Property(e => e.PlaylistName).HasMaxLength(255);

            entity.HasMany(d => d.MediaEntities).WithMany(p => p.Playlists)
                .UsingEntity<Dictionary<string, object>>(
                    "PlaylistMedium",
                    r => r.HasOne<MediaEntity>().WithMany()
                        .HasForeignKey("MediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__PlaylistM__Media__4E88ABD4"),
                    l => l.HasOne<Playlist>().WithMany()
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__PlaylistM__Playl__4D94879B"),
                    j =>
                    {
                        j.HasKey("PlaylistId", "MediaId").HasName("PK__Playlist__582D4CFC513A4E75");
                        j.ToTable("PlaylistMedia");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
