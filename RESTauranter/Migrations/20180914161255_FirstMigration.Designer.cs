﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Storage.Internal;
using RESTauranter.Models;
using System;

namespace RESTauranter.Migrations
{
    [DbContext(typeof(RestauranterContext))]
    [Migration("20180914161255_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

            modelBuilder.Entity("RESTauranter.Models.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RestaurantName")
                        .IsRequired();

                    b.Property<string>("ReviewComment")
                        .IsRequired();

                    b.Property<string>("ReviewDate")
                        .IsRequired();

                    b.Property<string>("ReviewerName")
                        .IsRequired();

                    b.Property<string>("StarRating")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Review");
                });
#pragma warning restore 612, 618
        }
    }
}
