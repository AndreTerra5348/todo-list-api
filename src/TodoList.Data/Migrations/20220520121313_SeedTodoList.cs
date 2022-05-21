using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Data.Migrations
{
    public partial class SeedTodoList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("INSERT INTO Users (Name) Values ('Mark')");

            migrationBuilder
               .Sql("INSERT INTO Todos (Title, UserId) Values ('Walk the dog', (SELECT Id FROM Users WHERE Name = 'Mark'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("DELETE FROM Todos");

            migrationBuilder
                .Sql("DELETE FROM Users");
        }
    }
}
