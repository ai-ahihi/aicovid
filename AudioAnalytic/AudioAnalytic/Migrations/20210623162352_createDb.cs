using Microsoft.EntityFrameworkCore.Migrations;

namespace AudioAnalytic.Migrations
{
    public partial class createDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PublicTest",
                columns: table => new
                {
                    Uuid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgeRaw = table.Column<float>(type: "real", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: true),
                    FileRaw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicTest", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "PublicTrain",
                columns: table => new
                {
                    Uuid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgeRaw = table.Column<float>(type: "real", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: true),
                    FileRaw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicTrain", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "TestFeature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileRaw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileSpec = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Feature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<long>(type: "bigint", nullable: false),
                    RootFile = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestFeature_PublicTest_RootFile",
                        column: x => x.RootFile,
                        principalTable: "PublicTest",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainFeature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileRaw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileSpec = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Feature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<long>(type: "bigint", nullable: false),
                    RootFile = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainFeature_PublicTrain_RootFile",
                        column: x => x.RootFile,
                        principalTable: "PublicTrain",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestFeature_RootFile",
                table: "TestFeature",
                column: "RootFile");

            migrationBuilder.CreateIndex(
                name: "IX_TrainFeature_RootFile",
                table: "TrainFeature",
                column: "RootFile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestFeature");

            migrationBuilder.DropTable(
                name: "TrainFeature");

            migrationBuilder.DropTable(
                name: "PublicTest");

            migrationBuilder.DropTable(
                name: "PublicTrain");
        }
    }
}
