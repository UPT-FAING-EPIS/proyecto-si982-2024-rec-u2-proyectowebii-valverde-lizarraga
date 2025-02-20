using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestionReservasWebII.Models;

public partial class ShortenContext : DbContext
{
    public ShortenContext()
    {
    }

    public ShortenContext(DbContextOptions<ShortenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administradore> Administradores { get; set; }

    public virtual DbSet<Aula> Aulas { get; set; }

    public virtual DbSet<DisponibilidadHorario> DisponibilidadHorarios { get; set; }

    public virtual DbSet<Docente> Docentes { get; set; }

    public virtual DbSet<EstadoIncidencium> EstadoIncidencia { get; set; }

    public virtual DbSet<EstadoRecurso> EstadoRecursos { get; set; }

    public virtual DbSet<EstadoReserva> EstadoReservas { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<HistorialReserva> HistorialReservas { get; set; }

    public virtual DbSet<Incidencia> Incidencias { get; set; }

    public virtual DbSet<Laboratorio> Laboratorios { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TipoBien> TipoBiens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:bd-proyecto-valverde-lizarraga.database.windows.net,1433;Initial Catalog=shorten;Persist Security Info=False;User ID=jeanvalverde;Password=valverde24c++;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administradore>(entity =>
        {
            entity.HasKey(e => e.IdAdministrador).HasName("PK__Administ__0FE822AA1E2E39AA");

            entity.HasIndex(e => e.Correo, "UQ__Administ__2A586E0B1E3029B5").IsUnique();

            entity.HasIndex(e => e.Dni, "UQ__Administ__D87608A71EDADCB9").IsUnique();

            entity.Property(e => e.IdAdministrador).HasColumnName("id_administrador");
            entity.Property(e => e.Apellido)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Correo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("dni");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.Telefono)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Administradores)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Administr__id_ro__10566F31");
        });

        modelBuilder.Entity<Aula>(entity =>
        {
            entity.HasKey(e => e.IdAula).HasName("PK__Aulas__B19134FEF806E5E2");

            entity.HasIndex(e => e.Nombre, "UQ__Aulas__72AFBCC67691AA5D").IsUnique();

            entity.Property(e => e.IdAula).HasColumnName("id_aula");
            entity.Property(e => e.CantidadCarpetas).HasColumnName("cantidad_carpetas");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.RegistradoPor).HasColumnName("registrado_por");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Aulas)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Aulas__id_estado__114A936A");

            entity.HasOne(d => d.RegistradoPorNavigation).WithMany(p => p.Aulas)
                .HasForeignKey(d => d.RegistradoPor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Aulas__registrad__123EB7A3");
        });

