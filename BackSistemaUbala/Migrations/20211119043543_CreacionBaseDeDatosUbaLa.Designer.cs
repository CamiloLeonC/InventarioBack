﻿// <auto-generated />
using System;
using BackSistemaUbala;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackSistemaUbala.Migrations
{
    [DbContext(typeof(AplicationDbContext))]
    [Migration("20211119043543_CreacionBaseDeDatosUbaLa")]
    partial class CreacionBaseDeDatosUbaLa
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BackSistemaUbala.Models.AlumnoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Documento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdGrupo")
                        .HasColumnType("int");

                    b.Property<int>("Jornada")
                        .HasColumnType("int");

                    b.Property<string>("NombreAcudiente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreCompleto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroAcudiente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoSangre")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdGrupo");

                    b.ToTable("Alumnos");
                });

            modelBuilder.Entity("BackSistemaUbala.Models.GrupoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Curso")
                        .HasColumnType("int");

                    b.Property<int>("Grado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Grupos");
                });

            modelBuilder.Entity("BackSistemaUbala.Models.MateriaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Materias");
                });

            modelBuilder.Entity("BackSistemaUbala.Models.MateriaProfesorModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdMateria")
                        .HasColumnType("int");

                    b.Property<int>("IdProfesor")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdMateria");

                    b.HasIndex("IdProfesor");

                    b.ToTable("MateriaProfesores");
                });

            modelBuilder.Entity("BackSistemaUbala.Models.NotaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("DefinitivaTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdAlumno")
                        .HasColumnType("int");

                    b.Property<int>("IdMateriaProfesor")
                        .HasColumnType("int");

                    b.Property<decimal>("NotaHacer")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("NotaSaber")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("NotaSer")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Observaciones")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Periodo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdAlumno");

                    b.HasIndex("IdMateriaProfesor");

                    b.ToTable("Notas");
                });

            modelBuilder.Entity("BackSistemaUbala.Models.ProfesorModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Documento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdGrupo")
                        .HasColumnType("int");

                    b.Property<string>("NombreCompleto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdGrupo");

                    b.ToTable("Profesores");
                });

            modelBuilder.Entity("BackSistemaUbala.Models.UsuarioModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("BackSistemaUbala.Models.AlumnoModel", b =>
                {
                    b.HasOne("BackSistemaUbala.Models.GrupoModel", "Grupo")
                        .WithMany("Alumnos")
                        .HasForeignKey("IdGrupo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Grupo");
                });

            modelBuilder.Entity("BackSistemaUbala.Models.MateriaProfesorModel", b =>
                {
                    b.HasOne("BackSistemaUbala.Models.MateriaModel", "Materia")
                        .WithMany("MateriaProfesores")
                        .HasForeignKey("IdMateria")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BackSistemaUbala.Models.ProfesorModel", "Profesor")
                        .WithMany("MateriasProfesor")
                        .HasForeignKey("IdProfesor")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Materia");

                    b.Navigation("Profesor");
                });

            modelBuilder.Entity("BackSistemaUbala.Models.NotaModel", b =>
                {
                    b.HasOne("BackSistemaUbala.Models.AlumnoModel", "Alumno")
                        .WithMany("NotasAlumnos")
                        .HasForeignKey("IdAlumno")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BackSistemaUbala.Models.MateriaProfesorModel", "MateriaProfesor")
                        .WithMany("Notas")
                        .HasForeignKey("IdMateriaProfesor")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Alumno");

                    b.Navigation("MateriaProfesor");
                });

            modelBuilder.Entity("BackSistemaUbala.Models.ProfesorModel", b =>
                {
                    b.HasOne("BackSistemaUbala.Models.GrupoModel", "Grupo")
                        .WithMany("Profesores")
                        .HasForeignKey("IdGrupo")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Grupo");
                });

            modelBuilder.Entity("BackSistemaUbala.Models.AlumnoModel", b =>
                {
                    b.Navigation("NotasAlumnos");
                });

            modelBuilder.Entity("BackSistemaUbala.Models.GrupoModel", b =>
                {
                    b.Navigation("Alumnos");

                    b.Navigation("Profesores");
                });

            modelBuilder.Entity("BackSistemaUbala.Models.MateriaModel", b =>
                {
                    b.Navigation("MateriaProfesores");
                });

            modelBuilder.Entity("BackSistemaUbala.Models.MateriaProfesorModel", b =>
                {
                    b.Navigation("Notas");
                });

            modelBuilder.Entity("BackSistemaUbala.Models.ProfesorModel", b =>
                {
                    b.Navigation("MateriasProfesor");
                });
#pragma warning restore 612, 618
        }
    }
}
