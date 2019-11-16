﻿// <auto-generated />
using CS4540PS2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CS4540PS2.Migrations
{
    [DbContext(typeof(LOTDBContext))]
    [Migration("20190912035912_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CS4540PS2.Models.CourseInstance", b =>
                {
                    b.Property<int>("CourseInstanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CourseInstanceID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Number");

                    b.Property<string>("Semester")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.Property<int>("Year");

                    b.HasKey("CourseInstanceId");

                    b.HasIndex("Department", "Number", "Semester", "Year")
                        .IsUnique()
                        .HasName("UQ__CourseIn__EA334D099E6B4C3E");

                    b.ToTable("CourseInstance");
                });

            modelBuilder.Entity("CS4540PS2.Models.EvaluationMetrics", b =>
                {
                    b.Property<int>("Emid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("EMID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("Loid")
                        .HasColumnName("LOID");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Emid")
                        .HasName("PK__Evaluati__258EC8E008589A09");

                    b.HasIndex("Loid");

                    b.ToTable("EvaluationMetrics");
                });

            modelBuilder.Entity("CS4540PS2.Models.LearningOutcomes", b =>
                {
                    b.Property<int>("Loid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("LOID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseInstanceId")
                        .HasColumnName("CourseInstanceID");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Loid")
                        .HasName("PK__Learning__76F319DDB98F0B5B");

                    b.HasIndex("CourseInstanceId");

                    b.ToTable("LearningOutcomes");
                });

            modelBuilder.Entity("CS4540PS2.Models.SampleFiles", b =>
                {
                    b.Property<int>("Sid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Emid")
                        .HasColumnName("EMID");

                    b.Property<string>("FileName");

                    b.Property<int>("Score");

                    b.HasKey("Sid")
                        .HasName("PK__SampleFi__CA195970E10633C4");

                    b.HasIndex("Emid");

                    b.ToTable("SampleFiles");
                });

            modelBuilder.Entity("CS4540PS2.Models.EvaluationMetrics", b =>
                {
                    b.HasOne("CS4540PS2.Models.LearningOutcomes", "Lo")
                        .WithMany("EvaluationMetrics")
                        .HasForeignKey("Loid")
                        .HasConstraintName("FK__Evaluation__LOID__2F10007B")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CS4540PS2.Models.LearningOutcomes", b =>
                {
                    b.HasOne("CS4540PS2.Models.CourseInstance", "CourseInstance")
                        .WithMany("LearningOutcomes")
                        .HasForeignKey("CourseInstanceId")
                        .HasConstraintName("FK__LearningO__Cours__2C3393D0")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CS4540PS2.Models.SampleFiles", b =>
                {
                    b.HasOne("CS4540PS2.Models.EvaluationMetrics", "Em")
                        .WithMany("SampleFiles")
                        .HasForeignKey("Emid")
                        .HasConstraintName("FK__SampleFile__EMID__31EC6D26")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
