using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IndustryIncident.Models;

public partial class IndustryIncidentContext : DbContext
{
    public IndustryIncidentContext()
    {
    }

    public IndustryIncidentContext(DbContextOptions<IndustryIncidentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Incident> Incidents { get; set; }

    public virtual DbSet<IncidentType> IncidentTypes { get; set; }

    public virtual DbSet<Indicator> Indicators { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAcce> UserAcces { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Zone> Zones { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("db_owner");

        modelBuilder.Entity<Incident>(entity =>
        {
            entity.ToTable("Incident", "dbo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Objectif).HasColumnType("Objectif");
            entity.Property(e => e.Taux).HasColumnType("Taux");
            entity.Property(e => e.Iduser)
                .HasMaxLength(250)
                .HasColumnName("IDUser");
        });

        modelBuilder.Entity<IncidentType>(entity =>
        {
            entity.ToTable("IncidentType", "dbo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<Indicator>(entity =>
        {
            entity.ToTable("indicator");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(250);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role", "dbo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Role1)
                .HasMaxLength(50)
                .HasColumnName("Role");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User", "dbo");

            entity.Property(e => e.Id)
                .HasMaxLength(250)
                .HasColumnName("ID");
            entity.Property(e => e.FamillyName).HasMaxLength(250);
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(250);

        });

        modelBuilder.Entity<UserAcce>(entity =>
        {
            entity.HasKey(e => new { e.Idzone, e.Iduser });

            entity.Property(e => e.Idzone).HasColumnName("IDZone");
            entity.Property(e => e.Iduser)
                .HasMaxLength(250)
                .HasColumnName("IDUser");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.Iduser, e.Idrole });

            entity.ToTable("UserRole", "dbo");

            entity.Property(e => e.Iduser)
                .HasMaxLength(250)
                .HasColumnName("IDUser");
            entity.Property(e => e.Idrole).HasColumnName("IDRole");
        });

        modelBuilder.Entity<Zone>(entity =>
        {
            entity.ToTable("Zone");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
