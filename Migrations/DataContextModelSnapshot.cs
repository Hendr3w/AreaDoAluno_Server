﻿// <auto-generated />
using System;
using AreaDoAluno.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AreaDoAluno_Server.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("AreaDoAluno.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cep")
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<string>("Number")
                        .HasColumnType("longtext");

                    b.Property<string>("State")
                        .HasColumnType("longtext");

                    b.Property<string>("Street")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("AreaDoAluno.Models.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisciplineId")
                        .HasColumnType("int");

                    b.Property<int>("ProfessorId")
                        .HasColumnType("int");

                    b.Property<string>("Room")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("ProfessorId");

                    b.ToTable("Class");
                });

            modelBuilder.Entity("AreaDoAluno.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Field")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("AreaDoAluno.Models.Discipline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("Discipline");
                });

            modelBuilder.Entity("AreaDoAluno.Models.Enrollment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ComplementaryHours")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<decimal>("DiscountRate")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateOnly>("EnrollmentDate")
                        .HasColumnType("date");

                    b.Property<string>("EnrollmentStatus")
                        .HasColumnType("longtext");

                    b.Property<int>("Period")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("AreaDoAluno.Models.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("Deadline")
                        .HasColumnType("date");

                    b.Property<float>("MaxGrade")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.ToTable("Exam");
                });

            modelBuilder.Entity("AreaDoAluno.Models.Materials", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("AreaDoAluno.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .HasColumnType("longtext");

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("AreaDoAluno.Models.StudentClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AttendanceRate")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<decimal>("Grade")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentClass");
                });

            modelBuilder.Entity("AreaDoAluno.Models.StudentExam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<decimal>("Grade")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentExam");
                });

            modelBuilder.Entity("AreaDoAluno.Models.Tuition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("EnrollmentId")
                        .HasColumnType("int");

                    b.Property<string>("MonthRef")
                        .HasColumnType("longtext");

                    b.Property<string>("SelfStatus")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EnrollmentId");

                    b.ToTable("Tuition");
                });

            modelBuilder.Entity("AreaDoAluno.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("Birthdate")
                        .HasColumnType("date");

                    b.Property<string>("Cpf")
                        .HasColumnType("longtext");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("varchar(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("Rg")
                        .HasColumnType("longtext");

                    b.Property<string>("Rgm")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("AreaDoAluno.Models.Professor", b =>
                {
                    b.HasBaseType("AreaDoAluno.Models.User");

                    b.Property<decimal>("HourlyRate")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("HoursWorked")
                        .HasColumnType("decimal(65,30)");

                    b.HasDiscriminator().HasValue("Professor");
                });

            modelBuilder.Entity("AreaDoAluno.Models.Student", b =>
                {
                    b.HasBaseType("AreaDoAluno.Models.User");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("AreaDoAluno.Models.Class", b =>
                {
                    b.HasOne("AreaDoAluno.Models.Discipline", "Discipline")
                        .WithMany()
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AreaDoAluno.Models.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discipline");

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("AreaDoAluno.Models.Enrollment", b =>
                {
                    b.HasOne("AreaDoAluno.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AreaDoAluno.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("AreaDoAluno.Models.Exam", b =>
                {
                    b.HasOne("AreaDoAluno.Models.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("AreaDoAluno.Models.Materials", b =>
                {
                    b.HasOne("AreaDoAluno.Models.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("AreaDoAluno.Models.Message", b =>
                {
                    b.HasOne("AreaDoAluno.Models.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("AreaDoAluno.Models.StudentClass", b =>
                {
                    b.HasOne("AreaDoAluno.Models.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AreaDoAluno.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("AreaDoAluno.Models.StudentExam", b =>
                {
                    b.HasOne("AreaDoAluno.Models.Exam", "Exam")
                        .WithMany()
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AreaDoAluno.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("AreaDoAluno.Models.Tuition", b =>
                {
                    b.HasOne("AreaDoAluno.Models.Enrollment", "Enrollment")
                        .WithMany()
                        .HasForeignKey("EnrollmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enrollment");
                });

            modelBuilder.Entity("AreaDoAluno.Models.User", b =>
                {
                    b.HasOne("AreaDoAluno.Models.Address", "Address")
                        .WithOne()
                        .HasForeignKey("AreaDoAluno.Models.User", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });
#pragma warning restore 612, 618
        }
    }
}
