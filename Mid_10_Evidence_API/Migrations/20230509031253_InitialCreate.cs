using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mid_10_Evidence_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    CandidateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppliedFor = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ExpectedSalary = table.Column<decimal>(type: "money", nullable: false),
                    WorkFromHome = table.Column<bool>(type: "bit", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.CandidateId);
                });

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    QualificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PassingYear = table.Column<int>(type: "int", nullable: false),
                    Institute = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Result = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CandidateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.QualificationId);
                    table.ForeignKey(
                        name: "FK_Qualifications_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "CandidateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "CandidateId", "AppliedFor", "BirthDate", "CandidateName", "ExpectedSalary", "Picture", "WorkFromHome" },
                values: new object[] { 1, "Executive", new DateTime(1999, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Candidate 1", 10000.00m, "1.jpg", true });

            migrationBuilder.InsertData(
                table: "Qualifications",
                columns: new[] { "QualificationId", "CandidateId", "Degree", "Institute", "PassingYear", "Result" },
                values: new object[] { 1, 1, "HSC", "ABC", 1999, "B" });

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_CandidateId",
                table: "Qualifications",
                column: "CandidateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropTable(
                name: "Candidates");
        }
    }
}
