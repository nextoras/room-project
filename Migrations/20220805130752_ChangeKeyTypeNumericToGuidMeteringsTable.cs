using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace server.Migrations
{
    public partial class ChangeKeyTypeNumericToGuidMeteringsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey("PK_Meterings","Meterings");
            migrationBuilder.DropColumn("Id","Meterings");
            migrationBuilder.AddColumn<Guid>("Id", "Meterings");
            migrationBuilder.AddPrimaryKey("PK_Meterings","Meterings","Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey("PK_Meterings","Meterings");
            migrationBuilder.DropColumn("Id","Meterings");
            migrationBuilder.AddColumn<int>("Id", "Meterings");
            migrationBuilder.AddPrimaryKey("PK_Meterings","Meterings","Id");
        }
    }
}
