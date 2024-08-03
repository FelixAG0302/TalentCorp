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
            entity.HasKey(e => e.Id).HasName("PK__Candidat__3213E83FFF56908D");

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("datetime")
                .HasColumnName("fecha_ingreso");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Educacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Educacio__3213E83FDAE5EEEC");

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
                .HasConstraintName("FK__Educacion__candi__1920BF5C");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empleado__3213E83F6021C439");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.CandidatoId).HasColumnName("candidato_id");
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

            entity.HasOne(d => d.Candidato).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.CandidatoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Empleados__candi__20C1E124");

            entity.HasOne(d => d.Puesto).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.PuestoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Empleados__puest__21B6055D");
        });

        modelBuilder.Entity<Entrevista>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Entrevis__3213E83FEC6D5A89");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CandidatoId).HasColumnName("candidato_id");
            entity.Property(e => e.FechaEntrevista)
                .HasColumnType("datetime")
                .HasColumnName("fecha_entrevista");
            entity.Property(e => e.PuestoId).HasColumnName("puesto_id");

            entity.HasOne(d => d.Candidato).WithMany(p => p.Entrevista)
                .HasForeignKey(d => d.CandidatoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entrevist__candi__1BFD2C07");

            entity.HasOne(d => d.Puesto).WithMany(p => p.Entrevista)
                .HasForeignKey(d => d.PuestoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entrevist__puest__1CF15040");
        });

        modelBuilder.Entity<ExperienciaLaboral>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Experien__3213E83F236AA580");

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
                .HasConstraintName("FK__Experienc__candi__164452B1");
        });

        modelBuilder.Entity<Puesto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Puestos__3213E83F7BC17294");

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
            entity.HasKey(e => e.Id).HasName("PK__Roles__3213E83F24C8511A");

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
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3213E83FC14CEF6A");

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
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3213E83FA4B26FE6");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Rol).WithMany(p => p.UsuariosRoles)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuariosR__rol_i__29572725");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuariosRoles)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuariosR__usuar__286302EC");
        });
    }
}