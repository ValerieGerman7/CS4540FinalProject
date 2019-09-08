using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CS4540PS2.Models
{
    public partial class LearningOutcomeDBContext : DbContext
    {
        public LearningOutcomeDBContext()
        {
        }

        public LearningOutcomeDBContext(DbContextOptions<LearningOutcomeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CourseInstance> CourseInstance { get; set; }
        public virtual DbSet<EvaluationMetrics> EvaluationMetrics { get; set; }
        public virtual DbSet<LearningOutcomes> LearningOutcomes { get; set; }
        public virtual DbSet<SampleFiles> SampleFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LearningOutcomeDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<CourseInstance>(entity =>
            {
                entity.HasIndex(e => new { e.Department, e.Number, e.Semester, e.Year })
                    .HasName("UQ__CourseIn__EA334D099E6B4C3E")
                    .IsUnique();

                entity.Property(e => e.CourseInstanceId).HasColumnName("CourseInstanceID");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Semester)
                    .IsRequired()
                    .HasMaxLength(6);
            });

            modelBuilder.Entity<EvaluationMetrics>(entity =>
            {
                entity.HasKey(e => e.Emid)
                    .HasName("PK__Evaluati__258EC8E008589A09");

                entity.Property(e => e.Emid).HasColumnName("EMID");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Loid).HasColumnName("LOID");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Lo)
                    .WithMany(p => p.EvaluationMetrics)
                    .HasForeignKey(d => d.Loid)
                    .HasConstraintName("FK__Evaluation__LOID__2F10007B");
            });

            modelBuilder.Entity<LearningOutcomes>(entity =>
            {
                entity.HasKey(e => e.Loid)
                    .HasName("PK__Learning__76F319DDB98F0B5B");

                entity.Property(e => e.Loid).HasColumnName("LOID");

                entity.Property(e => e.CourseInstanceId).HasColumnName("CourseInstanceID");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.CourseInstance)
                    .WithMany(p => p.LearningOutcomes)
                    .HasForeignKey(d => d.CourseInstanceId)
                    .HasConstraintName("FK__LearningO__Cours__2C3393D0");
            });

            modelBuilder.Entity<SampleFiles>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("PK__SampleFi__CA195970E10633C4");

                entity.Property(e => e.Sid).HasColumnName("SID");

                entity.Property(e => e.Emid).HasColumnName("EMID");

                entity.HasOne(d => d.Em)
                    .WithMany(p => p.SampleFiles)
                    .HasForeignKey(d => d.Emid)
                    .HasConstraintName("FK__SampleFile__EMID__31EC6D26");
            });
        }
    }
}
