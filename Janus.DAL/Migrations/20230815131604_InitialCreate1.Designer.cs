﻿// <auto-generated />
using System;
using Janus.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Janus.DAL.Migrations
{
    [DbContext(typeof(JanusDbContext))]
    [Migration("20230815131604_InitialCreate1")]
    partial class InitialCreate1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("Janus.Domain.Entites.AdSlot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("ScreenId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Slot1")
                        .HasColumnType("TEXT");

                    b.Property<string>("Slot2")
                        .HasColumnType("TEXT");

                    b.Property<string>("Slot3")
                        .HasColumnType("TEXT");

                    b.Property<string>("Slot4")
                        .HasColumnType("TEXT");

                    b.Property<string>("Slot5")
                        .HasColumnType("TEXT");

                    b.Property<string>("Slot6")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ScreenId");

                    b.ToTable("AdSlots");
                });

            modelBuilder.Entity("Janus.Domain.Entites.Screen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ConnectionId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ScreenAppId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Screens");
                });

            modelBuilder.Entity("Janus.Domain.Entites.AdSlot", b =>
                {
                    b.HasOne("Janus.Domain.Entites.Screen", "Screen")
                        .WithMany()
                        .HasForeignKey("ScreenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Screen");
                });
#pragma warning restore 612, 618
        }
    }
}