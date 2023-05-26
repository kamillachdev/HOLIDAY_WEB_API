﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HOLIDAY_WEB_API.Migrations
{
    /// <inheritdoc />
    public partial class User_table_restructurization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Role");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "Name");
        }
    }
}
