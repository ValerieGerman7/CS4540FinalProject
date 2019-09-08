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
        public virtual DbSet<LearningOutcomes> LearningOutcomes { get; set; }

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
                    .HasName("UQ__CourseIn__EA334D09BC08C7E2")
                    .IsUnique();

                entity.Property(e => e.CourseInstanceId).HasColumnName("CourseInstanceID");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Descripton).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Semester)
                    .IsRequired()
                    .HasMaxLength(6);
            });

            modelBuilder.Entity<LearningOutcomes>(entity =>
            {
                entity.HasKey(e => e.Loid)
                    .HasName("PK__Learning__76F319DD86543E19");

                entity.Property(e => e.Loid).HasColumnName("LOID");

                entity.Property(e => e.CourseInstanceId).HasColumnName("CourseInstanceID");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.CourseInstance)
                    .WithMany(p => p.LearningOutcomes)
                    .HasForeignKey(d => d.CourseInstanceId)
                    .HasConstraintName("FK__LearningO__Cours__267ABA7A");
            });
        }
    }
}
