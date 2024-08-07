﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class LastUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Nickname = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Birthdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Rola = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    QuestDateStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    QuestDateEnd = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    SlotLimit = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentOccupiedSlots = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuestId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    QuestDateStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    QuestDateEnd = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CancellationRequested = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsCanceled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Quests_QuestId",
                        column: x => x.QuestId,
                        principalTable: "Quests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Userdata",
                columns: table => new
                {
                    UserDataId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    QuestId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Userdata", x => x.UserDataId);
                    table.ForeignKey(
                        name: "FK_Userdata_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Userdata_Quests_QuestId",
                        column: x => x.QuestId,
                        principalTable: "Quests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CancelRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CancelRequests_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Quests",
                columns: new[] { "Id", "Address", "City", "CurrentOccupiedSlots", "Description", "ImageUrl", "Name", "QuestDateEnd", "QuestDateStart", "SlotLimit" },
                values: new object[,]
                {
                    { 1, "Międzynarodowe Targi Poznańskie, Głogowska 4, Poznań", "Poznań", 0, "Quest polega na zajmowaniu się dziećmi w wieku poniżej 13 roku życia. Jako główne cechy oczekujemy, życzliwości i umiejętności opieki nad najmłodszymi uczestnikami konwentu :)", "/images/1.jpg", "Pyrkon - Bejbiczki", new DateTime(2025, 6, 15, 23, 59, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 13, 8, 0, 0, 0, DateTimeKind.Unspecified), 20 },
                    { 2, "Międzynarodowe Targi Poznańskie, Głogowska 4, Poznań", "Poznań", 0, "Quest polega na udzielaniu pomocy gościom oraz prowadzącym sesje RPG", "/images/1.jpg", "Pyrkon - RPG", new DateTime(2025, 6, 15, 23, 59, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 12, 8, 0, 0, 0, DateTimeKind.Unspecified), 40 },
                    { 3, "Międzynarodowe Targi Poznańskie, Głogowska 4", "Poznań", 0, "Quest polega na wypożyczaniu oraz odbieraniu gier w wypożyczalni. Wymagamy podstawowej wiedzy z zakresu gier planszowych oraz chęci do nauki nowych gier.", "/images/1.jpg", "Pyrkon - Wypożyczalnia", new DateTime(2025, 6, 15, 23, 59, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 12, 8, 0, 0, 0, DateTimeKind.Unspecified), 30 },
                    { 4, "Liceum Ogólnokształące Mistrzostwa Sporotowego im. Poznańskich Olimpijczyków, Osiedle Tysiąclecia 43", "Poznań", 0, "Opieka nad sceną, czyli pomoc występującym, szybkie sprzątanie pomiędzy występami", "/images/2.jpg", "AnimeCon - Opieka Sceny", new DateTime(2024, 10, 31, 23, 59, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 31, 8, 0, 0, 0, DateTimeKind.Unspecified), 10 },
                    { 5, "Liceum Ogólnokształące Mistrzostwa Sporotowego im. Poznańskich Olimpijczyków, Osiedle Tysiąclecia 43", "Poznań", 0, "Prowadzenie akry", "/images/2.jpg", "AnimeCon - Akra", new DateTime(2024, 10, 31, 23, 59, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 31, 8, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 6, "Collegium Da Vinci, Kutrzeby 10, Poznań", "Poznań", 0, "Zajmowanie się terenem plenerowym aby były na bierząco dostarczane przedmioty do prowadzących", "/images/3.png", "Hikari - Plener", new DateTime(2024, 8, 18, 23, 59, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 16, 8, 0, 0, 0, DateTimeKind.Unspecified), 15 },
                    { 7, "Collegium Da Vinci, Kutrzeby 10, Poznań", "Poznań", 0, "Administracja sceną, wpuszczanie oraz informowanie o zejscu ze sceny", "/images/3.png", "Hikari - Scena", new DateTime(2024, 8, 18, 23, 59, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 16, 8, 0, 0, 0, DateTimeKind.Unspecified), 10 },
                    { 8, "Hotel Novotel Marina, Jelitkowska 20, 80-342 Gdańsk", "Gdańsk", 0, "Pomoc pracownikom ZTM Gdańsk przy ładowaniu oraz rozładowaniu pasażerów, podróżujących pomiędzy Hotelem a eventem w Plenerze", "/images/4.png", "Gdakon - Wsparcie ZTM Hotel", new DateTime(2025, 3, 1, 23, 59, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 9, "Hotel Novotel Marina, Jelitkowska 20, 80-342 Gdańsk", "Gdańsk", 0, "Pomoc pracownikom ZTM Gdańsk przy ładowaniu oraz rozładowaniu pasażerów, podróżujących pomiędzy Hotelem a eventem w Plenerze", "/images/4.png", "Gdakon - Wsparcie ZTM Plener", new DateTime(2025, 2, 28, 23, 59, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 23, 10, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 10, "Hotel Novotel Marina, Jelitkowska 20, 80-342 Gdańsk", "Gdańsk", 0, "Prowadzenie akredytacji oraz pomoc przy odbiorach identyfikatorów", "/images/4.png", "Gdakon - Akredytacja", new DateTime(2025, 2, 28, 23, 59, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 23, 10, 0, 0, 0, DateTimeKind.Unspecified), 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CancelRequests_OrderId",
                table: "CancelRequests",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_QuestId",
                table: "Orders",
                column: "QuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Userdata_QuestId",
                table: "Userdata",
                column: "QuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Userdata_UserId",
                table: "Userdata",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CancelRequests");

            migrationBuilder.DropTable(
                name: "Userdata");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Quests");
        }
    }
}
