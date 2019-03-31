namespace ZooPlanet.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class IsAnsweredPropertyContactsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAnswered",
                table: "Contacts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAnswered",
                table: "Contacts");
        }
    }
}
