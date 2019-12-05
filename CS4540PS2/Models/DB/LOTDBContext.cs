using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CS4540PS2.Models.DB;

namespace CS4540PS2.Models
{
    public partial class LOTDBContext : DbContext
    {
        public LOTDBContext()
        {
        }

        public LOTDBContext(DbContextOptions<LOTDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CourseInstance> CourseInstance { get; set; }
        public virtual DbSet<CourseNotes> CourseNotes { get; set; }
        public virtual DbSet<CourseStatus> CourseStatus { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<EvaluationMetrics> EvaluationMetrics { get; set; }
        public virtual DbSet<Instructors> Instructors { get; set; }
        public virtual DbSet<LearningOutcomes> LearningOutcomes { get; set; }
        public virtual DbSet<LONotes> LONotes { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<SampleFiles> SampleFiles { get; set; }
        public virtual DbSet<UserLocator> UserLocator { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LOTDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<CourseInstance>(entity =>
            {
                entity.HasIndex(e => new { e.Department, e.Number, e.Semester, e.Year })
                    .HasName("UQ__CourseIn__EA334D090513E077")
                    .IsUnique();

                entity.Property(e => e.CourseInstanceId).HasColumnName("CourseInstanceID");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.DueDate).HasColumnType("date");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Semester)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.DepartmentNavigation)
                    .WithMany(p => p.CourseInstance)
                    .HasForeignKey(d => d.Department)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CourseIns__Depar__2B3F6F97");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.CourseInstance)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__CourseIns__Statu__2A4B4B5E");
            });

            modelBuilder.Entity<CourseNotes>(entity =>
            {
                entity.HasKey(e => e.NoteId)
                    .HasName("PK__CourseNo__EACE355FFDB79055");

                entity.Property(e => e.CourseInstanceId).HasColumnName("CourseInstanceID");

                entity.Property(e => e.NoteModified).HasColumnType("date");

                entity.HasOne(d => d.CourseInstance)
                    .WithMany(p => p.CourseNotes)
                    .HasForeignKey(d => d.CourseInstanceId)
                    .HasConstraintName("FK__CourseNot__Cours__3B75D760");
            });

            modelBuilder.Entity<CourseStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__CourseSt__C8EE204329C5561A");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Status).HasMaxLength(200);
            });

            modelBuilder.Entity<Departments>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Departme__A25C5AA69D8CA09E");

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(400);
            });

            modelBuilder.Entity<EvaluationMetrics>(entity =>
            {
                entity.HasKey(e => e.Emid)
                    .HasName("PK__Evaluati__258EC8E09224BB94");

                entity.Property(e => e.Emid).HasColumnName("EMID");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Loid).HasColumnName("LOID");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Lo)
                    .WithMany(p => p.EvaluationMetrics)
                    .HasForeignKey(d => d.Loid)
                    .HasConstraintName("FK__Evaluation__LOID__30F848ED");
            });

            modelBuilder.Entity<Instructors>(entity =>
            {
                entity.HasKey(e => e.Ikey)
                    .HasName("PK__Instruct__8D7A08C617B65975");

                entity.HasIndex(e => new { e.CourseInstanceId, e.UserId })
                    .HasName("UQ__Instruct__E0CD20A3F46E21E2")
                    .IsUnique();

                entity.Property(e => e.Ikey).HasColumnName("IKey");

                entity.Property(e => e.CourseInstanceId).HasColumnName("CourseInstanceID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.CourseInstance)
                    .WithMany(p => p.Instructors)
                    .HasForeignKey(d => d.CourseInstanceId)
                    .HasConstraintName("FK__Instructo__Cours__37A5467C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Instructors)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Instructo__UserI__38996AB5");
            });

            modelBuilder.Entity<LearningOutcomes>(entity =>
            {
                entity.HasKey(e => e.Loid)
                    .HasName("PK__Learning__76F319DDE8C12190");

                entity.Property(e => e.Loid).HasColumnName("LOID");

                entity.Property(e => e.CourseInstanceId).HasColumnName("CourseInstanceID");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.CourseInstance)
                    .WithMany(p => p.LearningOutcomes)
                    .HasForeignKey(d => d.CourseInstanceId)
                    .HasConstraintName("FK__LearningO__Cours__2E1BDC42");
            });

            modelBuilder.Entity<LONotes>(entity =>
            {
                entity.HasKey(e => e.NoteId)
                    .HasName("PK__LONotes__EACE355F806B76E7");

                entity.ToTable("LONotes");

                entity.Property(e => e.Loid).HasColumnName("LOID");

                entity.Property(e => e.NoteModified).HasColumnType("date");

                entity.Property(e => e.NoteUserModified).HasMaxLength(450);

                entity.HasOne(d => d.Lo)
                    .WithMany(p => p.LONotes)
                    .HasForeignKey(d => d.Loid)
                    .HasConstraintName("FK__LONotes__LOID__3E52440B");
            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.ReceiverNavigation)
                    .WithMany(p => p.MessagesReceiverNavigation)
                    .HasForeignKey(d => d.Receiver)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Messages__Receiv__44FF419A");

                entity.HasOne(d => d.SenderNavigation)
                    .WithMany(p => p.MessagesSenderNavigation)
                    .HasForeignKey(d => d.Sender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Messages__Sender__440B1D61");
            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.HasKey(e => e.NotificationId)
                    .HasName("PK__Notifica__1788CCAC810E6D4F");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DateNotified).HasColumnType("date");

                entity.Property(e => e.Text).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Notificat__UserI__412EB0B6");
            });

            modelBuilder.Entity<SampleFiles>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("PK__SampleFi__CA19597019002ED2");

                entity.Property(e => e.Sid).HasColumnName("SID");

                entity.Property(e => e.Emid).HasColumnName("EMID");

                entity.HasOne(d => d.Em)
                    .WithMany(p => p.SampleFiles)
                    .HasForeignKey(d => d.Emid)
                    .HasConstraintName("FK__SampleFile__EMID__33D4B598");
            });

            modelBuilder.Entity<UserLocator>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UserLoginEmail)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.UserTitle).HasMaxLength(50);
            });
        }
    }
}
