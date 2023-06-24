﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace UkranianCulture.Backend.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20230108124549_userIdToGuid")]
    partial class userIdToGuid
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Models.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Articles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5eca5808-4f44-4c4c-b481-72d2bdf24203"),
                            CategoryId = new Guid("858feff1-770f-4090-922a-a8dd9b16e0ee"),
                            Date = new DateTime(1886, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Region = "hmelnytsk",
                            Type = "file"
                        },
                        new
                        {
                            Id = new Guid("5b32effd-2636-4cab-8ac9-3258c746aa53"),
                            CategoryId = new Guid("858feff1-770f-4090-922a-a8dd9b16e0ee"),
                            Date = new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Region = "Kyiv",
                            Type = "file"
                        });
                });

            modelBuilder.Entity("Entities.Models.ArticlesLocale", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CultureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "CultureId");

                    b.HasIndex("CultureId");

                    b.ToTable("ArticlesLocales");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e847e218-1be2-40c2-9d44-d4c93bbf493b"),
                            CultureId = new Guid("4fd5d8c1-f34b-4824-b252-69910285e681"),
                            Content = "About Bohdan Khmelnytsky .... ",
                            ShortDescription = "About Bohdan Khmelnytsky",
                            SubText = "About Bohdan Khmelnytsky",
                            Title = "About Bohdan Khmelnytsky"
                        },
                        new
                        {
                            Id = new Guid("e847e218-1be2-40c2-9d44-d4c93bbf493b"),
                            CultureId = new Guid("0a315a0f-4860-4302-bb79-dec86e87d378"),
                            Content = "Про Богдана Хмельницького .... ",
                            ShortDescription = "Про Богдана Хмельницького",
                            SubText = "Про Богдана Хмельницького",
                            Title = "Про Богдана Хмельницького"
                        },
                        new
                        {
                            Id = new Guid("0a2e4bf1-ce88-4008-8e7b-ad6855572a6d"),
                            CultureId = new Guid("4fd5d8c1-f34b-4824-b252-69910285e681"),
                            Content = "About Ivan Mazepa .... ",
                            ShortDescription = "About Ivan Mazepa",
                            SubText = "About Ivan Mazepa",
                            Title = "About Ivan Mazepa"
                        },
                        new
                        {
                            Id = new Guid("0a2e4bf1-ce88-4008-8e7b-ad6855572a6d"),
                            CultureId = new Guid("0a315a0f-4860-4302-bb79-dec86e87d378"),
                            Content = "Про Івана Мазепу .... ",
                            ShortDescription = "Про Івана Мазепу",
                            SubText = "Про Івана Мазепу",
                            Title = "Про Івана Мазепу"
                        });
                });

            modelBuilder.Entity("Entities.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("858feff1-770f-4090-922a-a8dd9b16e0ee")
                        },
                        new
                        {
                            Id = new Guid("0e5809cd-d66e-4b1d-ac25-27a36750ebbd")
                        });
                });

            modelBuilder.Entity("Entities.Models.CategoryLocale", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CultureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId", "CultureId");

                    b.HasIndex("CultureId");

                    b.ToTable("CategoryLocales");

                    b.HasData(
                        new
                        {
                            CategoryId = new Guid("858feff1-770f-4090-922a-a8dd9b16e0ee"),
                            CultureId = new Guid("4fd5d8c1-f34b-4824-b252-69910285e681"),
                            Name = "People"
                        },
                        new
                        {
                            CategoryId = new Guid("858feff1-770f-4090-922a-a8dd9b16e0ee"),
                            CultureId = new Guid("0a315a0f-4860-4302-bb79-dec86e87d378"),
                            Name = "Люди"
                        },
                        new
                        {
                            CategoryId = new Guid("0e5809cd-d66e-4b1d-ac25-27a36750ebbd"),
                            CultureId = new Guid("4fd5d8c1-f34b-4824-b252-69910285e681"),
                            Name = "Food"
                        },
                        new
                        {
                            CategoryId = new Guid("0e5809cd-d66e-4b1d-ac25-27a36750ebbd"),
                            CultureId = new Guid("0a315a0f-4860-4302-bb79-dec86e87d378"),
                            Name = "Їжа"
                        });
                });

            modelBuilder.Entity("Entities.Models.Culture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayedName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cultures");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4fd5d8c1-f34b-4824-b252-69910285e681"),
                            DisplayedName = "English",
                            Name = "en"
                        },
                        new
                        {
                            Id = new Guid("0a315a0f-4860-4302-bb79-dec86e87d378"),
                            DisplayedName = "Ukrainian",
                            Name = "ua"
                        });
                });

            modelBuilder.Entity("Entities.Models.Article", b =>
                {
                    b.HasOne("Entities.Models.Category", "Category")
                        .WithMany("Articles")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Entities.Models.ArticlesLocale", b =>
                {
                    b.HasOne("Entities.Models.Culture", "Culture")
                        .WithMany("ArticlesTranslates")
                        .HasForeignKey("CultureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Culture");
                });

            modelBuilder.Entity("Entities.Models.CategoryLocale", b =>
                {
                    b.HasOne("Entities.Models.Culture", "Culture")
                        .WithMany("Categories")
                        .HasForeignKey("CultureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Culture");
                });

            modelBuilder.Entity("Entities.Models.Category", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("Entities.Models.Culture", b =>
                {
                    b.Navigation("ArticlesTranslates");

                    b.Navigation("Categories");
                });
#pragma warning restore 612, 618
        }
    }
}
