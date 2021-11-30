using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZeroDemo.Migrations
{
    public partial class Add_Table_Box_Sensor_SensorConfig_BoxManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "App_Boxes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    BoxName = table.Column<string>(maxLength: 256, nullable: false),
                    Location = table.Column<string>(maxLength: 1028, nullable: false),
                    MaxBoxPort = table.Column<int>(nullable: false),
                    MaxBoxManagerPort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_Boxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "App_Sensors",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    SensorName = table.Column<string>(maxLength: 256, nullable: false),
                    HighValueDefault = table.Column<double>(nullable: false),
                    LowValueDefault = table.Column<double>(nullable: false),
                    TargetValueDefault = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_Sensors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "App_BoxManagers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    BoxId = table.Column<long>(nullable: false),
                    ManagerName = table.Column<string>(maxLength: 256, nullable: false),
                    ManagerPhoneNumber = table.Column<string>(maxLength: 32, nullable: false),
                    ManagerEmail = table.Column<string>(maxLength: 512, nullable: false),
                    ManagerPort = table.Column<int>(nullable: false),
                    IsAlarm = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_BoxManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_App_BoxManagers_App_Boxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "App_Boxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "App_SensorConfigs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    BoxId = table.Column<long>(nullable: false),
                    SensorId = table.Column<long>(nullable: false),
                    HighValue = table.Column<double>(nullable: false),
                    LowValue = table.Column<double>(nullable: false),
                    TargetValue = table.Column<double>(nullable: false),
                    InrangeColor = table.Column<string>(maxLength: 128, nullable: true),
                    OutrangeColor = table.Column<string>(maxLength: 128, nullable: true),
                    IsAlarm = table.Column<bool>(nullable: false),
                    AlarmMessage = table.Column<string>(nullable: true),
                    BoxPort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_SensorConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_App_SensorConfigs_App_Boxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "App_Boxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_App_SensorConfigs_App_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "App_Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_App_BoxManagers_BoxId",
                table: "App_BoxManagers",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_App_SensorConfigs_BoxId",
                table: "App_SensorConfigs",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_App_SensorConfigs_SensorId",
                table: "App_SensorConfigs",
                column: "SensorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "App_BoxManagers");

            migrationBuilder.DropTable(
                name: "App_SensorConfigs");

            migrationBuilder.DropTable(
                name: "App_Boxes");

            migrationBuilder.DropTable(
                name: "App_Sensors");
        }
    }
}
