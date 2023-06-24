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
    [Migration("20230107213316_updateConfiguration")]
    partial class updateConfiguration
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

            modelBuilder.Entity("Entities.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

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
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "169a9df2-231c-45e8-9a0a-c7333f0dc9f4",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "a385a9fa-3f6b-4348-ba48-2bfe69bcac5f",
                            Email = "Vadym@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Vadym",
                            LastName = "Orlov",
                            LockoutEnabled = false,
                            NormalizedEmail = "VADYM@GMAIL.COM",
                            NormalizedUserName = "VADYM",
                            PasswordHash = "6925a4905d02cc4c26872e1713a0a5f2",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "3314c092-778b-42a7-9bea-c8863699d398",
                            TwoFactorEnabled = false,
                            UserName = "Vadym"
                        },
                        new
                        {
                            Id = "87d76511-8b74-4250-aef1-c47b8cb9308f",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "28bb76b1-4142-47ad-9633-2a60933830d6",
                            Email = "Bohdan@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Bohdan",
                            LastName = "Vivchar",
                            LockoutEnabled = false,
                            NormalizedEmail = "BOHDAN@GMAIL.COM",
                            NormalizedUserName = "BOHDAN",
                            PasswordHash = "6925a4905d02cc4c26872e1813a0a5f2",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "1df0335e-b26a-4c32-959b-9e4c9ba3a84e",
                            TwoFactorEnabled = false,
                            UserName = "Bohdan"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "431f29e9-13ff-4f5f-b178-511610d5103f",
                            ConcurrencyStamp = "1",
                            Name = "Admin",
                            NormalizedName = "Admin"
                        },
                        new
                        {
                            Id = "5adbec33-97c5-4041-be6a-e0f3d3ca6f44",
                            ConcurrencyStamp = "2",
                            Name = "User",
                            NormalizedName = "User"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
