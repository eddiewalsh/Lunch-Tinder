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
    [Migration("20230627140336_updatedFieldsinEventTable")]
    partial class updatedFieldsinEventTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventLunchGroup", b =>
                {
                    b.Property<int>("EventsEventId")
                        .HasColumnType("int");

                    b.Property<int>("LunchGroupsGroupId")
                        .HasColumnType("int");

                    b.HasKey("EventsEventId", "LunchGroupsGroupId");

                    b.HasIndex("LunchGroupsGroupId");

                    b.ToTable("EventLunchGroup");
                });

            modelBuilder.Entity("EventRestaurant", b =>
                {
                    b.Property<int>("EventsEventId")
                        .HasColumnType("int");

                    b.Property<int>("RestaurantOptionsRestaurantID")
                        .HasColumnType("int");

                    b.HasKey("EventsEventId", "RestaurantOptionsRestaurantID");

                    b.HasIndex("RestaurantOptionsRestaurantID");

                    b.ToTable("EventRestaurant");
                });

            modelBuilder.Entity("LunchGroupUser", b =>
                {
                    b.Property<int>("LunchGroupsGroupId")
                        .HasColumnType("int");

                    b.Property<int>("UsersUserId")
                        .HasColumnType("int");

                    b.HasKey("LunchGroupsGroupId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("LunchGroupUser");
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

                    b.Property<DateTime>("EventEndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EventStartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("VotingEndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("VotingStartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("EventId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Lunch_Tinder.Models.InviteToLunchGroup", b =>
                {
                    b.Property<int>("InviteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InviteID"));

                    b.Property<string>("LunchGroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsernameEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InviteID");

                    b.ToTable("InvitesLG");
                });

            modelBuilder.Entity("Lunch_Tinder.Models.Invites", b =>
                {
                    b.Property<int>("InviteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InviteId"));

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InviteId");

                    b.ToTable("Invites");
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
                            Description = "We code",
                            GroupName = "Software Development"
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
                            RestaurantID = 4,
                            RestaurantDescription = "Hotel, Traditional Food",
                            RestaurantName = "McLaughlins Restaurant"
                        },
                        new
                        {
                            RestaurantID = 3,
                            RestaurantDescription = "Sandwhiches, Salads,Breakfast Foods",
                            RestaurantName = "Delish Cafe"
                        },
                        new
                        {
                            RestaurantID = 2,
                            RestaurantDescription = "Italian pizza and European dishes",
                            RestaurantName = "La Cucina Limerick"
                        },
                        new
                        {
                            RestaurantID = 1,
                            RestaurantDescription = "We don’t do burgers, we do Locke burgers.",
                            RestaurantName = "Locke Burger Castletroy"
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

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            EmailAddress = "useremail@gmail.com",
                            Password = "$2a$11$/1Z1IGaxnzJBSojSkfAhQeNf4XJIcaAmqSIjVzdfXcSjliNg8dV.a",
                            UserName = "Test",
                            UserType = "USER"
                        },
                        new
                        {
                            UserId = 2,
                            EmailAddress = "adminemail@gmail.com",
                            Password = "$2a$11$lhhYgQXADjq0cyUaNza0.udO.QKuiDeNqQopJniLIqiMSOO57sXyW",
                            UserName = "Test",
                            UserType = "ADMIN"
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

                    b.HasIndex("UserVoteID")
                        .IsUnique();

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("EventLunchGroup", b =>
                {
                    b.HasOne("Lunch_Tinder.Models.Event", null)
                        .WithMany()
                        .HasForeignKey("EventsEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lunch_Tinder.Models.LunchGroup", null)
                        .WithMany()
                        .HasForeignKey("LunchGroupsGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                        .HasForeignKey("RestaurantOptionsRestaurantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LunchGroupUser", b =>
                {
                    b.HasOne("Lunch_Tinder.Models.LunchGroup", null)
                        .WithMany()
                        .HasForeignKey("LunchGroupsGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lunch_Tinder.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lunch_Tinder.Models.Vote", b =>
                {
                    b.HasOne("Lunch_Tinder.Models.Event", "Event")
                        .WithMany("Votes")
                        .HasForeignKey("EventVoteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lunch_Tinder.Models.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantVoteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lunch_Tinder.Models.User", "User")
                        .WithOne("Vote")
                        .HasForeignKey("Lunch_Tinder.Models.Vote", "UserVoteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Restaurant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lunch_Tinder.Models.Event", b =>
                {
                    b.Navigation("Votes");
                });

            modelBuilder.Entity("Lunch_Tinder.Models.User", b =>
                {
                    b.Navigation("Vote");
                });
#pragma warning restore 612, 618
        }
    }
}
