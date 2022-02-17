using System;
using System.Collections.Generic;
using HappyTravel.EdoContracts.Accommodations.Internals;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyTravel.TestConnector.Data.Migrations
{
    public partial class AddBookingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    ReferenceCode = table.Column<string>(type: "text", nullable: false),
                    CheckInDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CheckOutDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    AccommodationId = table.Column<string>(type: "text", nullable: false),
                    Rooms = table.Column<List<SlimRoomOccupation>>(type: "jsonb", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.ReferenceCode);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}
