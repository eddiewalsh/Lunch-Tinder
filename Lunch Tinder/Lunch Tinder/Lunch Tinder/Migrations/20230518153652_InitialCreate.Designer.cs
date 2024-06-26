﻿// <auto-generated />
using System;
using Lunch_Tinder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lunch_Tinder.Migrations
{
    [DbContext(typeof(LunchTinderContext))]
    [Migration("20230518153652_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventRestaurant", b =>
                {
                    b.Property<int>("EventsEventId")
                        .HasColumnType("int");

                    b.Property<int>("OptionsRestaurantID")
                        .HasColumnType("int");

                    b.HasKey("EventsEventId", "OptionsRestaurantID");

                    b.HasIndex("OptionsRestaurantID");

                    b.ToTable("EventRestaurant");
                });

            modelBuilder.Entity("LunchGroupUser", b =>
                {
                    b.Property<int>("GroupsGroupId")
                        .HasColumnType("int");

                    b.Property<int>("MembersUserId")
                        .HasColumnType("int");

                    b.HasKey("GroupsGroupId", "MembersUserId");

                    b.HasIndex("MembersUserId");

                    b.ToTable("GroupMembers", (string)null);
                });

            modelBuilder.Entity("Lunch_Tinder.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("EventId");

                    b.HasIndex("GroupId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            EventId = 1,
                            Description = "Event 1 description",
                            EndTime = new DateTime(2023, 5, 19, 16, 36, 51, 868, DateTimeKind.Local).AddTicks(9586),
                            GroupId = 1,
                            Name = "Event1",
                            StartTime = new DateTime(2023, 5, 18, 16, 36, 51, 868, DateTimeKind.Local).AddTicks(9551)
                        },
                        new
                        {
                            EventId = 2,
                            Description = "Event 2 description",
                            EndTime = new DateTime(2023, 5, 19, 16, 36, 51, 868, DateTimeKind.Local).AddTicks(9593),
                            GroupId = 2,
                            Name = "Event2",
                            StartTime = new DateTime(2023, 5, 18, 16, 36, 51, 868, DateTimeKind.Local).AddTicks(9591)
                        });
                });

            modelBuilder.Entity("Lunch_Tinder.Models.LunchGroup", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GroupId");

                    b.ToTable("LunchGroups");

                    b.HasData(
                        new
                        {
                            GroupId = 1,
                            Description = "Group 1 description",
                            GroupName = "Group1"
                        },
                        new
                        {
                            GroupId = 2,
                            Description = "Group 2 description",
                            GroupName = "Group2"
                        });
                });

            modelBuilder.Entity("Lunch_Tinder.Models.Restaurant", b =>
                {
                    b.Property<int>("RestaurantID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RestaurantID"));

                    b.Property<string>("RestaurantDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RestaurantName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RestaurantID");

                    b.ToTable("Restaurants");

                    b.HasData(
                        new
                        {
                            RestaurantID = 1,
                            RestaurantDescription = "Restaurant 1 description",
                            RestaurantName = "Restaurant1"
                        },
                        new
                        {
                            RestaurantID = 2,
                            RestaurantDescription = "Restaurant 2 description",
                            RestaurantName = "Restaurant2"
                        });
                });

            modelBuilder.Entity("Lunch_Tinder.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            EmailAddress = "user1@example.com",
                            Password = "password1",
                            UserName = "user1"
                        },
                        new
                        {
                            UserId = 2,
                            EmailAddress = "user2@example.com",
                            Password = "password2",
                            UserName = "user2"
                        });
                });

            modelBuilder.Entity("Lunch_Tinder.Models.Vote", b =>
                {
                    b.Property<int>("VoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VoteId"));

                    b.Property<int>("EventVoteID")
                        .HasColumnType("int");

                    b.Property<int>("RestaurantVoteID")
                        .HasColumnType("int");

                    b.Property<int>("UserVoteID")
                        .HasColumnType("int");

                    b.HasKey("VoteId");

                    b.HasIndex("EventVoteID");

                    b.HasIndex("RestaurantVoteID");

                    b.HasIndex("UserVoteID");

                    b.ToTable("Votes");

                    b.HasData(
                        new
                        {
                            VoteId = 1,
                            EventVoteID = 1,
                            RestaurantVoteID = 1,
                            UserVoteID = 1
                        },
                        new
                        {
                            VoteId = 2,
                            EventVoteID = 2,
                            RestaurantVoteID = 2,
                            UserVoteID = 2
                        });
                });

            modelBuilder.Entity("EventRestaurant", b =>
                {
                    b.HasOne("Lunch_Tinder.Models.Event", null)
                        .WithMany()
                        .HasForeignKey("EventsEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lunch_Tinder.Models.Restaurant", null)
                        .WithMany()
                        .HasForeignKey("OptionsRestaurantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LunchGroupUser", b =>
                {
                    b.HasOne("Lunch_Tinder.Models.LunchGroup", null)
                        .WithMany()
                        .HasForeignKey("GroupsGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lunch_Tinder.Models.User", null)
                        .WithMany()
                        .HasForeignKey("MembersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lunch_Tinder.Models.Event", b =>
                {
                    b.HasOne("Lunch_Tinder.Models.LunchGroup", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Lunch_Tinder.Models.Vote", b =>
                {
                    b.HasOne("Lunch_Tinder.Models.Event", "EventVote")
                        .WithMany()
                        .HasForeignKey("EventVoteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lunch_Tinder.Models.Restaurant", "VotedRestaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantVoteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lunch_Tinder.Models.User", "UserVote")
                        .WithMany()
                        .HasForeignKey("UserVoteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventVote");

                    b.Navigation("UserVote");

                    b.Navigation("VotedRestaurant");
                });
#pragma warning restore 612, 618
        }
    }
}
