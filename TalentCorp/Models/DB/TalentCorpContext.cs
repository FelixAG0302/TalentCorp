using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TalentCorp.Models.DB;

public partial class TalentCorpContext : DbContext
{
    public TalentCorpContext()
    {
    }

    public TalentCorpContext(DbContextOptions<TalentCorpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Candidato> Candidatos { get; set; }

    public virtual DbSet<Educacion> Educacions { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Entrevista> Entrevistas { get; set; }

    public virtual DbSet<ExperienciaLaboral> ExperienciaLaborals { get; set; }

    public virtual DbSet<Puesto> Puestos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
       /// => optionsBuilder.UseSqlServer("Server=MSI; Database=TalentCorp; integrated security=true; Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidato>(entity =>
        {
            entity.HasKey(e => e.CandidatoId).HasName("PK__Candidat__755821FB783D186D");

            entity.Property(e => e.CandidatoId).HasColumnName("candidatoID");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Cédula)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("cédula");
            entity.Property(e => e.Departamento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("departamento");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("datetime")
                .HasColumnName("fechaIngreso");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Educacion>(entity =>
        {
            entity.HasKey(e => e.EducacionId).HasName("PK__Educacio__2C0FD6DE298C196C");

            entity.ToTable("Educacion");

            entity.Property(e => e.EducacionId)
                .ValueGeneratedNever()
                .HasColumnName("educacionID");
            entity.Property(e => e.CandidatoId).HasColumnName("candidatoID");
            entity.Property(e => e.FechaDesde)
                .HasColumnType("datetime")
                .HasColumnName("fechaDesde");
            entity.Property(e => e.FechaHasta)
                .HasColumnType("datetime")
                .HasColumnName("fechaHasta");
            entity.Property(e => e.Idiomas)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idiomas");
            entity.Property(e => e.Institucion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("institucion");
            entity.Property(e => e.Nivel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nivel");

            entity.HasOne(d => d.Candidato).WithMany(p => p.Educacions)
                .HasForeignKey(d => d.CandidatoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Educacion__candi__3F466844");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.EmpleadoId).HasName("PK__Empleado__CCDD501871F2BF65");

            entity.Property(e => e.EmpleadoId).HasColumnName("empleadoID");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.CandidatoId).HasColumnName("candidatoID");
            entity.Property(e => e.Cedula)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("cedula");
            entity.Property(e => e.Departamento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("departamento");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("datetime")
                .HasColumnName("fechaIngreso");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PuestoId).HasColumnName("puestoID");

            entity.HasOne(d => d.Candidato).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.CandidatoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Empleados__candi__46E78A0C");

            entity.HasOne(d => d.Puesto).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.PuestoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Empleados__puest__47DBAE45");
        });

        modelBuilder.Entity<Entrevista>(entity =>
        {
            entity.HasKey(e => e.EntrevistaId).HasName("PK__Entrevis__AD182AE912988B07");

            entity.Property(e => e.EntrevistaId)
                .ValueGeneratedNever()
                .HasColumnName("entrevistaID");
            entity.Property(e => e.CandidatoId).HasColumnName("candidatoID");
            entity.Property(e => e.FechaEntrevista)
                .HasColumnType("datetime")
                .HasColumnName("fechaEntrevista");
            entity.Property(e => e.PuestoId).HasColumnName("puestoID");

            entity.HasOne(d => d.Candidato).WithMany(p => p.Entrevista)
                .HasForeignKey(d => d.CandidatoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entrevist__candi__4222D4EF");

            entity.HasOne(d => d.Puesto).WithMany(p => p.Entrevista)
                .HasForeignKey(d => d.PuestoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entrevist__puest__4316F928");
        });

        modelBuilder.Entity<ExperienciaLaboral>(entity =>
        {
            entity.HasKey(e => e.ExperienciaId).HasName("PK__Experien__8544FFC0A3EF0000");

            entity.ToTable("ExperienciaLaboral");

            entity.Property(e => e.ExperienciaId)
                .ValueGeneratedNever()
                .HasColumnName("experienciaID");
            entity.Property(e => e.CandidatoId).HasColumnName("candidatoID");
            entity.Property(e => e.Empresa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("empresa");
            entity.Property(e => e.FechaDesde)
                .HasColumnType("datetime")
                .HasColumnName("fechaDesde");
            entity.Property(e => e.FechaHasta)
                .HasColumnType("datetime")
                .HasColumnName("fechaHasta");
            entity.Property(e => e.PuestoOcupado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("puestoOcupado");
            entity.Property(e => e.Salario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salario");

            entity.HasOne(d => d.Candidato).WithMany(p => p.ExperienciaLaborals)
                .HasForeignKey(d => d.CandidatoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Experienc__candi__3C69FB99");
        });

        modelBuilder.Entity<Puesto>(entity =>
        {
            entity.HasKey(e => e.PuestoId).HasName("PK__Puestos__2BAB308272487B6C");

            entity.Property(e => e.PuestoId).HasColumnName("puestoID");
            entity.Property(e => e.Descripción)
                .HasColumnType("text")
                .HasColumnName("descripción");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NivelRiesgo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nivel_riesgo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.SalarioMax)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salarioMax");
            entity.Property(e => e.SalarioMin)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salarioMin");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__CB9A1CDFED66EF23");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
