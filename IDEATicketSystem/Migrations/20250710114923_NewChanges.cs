using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IDEATicketSystem.Migrations
{
    /// <inheritdoc />
    public partial class NewChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AssignedTimeStamp",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailReceivedTimeStamped",
                table: "ReceivedEmails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "ReceivedEmails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedTimeStamp",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "EmailReceivedTimeStamped",
                table: "ReceivedEmails");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "ReceivedEmails");
        }
    }
}
