using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace dotnet_poc.Models;

public partial class TestContext : DbContext
{
    public TestContext()
    {
    }

    public TestContext(DbContextOptions<TestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tarefa> Tarefas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=server01;database=test;user=root;password=joseaa", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.40-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<Tarefa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tarefas");

            entity.HasIndex(e => e.IdUsuario, "idUsuario");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("int(11)")
                .HasColumnName("idUsuario");
            entity.Property(e => e.Nome).HasMaxLength(255);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Tarefas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("tarefas_ibfk_1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Nome).HasMaxLength(255);
            entity.Property(e => e.Senha).HasMaxLength(255);
            entity.Property(e => e.Telefone).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