        modelBuilder.Entity<DisponibilidadHorario>(entity =>
        {
            entity.HasKey(e => e.IdHorario).HasName("PK__Disponib__C5836D6983B020AD");

            entity.ToTable("Disponibilidad_Horarios");

            entity.Property(e => e.IdHorario).HasColumnName("id_horario");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.HoraFin)
                .HasPrecision(0)
                .HasColumnName("hora_fin");
            entity.Property(e => e.HoraInicio)
                .HasPrecision(0)
                .HasColumnName("hora_inicio");
            entity.Property(e => e.IdAula).HasColumnName("id_aula");
            entity.Property(e => e.IdLaboratorio).HasColumnName("id_laboratorio");

            entity.HasOne(d => d.IdAulaNavigation).WithMany(p => p.DisponibilidadHorarios)
                .HasForeignKey(d => d.IdAula)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Disponibi__id_au__1332DBDC");

            entity.HasOne(d => d.IdLaboratorioNavigation).WithMany(p => p.DisponibilidadHorarios)
                .HasForeignKey(d => d.IdLaboratorio)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Disponibi__id_la__14270015");
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.HasKey(e => e.IdDocente).HasName("PK__Docentes__300DB21137B4D630");

            entity.HasIndex(e => e.Correo, "UQ__Docentes__2A586E0B5D79A91E").IsUnique();

            entity.Property(e => e.IdDocente).HasColumnName("id_docente");
            entity.Property(e => e.Apellido)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Correo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.Telefono)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Docentes)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Docentes__id_rol__151B244E");
        });

        modelBuilder.Entity<EstadoIncidencium>(entity =>
        {
            entity.HasKey(e => e.IdEstadoIncidencia).HasName("PK__Estado_I__323C5237A09A61D4");

            entity.ToTable("Estado_Incidencia");

            entity.HasIndex(e => e.NombreEstado, "UQ__Estado_I__2F8C6375CDD2396E").IsUnique();

            entity.Property(e => e.IdEstadoIncidencia).HasColumnName("id_estado_incidencia");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("nombre_estado");
        });

        modelBuilder.Entity<EstadoRecurso>(entity =>
        {
            entity.HasKey(e => e.IdEstadoRecurso).HasName("PK__Estado_R__698AEEC3BAD930C4");

            entity.ToTable("Estado_Recurso");

            entity.HasIndex(e => e.NombreEstado, "UQ__Estado_R__2F8C6375BBCA864D").IsUnique();

            entity.Property(e => e.IdEstadoRecurso).HasColumnName("id_estado_recurso");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("nombre_estado");
        });

        modelBuilder.Entity<EstadoReserva>(entity =>
        {
            entity.HasKey(e => e.IdEstadoReserva).HasName("PK__Estado_R__D1D0D7E0262540C0");

            entity.ToTable("Estado_Reserva");

            entity.HasIndex(e => e.NombreEstado, "UQ__Estado_R__2F8C6375234EEA62").IsUnique();

            entity.Property(e => e.IdEstadoReserva).HasColumnName("id_estado_reserva");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("nombre_estado");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante).HasName("PK__Estudian__E0B2763CA8BCB962");

            entity.HasIndex(e => e.CodigoUniversitario, "UQ__Estudian__3DDB00F5857CE4F3").IsUnique();

            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.Apellido)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.CodigoUniversitario)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo_universitario");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.Telefono)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Estudiant__id_ro__160F4887");
        });

        modelBuilder.Entity<HistorialReserva>(entity =>
        {
            entity.HasKey(e => e.IdHistorial).HasName("PK__Historia__76E6C50298DDA288");

            entity.ToTable("Historial_Reservas");

            entity.Property(e => e.IdHistorial).HasColumnName("id_historial");
            entity.Property(e => e.FechaModificacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.IdEstadoActual).HasColumnName("id_estado_actual");
            entity.Property(e => e.IdEstadoAnterior).HasColumnName("id_estado_anterior");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.IdReserva).HasColumnName("id_reserva");

            entity.HasOne(d => d.IdEstadoActualNavigation).WithMany(p => p.HistorialReservaIdEstadoActualNavigations)
                .HasForeignKey(d => d.IdEstadoActual)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__id_es__18EBB532");

            entity.HasOne(d => d.IdEstadoAnteriorNavigation).WithMany(p => p.HistorialReservaIdEstadoAnteriorNavigations)
                .HasForeignKey(d => d.IdEstadoAnterior)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__id_es__17F790F9");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.HistorialReservas)
                .HasForeignKey(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__id_es__17036CC0");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.HistorialReservas)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__id_re__19DFD96B");
        });

        modelBuilder.Entity<Incidencia>(entity =>
        {
            entity.HasKey(e => e.IdIncidencia).HasName("PK__Incidenc__D7757E76DCB4FCBD");

            entity.Property(e => e.IdIncidencia).HasColumnName("id_incidencia");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaReporte)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_reporte");
            entity.Property(e => e.IdAula).HasColumnName("id_aula");
            entity.Property(e => e.IdEstadoIncidencia).HasColumnName("id_estado_incidencia");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.IdLaboratorio).HasColumnName("id_laboratorio");

            entity.HasOne(d => d.IdAulaNavigation).WithMany(p => p.Incidencia)
                .HasForeignKey(d => d.IdAula)
                .HasConstraintName("FK__Incidenci__id_au__1AD3FDA4");

            entity.HasOne(d => d.IdEstadoIncidenciaNavigation).WithMany(p => p.Incidencia)
                .HasForeignKey(d => d.IdEstadoIncidencia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Incidenci__id_es__1CBC4616");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Incidencia)
                .HasForeignKey(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Incidenci__id_es__1BC821DD");

            entity.HasOne(d => d.IdLaboratorioNavigation).WithMany(p => p.Incidencia)
                .HasForeignKey(d => d.IdLaboratorio)
                .HasConstraintName("FK__Incidenci__id_la__1DB06A4F");
        });

        modelBuilder.Entity<Laboratorio>(entity =>
        {
            entity.HasKey(e => e.IdLaboratorio).HasName("PK__Laborato__781B42E21A4C30C1");

            entity.HasIndex(e => e.Nombre, "UQ__Laborato__72AFBCC6E88B55D1").IsUnique();

            entity.Property(e => e.IdLaboratorio).HasColumnName("id_laboratorio");
            entity.Property(e => e.CantidadComputadoras).HasColumnName("cantidad_computadoras");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.RegistradoPor).HasColumnName("registrado_por");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Laboratorios)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Laborator__id_es__1EA48E88");

            entity.HasOne(d => d.RegistradoPorNavigation).WithMany(p => p.Laboratorios)
                .HasForeignKey(d => d.RegistradoPor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Laborator__regis__1F98B2C1");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__Reservas__423CBE5D7226E57B");

            entity.Property(e => e.IdReserva).HasColumnName("id_reserva");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaReserva).HasColumnName("fecha_reserva");
            entity.Property(e => e.IdAula).HasColumnName("id_aula");
            entity.Property(e => e.IdEstadoReserva).HasColumnName("id_estado_reserva");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.IdHorario).HasColumnName("id_horario");
            entity.Property(e => e.IdLaboratorio).HasColumnName("id_laboratorio");
            entity.Property(e => e.IdTipoBien).HasColumnName("id_tipo_bien");
            entity.Property(e => e.NumeroBien).HasColumnName("numero_bien");

            entity.HasOne(d => d.IdAulaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdAula)
                .HasConstraintName("FK__Reservas__id_aul__208CD6FA");

            entity.HasOne(d => d.IdEstadoReservaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdEstadoReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__id_est__22751F6C");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__id_est__2180FB33");

            entity.HasOne(d => d.IdHorarioNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdHorario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__id_hor__236943A5");

            entity.HasOne(d => d.IdLaboratorioNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdLaboratorio)
                .HasConstraintName("FK__Reservas__id_lab__245D67DE");

            entity.HasOne(d => d.IdTipoBienNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdTipoBien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__id_tip__25518C17");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__6ABCB5E0476F119C");

            entity.HasIndex(e => e.NombreRol, "UQ__Roles__673CB4355E7FEFD2").IsUnique();

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombre_rol");
        });

        modelBuilder.Entity<TipoBien>(entity =>
        {
            entity.HasKey(e => e.IdTipoBien).HasName("PK__Tipo_Bie__4517DF573B34A570");

            entity.ToTable("Tipo_Bien");

            entity.HasIndex(e => e.NombreTipo, "UQ__Tipo_Bie__0C60C00D0B794420").IsUnique();

            entity.Property(e => e.IdTipoBien).HasColumnName("id_tipo_bien");
            entity.Property(e => e.NombreTipo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombre_tipo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
