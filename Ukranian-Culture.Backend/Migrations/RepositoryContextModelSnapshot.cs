﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace UkranianCulture.Backend.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Articles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5eca5808-4f44-4c4c-b481-72d2bdf24203"),
                            CategoryId = new Guid("858feff1-770f-4090-922a-a8dd9b16e0ee"),
                            Date = new DateTime(1886, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Region = "hmelnytsk"
                        },
                        new
                        {
                            Id = new Guid("5b32effd-2636-4cab-8ac9-3258c746aa53"),
                            CategoryId = new Guid("858feff1-770f-4090-922a-a8dd9b16e0ee"),
                            Date = new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Region = "Kyiv"
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
                            Id = new Guid("5eca5808-4f44-4c4c-b481-72d2bdf24203"),
                            CultureId = new Guid("4fd5d8c1-f34b-4824-b252-69910285e681"),
                            Content = "About Bohdan Khmelnytsky .... ",
                            ShortDescription = "About Bohdan Khmelnytsky",
                            SubText = "About Bohdan Khmelnytsky",
                            Title = "About Bohdan Khmelnytsky"
                        },
                        new
                        {
                            Id = new Guid("5eca5808-4f44-4c4c-b481-72d2bdf24203"),
                            CultureId = new Guid("0a315a0f-4860-4302-bb79-dec86e87d378"),
                            Content = "Про Богдана Хмельницького .... ",
                            ShortDescription = "Про Богдана Хмельницького",
                            SubText = "Про Богдана Хмельницького",
                            Title = "Про Богдана Хмельницького"
                        },
                        new
                        {
                            Id = new Guid("5b32effd-2636-4cab-8ac9-3258c746aa53"),
                            CultureId = new Guid("4fd5d8c1-f34b-4824-b252-69910285e681"),
                            Content = "About Ivan Mazepa .... ",
                            ShortDescription = "About Ivan Mazepa",
                            SubText = "About Ivan Mazepa",
                            Title = "About Ivan Mazepa"
                        },
                        new
                        {
                            Id = new Guid("5b32effd-2636-4cab-8ac9-3258c746aa53"),
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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("858feff1-770f-4090-922a-a8dd9b16e0ee"),
                            Name = "Music"
                        },
                        new
                        {
                            Id = new Guid("0e5809cd-d66e-4b1d-ac25-27a36750ebbd"),
                            Name = "People"
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

            modelBuilder.Entity("Entities.Models.Roles", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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
                            Id = new Guid("431f29e9-13ff-4f5f-b178-511610d5103f"),
                            ConcurrencyStamp = "1",
                            Name = "Admin",
                            NormalizedName = "Admin"
                        },
                        new
                        {
                            Id = new Guid("5adbec33-97c5-4041-be6a-e0f3d3ca6f44"),
                            ConcurrencyStamp = "2",
                            Name = "User",
                            NormalizedName = "User"
                        });
                });

            modelBuilder.Entity("Entities.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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
                            Id = new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "9d5aa0ba-e298-47c6-82e6-8579d8eef825",
                            Email = "Admin@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEI45gWwwEJ4s6ogTf1c/m3Ky42oEuJeVvUB+Yp3hJuK74ASOmew9d3qBq4qA53P5UA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "76004976-4518-4c9f-b6db-3537f05ceb7a",
                            TwoFactorEnabled = false,
                            UserName = "Admin"
                        },
                        new
                        {
                            Id = new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "aacf9056-4351-46ef-bf55-1cc4595af262",
                            Email = "Bohdan@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "BOHDAN@GMAIL.COM",
                            NormalizedUserName = "BOHDAN",
                            PasswordHash = "AQAAAAEAACcQAAAAEIFpwcL9s/pQ3v8Jkag0C536aqgEhtJ92oYb4869lKb581QvZFrJVbjKJhLTR4nUrA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "54512474-5c70-449b-8e3c-eaeb6b40cfde",
                            TwoFactorEnabled = false,
                            UserName = "Bohdan"
                        });
                });

            modelBuilder.Entity("Entities.Models.UserHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfWatch")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UsersHistories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c5a0e131-46a0-4f37-9a9d-6e426cb94f46"),
                            DateOfWatch = new DateTime(2023, 1, 18, 1, 1, 1, 1, DateTimeKind.Utc),
                            Title = "About Bohdan Khmelnytsky",
                            UserId = new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4")
                        },
                        new
                        {
                            Id = new Guid("9d2abe54-d8fb-45eb-94a0-65cefcbfa432"),
                            DateOfWatch = new DateTime(2023, 1, 18, 1, 3, 1, 0, DateTimeKind.Utc),
                            Title = "About Ivan Mazepa",
                            UserId = new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4"),
                            RoleId = new Guid("431f29e9-13ff-4f5f-b178-511610d5103f")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

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

            modelBuilder.Entity("Entities.Models.UserHistory", b =>
                {
                    b.HasOne("Entities.Models.User", "User")
                        .WithMany("History")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Entities.Models.Roles", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Entities.Models.Roles", null)
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
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
                });

            modelBuilder.Entity("Entities.Models.User", b =>
                {
                    b.Navigation("History");
                });
#pragma warning restore 612, 618
        }
    }
}
