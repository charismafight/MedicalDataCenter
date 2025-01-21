using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDC.Migrations
{
    /// <inheritdoc />
    public partial class remove_recursive_referece : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Patients_PatientId",
                table: "Tests");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Tests",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Patients_PatientId",
                table: "Tests",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Patients_PatientId",
                table: "Tests");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Tests",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Patients_PatientId",
                table: "Tests",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
