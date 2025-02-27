﻿// <auto-generated />
using BookManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookManagementAPI.Migrations
{
    [DbContext(typeof(BookDbContext))]
    [Migration("20250220225501_AddConstants")]
    partial class AddConstants
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookManagementAPI.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("BookViews")
                        .HasColumnType("int");

                    b.Property<int>("PublicationYear")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Robert Kyosaky",
                            BookViews = 0,
                            PublicationYear = 1992,
                            Title = "RichDad PoorDad"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Unknown",
                            BookViews = 0,
                            PublicationYear = 2000,
                            Title = "Club 5 in the morening"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Some Magician",
                            BookViews = 0,
                            PublicationYear = 2002,
                            Title = "Harry Potter"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
