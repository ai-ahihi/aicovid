using Microsoft.EntityFrameworkCore.Migrations;

namespace AudioAnalytic.Migrations
{
    public partial class createdbTrain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AudioDetail",
                columns: table => new
                {
                    Uuid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgeRaw = table.Column<float>(type: "real", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: true),
                    FileRaw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioDetail", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "PublicTest",
                columns: table => new
                {
                    Uuid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgeRaw = table.Column<float>(type: "real", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: true),
                    FileRaw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<long>(type: "bigint", nullable: false),
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
                    Time = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicTrain", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "FileFeature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileRaw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileSpec = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Feature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<long>(type: "bigint", nullable: false),
                    RootFile = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PublicTestUuid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PublicTrainUuid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileFeature_AudioDetail_RootFile",
                        column: x => x.RootFile,
                        principalTable: "AudioDetail",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileFeature_PublicTest_PublicTestUuid",
                        column: x => x.PublicTestUuid,
                        principalTable: "PublicTest",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileFeature_PublicTrain_PublicTrainUuid",
                        column: x => x.PublicTrainUuid,
                        principalTable: "PublicTrain",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileFeature_PublicTestUuid",
                table: "FileFeature",
                column: "PublicTestUuid");

            migrationBuilder.CreateIndex(
                name: "IX_FileFeature_PublicTrainUuid",
                table: "FileFeature",
                column: "PublicTrainUuid");

            migrationBuilder.CreateIndex(
                name: "IX_FileFeature_RootFile",
                table: "FileFeature",
                column: "RootFile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileFeature");

            migrationBuilder.DropTable(
                name: "AudioDetail");

            migrationBuilder.DropTable(
                name: "PublicTest");

            migrationBuilder.DropTable(
                name: "PublicTrain");
        }
    }
}
