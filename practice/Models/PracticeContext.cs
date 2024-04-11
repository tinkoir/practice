using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace practice.Models;

public partial class PracticeContext : DbContext
{
    public PracticeContext()
    {
    }

    public PracticeContext(DbContextOptions<PracticeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=practice;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tickets_pkey");

            entity.ToTable("tickets");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Client).HasColumnName("client");
            entity.Property(e => e.ClosingDate).HasColumnName("closing_date");
            entity.Property(e => e.CreationDate).HasColumnName("creation_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Device).HasColumnName("device");
            entity.Property(e => e.EngineerId).HasColumnName("engineer_id");
            entity.Property(e => e.ProblemType).HasColumnName("problem_type");
            entity.Property(e => e.TicketStatus).HasColumnName("ticket_status");

            entity.HasOne(d => d.Engineer).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.EngineerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tickets_engineer_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Login).HasColumnName("login");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
