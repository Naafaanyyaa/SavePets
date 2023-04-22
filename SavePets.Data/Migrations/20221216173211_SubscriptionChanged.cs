using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavePets.Data.Migrations
{
    public partial class SubscriptionChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDateOfPayment",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "StartDateOfPayment",
                table: "Subscriptions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateOfPayment",
                table: "Subscriptions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateOfPayment",
                table: "Subscriptions",
                type: "datetime2",
                nullable: true);
        }
    }
}
