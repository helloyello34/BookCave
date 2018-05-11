﻿// <auto-generated />
using BookCave.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace BookCave.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180508163840_deletedGenres")]
    partial class deletedGenres
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BookCave.Models.EntityModels.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("BookCave.Models.EntityModels.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<double>("Discount");

                    b.Property<string>("ISBN");

                    b.Property<string>("Image");

                    b.Property<string>("Info");

                    b.Property<string>("Language");

                    b.Property<int>("PageCount");

                    b.Property<double>("Price");

                    b.Property<string>("Publisher");

                    b.Property<double>("Rating");

                    b.Property<int>("RatingCount");

                    b.Property<int>("ReleaseYear");

                    b.Property<int>("Stock");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookCave.Models.EntityModels.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookId");

                    b.Property<string>("Comments");

                    b.Property<double>("Rating");

                    b.HasKey("Id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BookCave.Models.EntityModels.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });
#pragma warning restore 612, 618
        }
    }
}
