using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrafficFineSystem.Migrations
{
    /// <inheritdoc />
    public partial class ChangeApprovalHistoryActionToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActionTemp",
                table: "ApprovalHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql("""
                UPDATE ApprovalHistories
                SET ActionTemp =
                    CASE Action
                        WHEN 'Approve' THEN 1
                        WHEN 'Reject' THEN 2
                        ELSE 0
                    END
                """);

            migrationBuilder.DropColumn(
                name: "Action",
                table: "ApprovalHistories");

            migrationBuilder.RenameColumn(
                name: "ActionTemp",
                table: "ApprovalHistories",
                newName: "Action");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActionTemp",
                table: "ApprovalHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("""
                UPDATE ApprovalHistories
                SET ActionTemp =
                    CASE Action
                        WHEN 1 THEN 'Approve'
                        WHEN 2 THEN 'Reject'
                        ELSE ''
                    END
                """);

            migrationBuilder.DropColumn(
                name: "Action",
                table: "ApprovalHistories");

            migrationBuilder.RenameColumn(
                name: "ActionTemp",
                table: "ApprovalHistories",
                newName: "Action");
        }
    }
}