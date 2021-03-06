﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UContact.MyReportApi.Database;

namespace UContact.MyReportApi.Migrations
{
    [DbContext(typeof(MyReportDbContext))]
    partial class MyReportDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("UContact.MyReportApi.Database.Entities.Report", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ContactCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Location")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasDefaultValue("");

                    b.Property<int>("PhoneNumberCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(10);

                    b.HasKey("Id");

                    b.ToTable("Reports");
                });
#pragma warning restore 612, 618
        }
    }
}
