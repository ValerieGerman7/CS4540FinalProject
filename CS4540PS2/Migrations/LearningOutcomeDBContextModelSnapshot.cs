﻿// <auto-generated />
using System;
using CS4540PS2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CS4540PS2.Migrations
{
    [DbContext(typeof(LOTDBContext))]
    partial class LearningOutcomeDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Number");

                    b.Property<string>("Semester")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.Property<int?>("StatusId")
                        .HasColumnName("StatusID");

                    b.Property<int>("Year");

                    b.HasKey("CourseInstanceId");

                    b.HasIndex("StatusId");

                    b.HasIndex("Department", "Number", "Semester", "Year")
                        .IsUnique()
                        .HasName("UQ__CourseIn__EA334D090513E077");

                    b.ToTable("CourseInstance");
                });

            modelBuilder.Entity("CS4540PS2.Models.CourseNotes", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseInstanceId")
                        .HasColumnName("CourseInstanceID");

                    b.Property<string>("Note");

                    b.Property<DateTime?>("NoteModified")
                        .HasColumnType("date");

                    b.HasKey("NoteId")
                        .HasName("PK__CourseNo__EACE355FFDB79055");

                    b.HasIndex("CourseInstanceId");

                    b.ToTable("CourseNotes");
                });

            modelBuilder.Entity("CS4540PS2.Models.CourseStatus", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("StatusID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Status")
                        .HasMaxLength(200);

                    b.HasKey("StatusId")
                        .HasName("PK__CourseSt__C8EE204329C5561A");

                    b.ToTable("CourseStatus");
                });

            modelBuilder.Entity("CS4540PS2.Models.Departments", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(5);

                    b.Property<string>("Name")
                        .HasMaxLength(400);

                    b.HasKey("Code")
                        .HasName("PK__Departme__A25C5AA69D8CA09E");

                    b.ToTable("Departments");
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
                        .HasName("PK__Evaluati__258EC8E09224BB94");

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

                    b.Property<int>("UserId")
                        .HasColumnName("UserID");

                    b.HasKey("Ikey")
                        .HasName("PK__Instruct__8D7A08C617B65975");

                    b.HasIndex("UserId");

                    b.HasIndex("CourseInstanceId", "UserId")
                        .IsUnique()
                        .HasName("UQ__Instruct__E0CD20A3F46E21E2");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("CS4540PS2.Models.LONotes", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Loid")
                        .HasColumnName("LOID");

                    b.Property<string>("Note");

                    b.Property<DateTime?>("NoteModified")
                        .HasColumnType("date");

                    b.Property<string>("NoteUserModified")
                        .HasMaxLength(450);

                    b.HasKey("NoteId")
                        .HasName("PK__LONotes__EACE355F806B76E7");

                    b.HasIndex("Loid");

                    b.ToTable("LONotes");
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
                        .HasName("PK__Learning__76F319DDE8C12190");

                    b.HasIndex("CourseInstanceId");

                    b.ToTable("LearningOutcomes");
                });

            modelBuilder.Entity("CS4540PS2.Models.Messages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Date")
                        .HasColumnType("date");

                    b.Property<int>("Receiver");

                    b.Property<int>("Sender");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("Receiver");

                    b.HasIndex("Sender");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("CS4540PS2.Models.Notifications", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CourseInstanceId");

                    b.Property<DateTime>("DateNotified")
                        .HasColumnType("date");

                    b.Property<bool>("Read");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<int>("UserId")
                        .HasColumnName("UserID");

                    b.HasKey("NotificationId")
                        .HasName("PK__Notifica__1788CCAC810E6D4F");

                    b.HasIndex("CourseInstanceId");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("CS4540PS2.Models.SampleFiles", b =>
                {
                    b.Property<int>("Sid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentType");

                    b.Property<int>("Emid")
                        .HasColumnName("EMID");

                    b.Property<byte[]>("FileContent");

                    b.Property<string>("FileName");

                    b.Property<int>("Score");

                    b.HasKey("Sid")
                        .HasName("PK__SampleFi__CA19597019002ED2");

                    b.HasIndex("Emid");

                    b.ToTable("SampleFiles");
                });

            modelBuilder.Entity("CS4540PS2.Models.UserLocator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserLoginEmail")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.Property<string>("UserTitle")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("UserLocator");
                });

            modelBuilder.Entity("CS4540PS2.Models.CourseInstance", b =>
                {
                    b.HasOne("CS4540PS2.Models.Departments", "DepartmentNavigation")
                        .WithMany("CourseInstance")
                        .HasForeignKey("Department")
                        .HasConstraintName("FK__CourseIns__Depar__2B3F6F97");

                    b.HasOne("CS4540PS2.Models.CourseStatus", "Status")
                        .WithMany("CourseInstance")
                        .HasForeignKey("StatusId")
                        .HasConstraintName("FK__CourseIns__Statu__2A4B4B5E")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("CS4540PS2.Models.CourseNotes", b =>
                {
                    b.HasOne("CS4540PS2.Models.CourseInstance", "CourseInstance")
                        .WithMany("CourseNotes")
                        .HasForeignKey("CourseInstanceId")
                        .HasConstraintName("FK__CourseNot__Cours__3B75D760")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CS4540PS2.Models.EvaluationMetrics", b =>
                {
                    b.HasOne("CS4540PS2.Models.LearningOutcomes", "Lo")
                        .WithMany("EvaluationMetrics")
                        .HasForeignKey("Loid")
                        .HasConstraintName("FK__Evaluation__LOID__30F848ED")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CS4540PS2.Models.Instructors", b =>
                {
                    b.HasOne("CS4540PS2.Models.CourseInstance", "CourseInstance")
                        .WithMany("Instructors")
                        .HasForeignKey("CourseInstanceId")
                        .HasConstraintName("FK__Instructo__Cours__37A5467C")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CS4540PS2.Models.UserLocator", "User")
                        .WithMany("Instructors")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Instructo__UserI__38996AB5");
                });

            modelBuilder.Entity("CS4540PS2.Models.LONotes", b =>
                {
                    b.HasOne("CS4540PS2.Models.LearningOutcomes", "Lo")
                        .WithMany("LONotes")
                        .HasForeignKey("Loid")
                        .HasConstraintName("FK__LONotes__LOID__3E52440B")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CS4540PS2.Models.LearningOutcomes", b =>
                {
                    b.HasOne("CS4540PS2.Models.CourseInstance", "CourseInstance")
                        .WithMany("LearningOutcomes")
                        .HasForeignKey("CourseInstanceId")
                        .HasConstraintName("FK__LearningO__Cours__2E1BDC42")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CS4540PS2.Models.Messages", b =>
                {
                    b.HasOne("CS4540PS2.Models.UserLocator", "ReceiverNavigation")
                        .WithMany("MessagesReceiverNavigation")
                        .HasForeignKey("Receiver")
                        .HasConstraintName("FK__Messages__Receiv__44FF419A");

                    b.HasOne("CS4540PS2.Models.UserLocator", "SenderNavigation")
                        .WithMany("MessagesSenderNavigation")
                        .HasForeignKey("Sender")
                        .HasConstraintName("FK__Messages__Sender__440B1D61");
                });

            modelBuilder.Entity("CS4540PS2.Models.Notifications", b =>
                {
                    b.HasOne("CS4540PS2.Models.CourseInstance", "CourseInstance")
                        .WithMany()
                        .HasForeignKey("CourseInstanceId");

                    b.HasOne("CS4540PS2.Models.UserLocator", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Notificat__UserI__412EB0B6")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CS4540PS2.Models.SampleFiles", b =>
                {
                    b.HasOne("CS4540PS2.Models.EvaluationMetrics", "Em")
                        .WithMany("SampleFiles")
                        .HasForeignKey("Emid")
                        .HasConstraintName("FK__SampleFile__EMID__33D4B598")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
