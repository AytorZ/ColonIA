using System;
using System.Collections.Generic;
using ColonAI.Models;
using Microsoft.EntityFrameworkCore;

namespace ColonAI.Data;

public partial class DbColonIaContext : DbContext
{
    public DbColonIaContext()
    {
    }

    public DbColonIaContext(DbContextOptions<DbColonIaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriaInventario> CategoriaInventarios { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<RolUsuario> RolUsuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:colonaisqldb.database.windows.net,1433;Initial Catalog=DbColonIA;Persist Security Info=False;User ID=AdminSQLcolonAI;Password=MiClaveSegura2025q2ColonAI!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriaInventario>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__4A033A930F04CA0E");

            entity.ToTable("CategoriaInventario");

            entity.Property(e => e.IdCategoria).HasColumnName("Id_categoria");
            entity.Property(e => e.NombreCategoria).HasMaxLength(100);
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdActivo).HasName("PK__Inventar__9121353B2A79AEE1");

            entity.ToTable("Inventario");

            entity.Property(e => e.IdActivo).HasColumnName("Id_Activo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdCategoria).HasColumnName("Id_categoria");
            entity.Property(e => e.NombreDelActivo).HasMaxLength(100);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventari__Id_ca__17F790F9");
        });

        modelBuilder.Entity<RolUsuario>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__RolUsuar__46CA8DBE1F681C9A");

            entity.ToTable("RolUsuario");

            entity.Property(e => e.IdRole).HasColumnName("Id_role");
            entity.Property(e => e.NombreRole).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__EF59F76208642B73");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Correo, "UQ__Usuario__60695A19B6572ABA").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("Id_usuario");
            entity.Property(e => e.Contrasena).HasMaxLength(500);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasColumnType("text");
            entity.Property(e => e.IdRole).HasColumnName("Id_role");
            entity.Property(e => e.NombreCompleto).HasMaxLength(250);
            entity.Property(e => e.Telefono).HasMaxLength(20);

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__Id_role__123EB7A3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
