﻿// <auto-generated />
using System;
using Chaos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Chaos.Data.Migrations
{
    [DbContext(typeof(GameDbContext))]
    [Migration("20230402151106_AddOutcomeToReportModels")]
    partial class AddOutcomeToReportModels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("Chaos.Models.DbModels.AfterActionReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AggressorAttackerLosses")
                        .HasColumnType("INTEGER");

                    b.Property<string>("AggressorId")
                        .HasColumnType("TEXT");

                    b.Property<int>("AggressorRecruitLosses")
                        .HasColumnType("INTEGER");

                    b.Property<string>("AggressorToolsLostJson")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BattleTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("DefenderCoinLosses")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DefenderDefenderLosses")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DefenderId")
                        .HasColumnType("TEXT");

                    b.Property<int>("DefenderRecruitLosses")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DefenderToolsLostJson")
                        .HasColumnType("TEXT");

                    b.Property<string>("Outcome")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AggressorId");

                    b.HasIndex("DefenderId");

                    b.ToTable("AfterActionReports");
                });

            modelBuilder.Entity("Chaos.Models.DbModels.Army", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Attackers")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CoinGenerationRate")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Coins")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Defenders")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastCoinGeneration")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastRecruitment")
                        .HasColumnType("TEXT");

                    b.Property<int>("RecruitRate")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Recruits")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Sappers")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Sentries")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserWeaponsJsonData")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Armies");
                });

            modelBuilder.Entity("Chaos.Models.DbModels.GameUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Faction")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Chaos.Models.DbModels.SpyReport", b =>
                {
                    b.Property<string>("SapperId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SentryId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Outcome")
                        .HasColumnType("TEXT");

                    b.Property<int>("SapperRecruitLosses")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SapperSapperLosses")
                        .HasColumnType("INTEGER");

                    b.Property<double>("SapperStrength")
                        .HasColumnType("REAL");

                    b.Property<string>("SapperToolsLostJson")
                        .HasColumnType("TEXT");

                    b.Property<double>("SentryMaximumDefence")
                        .HasColumnType("REAL");

                    b.Property<double>("SentryMinimumDefence")
                        .HasColumnType("REAL");

                    b.Property<int>("SentryRecruitLosses")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SentrySentryLosses")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SentryToolsLostJson")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SpyTime")
                        .HasColumnType("TEXT");

                    b.HasKey("SapperId", "SentryId");

                    b.ToTable("SpyReports");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

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
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Chaos.Models.DbModels.AfterActionReport", b =>
                {
                    b.HasOne("Chaos.Models.DbModels.GameUser", null)
                        .WithMany()
                        .HasForeignKey("AggressorId");

                    b.HasOne("Chaos.Models.DbModels.GameUser", null)
                        .WithMany()
                        .HasForeignKey("DefenderId");
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
                    b.HasOne("Chaos.Models.DbModels.GameUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Chaos.Models.DbModels.GameUser", null)
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

                    b.HasOne("Chaos.Models.DbModels.GameUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Chaos.Models.DbModels.GameUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
