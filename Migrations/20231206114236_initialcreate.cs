using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Team_Project_4.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KHACH",
                columns: table => new
                {
                    MAKH = table.Column<string>(type: "nchar(7)", fixedLength: true, maxLength: 7, nullable: false),
                    TENKH = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TUOI = table.Column<int>(type: "int", nullable: false),
                    TEL = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    DIACHIKH = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CMNDKH = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    LOAIKHACH = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KHACH", x => x.MAKH);
                });

            migrationBuilder.CreateTable(
                name: "NHANVIEN",
                columns: table => new
                {
                    MANV = table.Column<string>(type: "nchar(7)", fixedLength: true, maxLength: 7, nullable: false),
                    HOTEN = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PHAI = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: true),
                    NGAYSINH = table.Column<DateTime>(type: "date", nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DIACHI = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NHANVIEN", x => x.MANV);
                });

            migrationBuilder.CreateTable(
                name: "PHONG",
                columns: table => new
                {
                    MAP = table.Column<int>(type: "int", nullable: false),
                    TENPHONG = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LOAI = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DONGIA = table.Column<int>(type: "int", nullable: false),
                    TINHTRANG = table.Column<bool>(type: "bit", nullable: false),
                    GHICHU = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHONG", x => x.MAP);
                });

            migrationBuilder.CreateTable(
                name: "PHIEUTHUE",
                columns: table => new
                {
                    MAPT = table.Column<string>(type: "nchar(7)", fixedLength: true, maxLength: 7, nullable: false),
                    NGAYLAPPT = table.Column<DateTime>(type: "date", nullable: false),
                    MAKH = table.Column<string>(type: "nchar(7)", fixedLength: true, maxLength: 7, nullable: false),
                    MAP = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_1", x => x.MAPT);
                    table.ForeignKey(
                        name: "FK_PHIEUTHUE_KHACH",
                        column: x => x.MAKH,
                        principalTable: "KHACH",
                        principalColumn: "MAKH");
                    table.ForeignKey(
                        name: "FK_PHIEUTHUE_PHONG",
                        column: x => x.MAP,
                        principalTable: "PHONG",
                        principalColumn: "MAP");
                });

            migrationBuilder.CreateTable(
                name: "HOADON",
                columns: table => new
                {
                    MAHD = table.Column<string>(type: "nchar(7)", fixedLength: true, maxLength: 7, nullable: false),
                    SONGAYO = table.Column<int>(type: "int", nullable: true),
                    MANV = table.Column<string>(type: "nchar(7)", fixedLength: true, maxLength: 7, nullable: true),
                    TONGTIEN = table.Column<int>(type: "int", nullable: true),
                    MAPT = table.Column<string>(type: "nchar(7)", fixedLength: true, maxLength: 7, nullable: true),
                    MAKH = table.Column<string>(type: "nchar(7)", fixedLength: true, maxLength: 7, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HOADON", x => x.MAHD);
                    table.ForeignKey(
                        name: "FK_HOADON_KHACH",
                        column: x => x.MAKH,
                        principalTable: "KHACH",
                        principalColumn: "MAKH");
                    table.ForeignKey(
                        name: "FK_HOADON_NHANVIEN",
                        column: x => x.MANV,
                        principalTable: "NHANVIEN",
                        principalColumn: "MANV");
                    table.ForeignKey(
                        name: "FK_HOADON_PHIEUTHUE",
                        column: x => x.MAPT,
                        principalTable: "PHIEUTHUE",
                        principalColumn: "MAPT");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HOADON_MAKH",
                table: "HOADON",
                column: "MAKH");

            migrationBuilder.CreateIndex(
                name: "IX_HOADON_MANV",
                table: "HOADON",
                column: "MANV");

            migrationBuilder.CreateIndex(
                name: "IX_HOADON_MAPT",
                table: "HOADON",
                column: "MAPT");

            migrationBuilder.CreateIndex(
                name: "IX_PHIEUTHUE_MAKH",
                table: "PHIEUTHUE",
                column: "MAKH");

            migrationBuilder.CreateIndex(
                name: "IX_PHIEUTHUE_MAP",
                table: "PHIEUTHUE",
                column: "MAP");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HOADON");

            migrationBuilder.DropTable(
                name: "NHANVIEN");

            migrationBuilder.DropTable(
                name: "PHIEUTHUE");

            migrationBuilder.DropTable(
                name: "KHACH");

            migrationBuilder.DropTable(
                name: "PHONG");
        }
    }
}
