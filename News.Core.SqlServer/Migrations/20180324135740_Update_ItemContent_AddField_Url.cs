using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace News.Core.SqlServer.Migrations
{
    public partial class Update_ItemContent_AddField_Url : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SourceUrl",
                table: "ItemContents",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceUrl",
                table: "ItemContents");
        }
    }
}
