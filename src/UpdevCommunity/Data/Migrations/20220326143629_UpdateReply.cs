using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpdevCommunity.Data.Migrations
{
    public partial class UpdateReply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Replies_ReplyParentId",
                table: "Replies");

            migrationBuilder.AlterColumn<int>(
                name: "ReplyParentId",
                table: "Replies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01B168FE-810B-432D-9010-233BA0B380E9",
                column: "ConcurrencyStamp",
                value: "e67d56de-c2c8-4532-9ddc-a6537d6afbf2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2301D884-221A-4E7D-B509-0113DCC043E1",
                column: "ConcurrencyStamp",
                value: "0fc4c3f8-2b6b-4c4e-bdb6-a508bdafd1b1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78A7570F-3CE5-48BA-9461-80283ED1D94D",
                column: "ConcurrencyStamp",
                value: "6805b585-adb8-4b8f-a562-7081f4b64e83");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7D9B7113-A8F8-4035-99A7-A20DD400F6A3",
                column: "ConcurrencyStamp",
                value: "9281d478-a1e1-452d-81f3-cbab29d0c3bc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "FFE8796D-9AB2-4C5C-9B01-BB58C9F73657",
                column: "ConcurrencyStamp",
                value: "09324464-8a29-4fb9-ade4-26991a7996a7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "198E6AEA-6827-4562-B415-242146DE9B9B",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4bff884c-4c26-4b92-9dbf-0e80eb117abe", "AQAAAAEAACcQAAAAEJkU/z8LkcVTsA+ymPfCfKrz4/5WUuBNwKufE8NzMie28Lvk1IP/ObGmpmlLCaUbLQ==", "2f8bb32d-ff51-4aec-b026-9e93a2b0f4a0" });

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Replies_ReplyParentId",
                table: "Replies",
                column: "ReplyParentId",
                principalTable: "Replies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Replies_ReplyParentId",
                table: "Replies");

            migrationBuilder.AlterColumn<int>(
                name: "ReplyParentId",
                table: "Replies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01B168FE-810B-432D-9010-233BA0B380E9",
                column: "ConcurrencyStamp",
                value: "2b167212-d316-4578-93f5-0337fac56bf8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2301D884-221A-4E7D-B509-0113DCC043E1",
                column: "ConcurrencyStamp",
                value: "e5ce70e6-d1e7-47f6-92df-1597711cd184");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78A7570F-3CE5-48BA-9461-80283ED1D94D",
                column: "ConcurrencyStamp",
                value: "0bd199d5-3c6f-4e56-a0f0-a2c6d4e8f1b1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7D9B7113-A8F8-4035-99A7-A20DD400F6A3",
                column: "ConcurrencyStamp",
                value: "f73772d2-0e66-4fd9-8a3d-68bfc7f69097");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "FFE8796D-9AB2-4C5C-9B01-BB58C9F73657",
                column: "ConcurrencyStamp",
                value: "a6acf182-a342-4d5f-81be-e345850d62f6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "198E6AEA-6827-4562-B415-242146DE9B9B",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7d528a28-9d9c-479c-81aa-12ae02814400", "AQAAAAEAACcQAAAAEJFwrI7Mm/RSAuN1TM5lSJmVzukKf3whpbReaRqOzMz5uPIrbhz9sDF7rDTMoKoPmA==", "3e96291b-82f5-424a-946e-725d60cf29f3" });

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Replies_ReplyParentId",
                table: "Replies",
                column: "ReplyParentId",
                principalTable: "Replies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
