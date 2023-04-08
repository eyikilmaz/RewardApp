﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RewardApp.Api.Domain.Models;
using RewardApp.Infrastructure.Persistence.Context;

#nullable disable

namespace RewardApp.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(RewardAppContext))]
    [Migration("20230407125954_rudnamechanged")]
    partial class rudnamechanged
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RewardApp.Api.Domain.Models.Assignment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AssignmentName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("RewardId")
                        .HasColumnType("uuid");

                    b.Property<short>("State")
                        .HasColumnType("smallint");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RewardId");

                    b.HasIndex("UserId");

                    b.ToTable("assignment", "dbo");
                });

            modelBuilder.Entity("RewardApp.Api.Domain.Models.EmailConfirmation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("NewEmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OldEmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("emailconfirmation", "dbo");
                });

            modelBuilder.Entity("RewardApp.Api.Domain.Models.Reward", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("boolean");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<short>("Mod")
                        .HasColumnType("smallint");

                    b.Property<short>("Repeat")
                        .HasColumnType("smallint");

                    b.Property<Guid>("RewardKey")
                        .HasColumnType("uuid");

                    b.Property<string>("RewardName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("reward", "dbo");
                });

            modelBuilder.Entity("RewardApp.Api.Domain.Models.RewardUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("RewardId")
                        .HasColumnType("uuid");

                    b.Property<List<RewardUserDetail>>("RewardUserDetails")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RewardId");

                    b.HasIndex("UserId");

                    b.ToTable("rewarduser", "dbo");
                });

            modelBuilder.Entity("RewardApp.Api.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("user", "dbo");
                });

            modelBuilder.Entity("RewardApp.Api.Domain.Models.Winner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("RewardId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("winner", "dbo");
                });

            modelBuilder.Entity("RewardApp.Api.Domain.Models.Assignment", b =>
                {
                    b.HasOne("RewardApp.Api.Domain.Models.Reward", "Reward")
                        .WithMany()
                        .HasForeignKey("RewardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RewardApp.Api.Domain.Models.User", "User")
                        .WithMany("Assignments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Reward");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RewardApp.Api.Domain.Models.Reward", b =>
                {
                    b.HasOne("RewardApp.Api.Domain.Models.User", "CreatedBy")
                        .WithMany("Rewards")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("RewardApp.Api.Domain.Models.RewardUser", b =>
                {
                    b.HasOne("RewardApp.Api.Domain.Models.Reward", null)
                        .WithMany("RewardUsers")
                        .HasForeignKey("RewardId");

                    b.HasOne("RewardApp.Api.Domain.Models.User", "User")
                        .WithMany("RewardUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RewardApp.Api.Domain.Models.Reward", b =>
                {
                    b.Navigation("RewardUsers");
                });

            modelBuilder.Entity("RewardApp.Api.Domain.Models.User", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("RewardUsers");

                    b.Navigation("Rewards");
                });
#pragma warning restore 612, 618
        }
    }
}
