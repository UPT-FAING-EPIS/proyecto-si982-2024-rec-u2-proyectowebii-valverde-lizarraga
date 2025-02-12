using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestionReservasWebII.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Evaluacione> Evaluaciones { get; set; }

    public virtual DbSet<HistorialNormativa> HistorialNormativas { get; set; }

    public virtual DbSet<Incidencia> Incidencias { get; set; }

    public virtual DbSet<LogsAuditorium> LogsAuditoria { get; set; }

    public virtual DbSet<Notificacione> Notificaciones { get; set; }

    public virtual DbSet<Recurso> Recursos { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=VALVERDE;Database=BD_RECWEB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Evaluacione>(entity =>
        {
            entity.HasKey(e => e.EvaluacionId).HasName("PK__Evaluaci__99ABA8A54CBF0EDA");

            entity.ToTable(tb => tb.HasTrigger("TRG_NotificarEvaluacionBaja"));

            entity.Property(e => e.EvaluacionId).HasColumnName("EvaluacionID");
            entity.Property(e => e.FechaEvaluacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReservaId).HasColumnName("ReservaID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Reserva).WithMany(p => p.Evaluaciones)
                .HasForeignKey(d => d.ReservaId)
                .HasConstraintName("FK_Evaluaciones_Reservas");
        });

        modelBuilder.Entity<HistorialNormativa>(entity =>
        {
            entity.HasKey(e => e.NormativaId).HasName("PK__Historia__24A4F79B38F3E8FD");

            entity.Property(e => e.NormativaId).HasColumnName("NormativaID");
            entity.Property(e => e.FechaAceptacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.HistorialNormativas)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_HistorialNormativas_Usuarios");
        });

        modelBuilder.Entity<Incidencia>(entity =>
        {
            entity.HasKey(e => e.IncidenciaId).HasName("PK__Incidenc__E41133C699177A48");

            entity.Property(e => e.IncidenciaId).HasColumnName("IncidenciaID");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasDefaultValue("Pendiente");
            entity.Property(e => e.FechaReporte)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaResolucion).HasColumnType("datetime");
            entity.Property(e => e.RecursoId).HasColumnName("RecursoID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Recurso).WithMany(p => p.Incidencia)
                .HasForeignKey(d => d.RecursoId)
                .HasConstraintName("FK_Incidencias_Recursos");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Incidencia)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_Incidencias_Usuarios");
        });

        modelBuilder.Entity<LogsAuditorium>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__LogsAudi__5E5499A8C47DE5B5");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.Accion).HasMaxLength(255);
            entity.Property(e => e.FechaAccion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.LogsAuditoria)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_LogsAuditoria_Usuarios");
        });

        modelBuilder.Entity<Notificacione>(entity =>
        {
            entity.HasKey(e => e.NotificacionId).HasName("PK__Notifica__BCC120C4400102FE");

            entity.Property(e => e.NotificacionId).HasColumnName("NotificacionID");
            entity.Property(e => e.FechaEnvio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Titulo).HasMaxLength(255);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_Notificaciones_Usuarios");
        });

        modelBuilder.Entity<Recurso>(entity =>
        {
            entity.HasKey(e => e.RecursoId).HasName("PK__Recursos__82F2B1A42D042F2E");

            entity.ToTable(tb => tb.HasTrigger("TRG_ActualizarRecursos"));

            entity.Property(e => e.RecursoId).HasColumnName("RecursoID");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasDefaultValue("Disponible");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(255);
            entity.Property(e => e.Tipo).HasMaxLength(50);
            entity.Property(e => e.Ubicacion).HasMaxLength(255);
            entity.Property(e => e.UltimaActualizacion).HasColumnType("datetime");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.ReservaId).HasName("PK__Reservas__C399370336228323");

            entity.ToTable(tb => tb.HasTrigger("TRG_ActualizarReservas"));

            entity.Property(e => e.ReservaId).HasColumnName("ReservaID");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasDefaultValue("Activa");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaReserva).HasColumnType("datetime");
            entity.Property(e => e.HoraFin).HasColumnType("datetime");
            entity.Property(e => e.HoraInicio).HasColumnType("datetime");
            entity.Property(e => e.RecursoId).HasColumnName("RecursoID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Recurso).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.RecursoId)
                .HasConstraintName("FK_Reservas_Recursos");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_Reservas_Usuarios");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE798534D4404");

            entity.HasIndex(e => e.CodigoUniversitario, "UQ__Usuarios__11DC1F7385650DEE").IsUnique();

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__60695A1908993BD3").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Apellido).HasMaxLength(255);
            entity.Property(e => e.Carrera).HasMaxLength(255);
            entity.Property(e => e.CodigoUniversitario).HasMaxLength(50);
            entity.Property(e => e.Correo).HasMaxLength(255);
            entity.Property(e => e.Facultad).HasMaxLength(255);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(255);
            entity.Property(e => e.Rol).HasMaxLength(50);
            entity.Property(e => e.UltimaConexion).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
