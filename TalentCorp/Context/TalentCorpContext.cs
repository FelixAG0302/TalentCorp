using Microsoft.EntityFrameworkCore;
using TalentCorp.Entities;

namespace TalentCorp.Context;

public class TalentCorpContext(DbContextOptions<TalentCorpContext> options) : DbContext(options)
{
    public virtual DbSet<Candidato> Candidatos { get; set; }

    public virtual DbSet<Educacion> Educacions { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Entrevista> Entrevistas { get; set; }

    public virtual DbSet<ExperienciaLaboral> ExperienciaLaborals { get; set; }

    public virtual DbSet<Puesto> Puestos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuariosRole> UsuariosRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Candidat__3213E83FCAB39976");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Cédula)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("cédula");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("datetime")
                .HasColumnName("fecha_ingreso");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PuestoId).HasColumnName("puesto_id");

            entity.HasOne(d => d.Puesto).WithMany(p => p.Candidatos)
                .HasForeignKey(d => d.PuestoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Candidato__puest__145C0A3F");
        });

        modelBuilder.Entity<Educacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Educacio__3213E83FE23C6C7C");

            entity.ToTable("Educacion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CandidatoId).HasColumnName("candidato_id");
            entity.Property(e => e.FechaDesde)
                .HasColumnType("datetime")
                .HasColumnName("fecha_desde");
            entity.Property(e => e.FechaHasta)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hasta");
            entity.Property(e => e.Idiomas)
                .HasMaxLength(255)
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
                .HasConstraintName("FK__Educacion__candi__1A14E395");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empleado__3213E83F678E2BB2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Cedula)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("cedula");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("datetime")
                .HasColumnName("fecha_ingreso");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PuestoId).HasColumnName("puesto_id");

            entity.HasOne(d => d.Puesto).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.PuestoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Empleados__puest__20C1E124");
        });

        modelBuilder.Entity<Entrevista>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Entrevis__3213E83FF75406CE");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CandidatoId).HasColumnName("candidato_id");
            entity.Property(e => e.FechaEntrevista)
                .HasColumnType("datetime")
                .HasColumnName("fecha_entrevista");

            entity.HasOne(d => d.Candidato).WithMany(p => p.Entrevista)
                .HasForeignKey(d => d.CandidatoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entrevist__candi__1CF15040");
        });

        modelBuilder.Entity<ExperienciaLaboral>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Experien__3213E83FF8B39D50");

            entity.ToTable("ExperienciaLaboral");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CandidatoId).HasColumnName("candidato_id");
            entity.Property(e => e.Empresa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("empresa");
            entity.Property(e => e.FechaDesde)
                .HasColumnType("datetime")
                .HasColumnName("fecha_desde");
            entity.Property(e => e.FechaHasta)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hasta");
            entity.Property(e => e.PuestoOcupado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("puesto_ocupado");
            entity.Property(e => e.Salario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salario");

            entity.HasOne(d => d.Candidato).WithMany(p => p.ExperienciaLaborals)
                .HasForeignKey(d => d.CandidatoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Experienc__candi__173876EA");
        });

        modelBuilder.Entity<Puesto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Puestos__3213E83F821AE48D");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
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
                .HasColumnName("salario_max");
            entity.Property(e => e.SalarioMin)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salario_min");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3213E83FFF376B86");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3213E83F08791760");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contrasena");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<UsuariosRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3213E83FD758D7DD");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Rol).WithMany(p => p.UsuariosRoles)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuariosR__rol_i__286302EC");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuariosRoles)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuariosR__usuar__276EDEB3");
        });
    }
}