﻿// <auto-generated />
using System;
using Acorn.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Acorn.Migrations
{
    [DbContext(typeof(MySqlContext))]
    [Migration("20230214013711_Identity")]
    partial class Identity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Acorn.Domain.Entities.Post.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Acorn.Domain.Entities.Post.Vote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("PostId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("RevokedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Vote");
                });

            modelBuilder.Entity("Acorn.Domain.Entities.Tag.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("char(36)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("TagSetId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Acorn.Domain.Entities.User.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.DeviceFlowCodes", b =>
                {
                    b.Property<string>("UserCode")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("DeviceCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("UserCode");

                    b.HasIndex("DeviceCode")
                        .IsUnique();

                    b.HasIndex("Expiration");

                    b.ToTable("DeviceCodes", (string)null);
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.Key", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Algorithm")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("DataProtected")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsX509Certificate")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Use")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Use");

                    b.ToTable("Keys", (string)null);
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("ConsumedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Key");

                    b.HasIndex("ConsumedTime");

                    b.HasIndex("Expiration");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.HasIndex("SubjectId", "SessionId", "Type");

                    b.ToTable("PersistedGrants", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.Property<Guid>("PostsId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("char(36)");

                    b.HasKey("PostsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("PostTag");
                });

            modelBuilder.Entity("Acorn.Domain.Entities.Post.Vote", b =>
                {
                    b.HasOne("Acorn.Domain.Entities.Post.Post", null)
                        .WithMany("Votes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                    b.HasOne("Acorn.Domain.Entities.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Acorn.Domain.Entities.User.User", null)
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

                    b.HasOne("Acorn.Domain.Entities.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Acorn.Domain.Entities.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.HasOne("Acorn.Domain.Entities.Post.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Acorn.Domain.Entities.Tag.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Acorn.Domain.Entities.Post.Post", b =>
                {
                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}