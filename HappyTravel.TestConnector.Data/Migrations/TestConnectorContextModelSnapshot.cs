﻿// <auto-generated />
using System;
using System.Collections.Generic;
using HappyTravel.EdoContracts.Accommodations.Internals;
using HappyTravel.TestConnector.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HappyTravel.TestConnector.Data.Migrations
{
    [DbContext(typeof(TestConnectorContext))]
    partial class TestConnectorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HappyTravel.TestConnector.Data.Models.Booking", b =>
                {
                    b.Property<string>("ReferenceCode")
                        .HasColumnType("text");

                    b.Property<string>("AccommodationId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CheckInDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("CheckOutDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<List<SlimRoomOccupation>>("Rooms")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("ReferenceCode");

                    b.ToTable("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
