﻿// <auto-generated />
using System;
using LudoGameApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LudoGameApi.Migrations
{
    [DbContext(typeof(LudoGameContext))]
    partial class LudoGameContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LudoGameApi.Models.GamePiece", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Color")
                        .HasColumnType("int");

                    b.Property<bool>("InGoal")
                        .HasColumnType("bit");

                    b.Property<double>("LeftPosition")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("PositionOnBoard")
                        .HasColumnType("int");

                    b.Property<double>("TopPosition")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Pieces");
                });

            modelBuilder.Entity("LudoGameApi.Models.GameSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GameSession");
                });

            modelBuilder.Entity("LudoGameApi.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Color")
                        .HasColumnType("int");

                    b.Property<int?>("GameSessionId")
                        .HasColumnType("int");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameSessionId");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("LudoGameApi.Models.GamePiece", b =>
                {
                    b.HasOne("LudoGameApi.Models.Player", null)
                        .WithMany("GamePiece")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("LudoGameApi.Models.Player", b =>
                {
                    b.HasOne("LudoGameApi.Models.GameSession", null)
                        .WithMany("Player")
                        .HasForeignKey("GameSessionId");
                });

            modelBuilder.Entity("LudoGameApi.Models.GameSession", b =>
                {
                    b.Navigation("Player");
                });

            modelBuilder.Entity("LudoGameApi.Models.Player", b =>
                {
                    b.Navigation("GamePiece");
                });
#pragma warning restore 612, 618
        }
    }
}
