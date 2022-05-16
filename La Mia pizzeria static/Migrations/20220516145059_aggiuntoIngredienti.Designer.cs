﻿// <auto-generated />
using La_Mia_pizzeria_static.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace La_Mia_pizzeria_static.Migrations
{
    [DbContext(typeof(MenùContext))]
    [Migration("20220516145059_aggiuntoIngredienti")]
    partial class aggiuntoIngredienti
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("IngredientePizza", b =>
                {
                    b.Property<int>("IngredientiId")
                        .HasColumnType("int");

                    b.Property<int>("ListaPizzeId")
                        .HasColumnType("int");

                    b.HasKey("IngredientiId", "ListaPizzeId");

                    b.HasIndex("ListaPizzeId");

                    b.ToTable("IngredientePizza");
                });

            modelBuilder.Entity("La_Mia_pizzeria_static.Models.Ingrediente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ingrediente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ingrediente");
                });

            modelBuilder.Entity("La_Mia_pizzeria_static.Models.Pizza", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<double>("Prezzo")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Pizze");
                });

            modelBuilder.Entity("IngredientePizza", b =>
                {
                    b.HasOne("La_Mia_pizzeria_static.Models.Ingrediente", null)
                        .WithMany()
                        .HasForeignKey("IngredientiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("La_Mia_pizzeria_static.Models.Pizza", null)
                        .WithMany()
                        .HasForeignKey("ListaPizzeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
