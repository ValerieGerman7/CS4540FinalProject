﻿// <auto-generated />
using System;
using CS4540PS2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CS4540PS2.Migrations
{
    [DbContext(typeof(LearningOutcomeDBContext))]
    [Migration("20191017231952_NotesModifications")]
    partial class NotesModifications
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

                    b.Property<string>("Note");

                    b.Property<DateTime?>("NoteModified");

                    b.Property<int>("Number");

                    b.Property<string>("Semester")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.Property<int>("Year");

                    b.HasKey("CourseInstanceId");

                    b.HasIndex("Department", "Number", "Semester", "Year")
                        .IsUnique()
                        .HasName("UQ__CourseIn__EA334D09ED3CADE0");

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
                        .HasName("PK__Evaluati__258EC8E02063D158");

                    b.HasIndex("Loid");

                    b.ToTable("EvaluationMetrics");
                });

            modelBuilder.Entity("CS4540PS2.Models.Instructors", b =>
                {
                    b.Property<int>("Ikey")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IKey")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseInstanceId")
                        .HasColumnName("CourseInstanceID");

                    b.Property<string>("InstructorLoginEmail")
                        .IsRequired();

                    b.Property<string>("InstructorTitle")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("Ikey")
                        .HasName("PK__Instruct__8D7A08C6F8CFADF9");

                    b.HasIndex("CourseInstanceId", "InstructorLoginEmail")
                        .IsUnique()
                        .HasName("UQ__Instruct__9386E1D4FC8546A5");

                    b.ToTable("Instructors");
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

                    b.Property<string>("Note");

                    b.Property<DateTime?>("NoteModified");

                    b.Property<string>("NoteUserModifed");

                    b.HasKey("Loid")
                        .HasName("PK__Learning__76F319DDBAB9F1A4");

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
                        .HasName("PK__SampleFi__CA195970D4F455B9");

                    b.HasIndex("Emid");

                    b.ToTable("SampleFiles");
                });

            modelBuilder.Entity("CS4540PS2.Models.EvaluationMetrics", b =>
                {
                    b.HasOne("CS4540PS2.Models.LearningOutcomes", "Lo")
                        .WithMany("EvaluationMetrics")
                        .HasForeignKey("Loid")
                        .HasConstraintName("FK__Evaluation__LOID__29572725")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CS4540PS2.Models.Instructors", b =>
                {
                    b.HasOne("CS4540PS2.Models.CourseInstance", "CourseInstance")
                        .WithMany("Instructors")
                        .HasForeignKey("CourseInstanceId")
                        .HasConstraintName("FK__Instructo__Cours__412EB0B6")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CS4540PS2.Models.LearningOutcomes", b =>
                {
                    b.HasOne("CS4540PS2.Models.CourseInstance", "CourseInstance")
                        .WithMany("LearningOutcomes")
                        .HasForeignKey("CourseInstanceId")
                        .HasConstraintName("FK__LearningO__Cours__267ABA7A")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CS4540PS2.Models.SampleFiles", b =>
                {
                    b.HasOne("CS4540PS2.Models.EvaluationMetrics", "Em")
                        .WithMany("SampleFiles")
                        .HasForeignKey("Emid")
                        .HasConstraintName("FK__SampleFile__EMID__2C3393D0")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
