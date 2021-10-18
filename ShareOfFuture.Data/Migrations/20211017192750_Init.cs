using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareForFuture.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OfferingDeviceCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferingDeviceCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferingDeviceCategories_OfferingDeviceCategories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "OfferingDeviceCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfferingTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferingTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    EmailVerifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastLogingDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IdentityProvider = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserGroup_GroupId",
                        column: x => x.GroupId,
                        principalTable: "UserGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Complains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplainerId = table.Column<int>(type: "int", nullable: true),
                    ComplaineeId = table.Column<int>(type: "int", nullable: true),
                    AssignedToId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Complains_Users_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Complains_Users_ComplaineeId",
                        column: x => x.ComplaineeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Complains_Users_ComplainerId",
                        column: x => x.ComplainerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfferingDevices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Condition = table.Column<int>(type: "int", nullable: false),
                    LastVerificationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2021, 10, 17, 15, 27, 50, 464, DateTimeKind.Unspecified).AddTicks(9081), new TimeSpan(0, -4, 0, 0, 0))),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    MarkedAsUnavailableDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferingDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferingDevices_OfferingDeviceCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "OfferingDeviceCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfferingDevices_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComplainNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplainId = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DoneDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplainNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplainNotes_Complains_ComplainId",
                        column: x => x.ComplainId,
                        principalTable: "Complains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceUnavailabilityPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Until = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OfferingId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceUnavailabilityPeriods", x => x.Id);
                    table.CheckConstraint("CK_DeviceUnavailabilityPeriods_UntilAfterFrom", "[Until] IS NULL\r\n                OR [Until] > [From]");
                    table.ForeignKey(
                        name: "FK_DeviceUnavailabilityPeriods_OfferingDevices_OfferingId",
                        column: x => x.OfferingId,
                        principalTable: "OfferingDevices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OfferingDeviceImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfferingId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferingDeviceImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferingDeviceImages_OfferingDevices_OfferingId",
                        column: x => x.OfferingId,
                        principalTable: "OfferingDevices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OfferingOfferingTag",
                columns: table => new
                {
                    OfferingsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferingOfferingTag", x => new { x.OfferingsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_OfferingOfferingTag_OfferingDevices_OfferingsId",
                        column: x => x.OfferingsId,
                        principalTable: "OfferingDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferingOfferingTag_OfferingTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "OfferingTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sharings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowerId = table.Column<int>(type: "int", nullable: true),
                    OfferingId = table.Column<int>(type: "int", nullable: true),
                    From = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Until = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastRequestNotificationSentDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OfferingWasAccepted = table.Column<bool>(type: "bit", nullable: true),
                    AcceptedDeclinedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    AcceptOrDeclineMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShareActivationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ShareDoneDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LenderToBorrowerRating = table.Column<int>(type: "int", nullable: true),
                    BorrowerToLenderRating = table.Column<int>(type: "int", nullable: true),
                    BorrowerToOffering = table.Column<int>(type: "int", nullable: true),
                    LenderToBorrowerNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BorrowerToLenderNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BorrowerToOfferingNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sharings", x => x.Id);
                    table.CheckConstraint("CK_Sharings_UntilAfterFrom", "[Until] > [From]");
                    table.ForeignKey(
                        name: "FK_Sharings_OfferingDevices_OfferingId",
                        column: x => x.OfferingId,
                        principalTable: "OfferingDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sharings_Users_BorrowerId",
                        column: x => x.BorrowerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "UserGroup",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2021, 10, 17, 15, 27, 50, 465, DateTimeKind.Unspecified).AddTicks(8243), new TimeSpan(0, -4, 0, 0, 0)), "S4FEmployee", new DateTimeOffset(new DateTime(2021, 10, 17, 15, 27, 50, 465, DateTimeKind.Unspecified).AddTicks(8264), new TimeSpan(0, -4, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "UserGroup",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { 2, new DateTimeOffset(new DateTime(2021, 10, 17, 15, 27, 50, 465, DateTimeKind.Unspecified).AddTicks(8269), new TimeSpan(0, -4, 0, 0, 0)), "Manager", new DateTimeOffset(new DateTime(2021, 10, 17, 15, 27, 50, 465, DateTimeKind.Unspecified).AddTicks(8271), new TimeSpan(0, -4, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "UserGroup",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { 3, new DateTimeOffset(new DateTime(2021, 10, 17, 15, 27, 50, 465, DateTimeKind.Unspecified).AddTicks(8275), new TimeSpan(0, -4, 0, 0, 0)), "SystemAdministrator", new DateTimeOffset(new DateTime(2021, 10, 17, 15, 27, 50, 465, DateTimeKind.Unspecified).AddTicks(8277), new TimeSpan(0, -4, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_ComplainNotes_ComplainId",
                table: "ComplainNotes",
                column: "ComplainId");

            migrationBuilder.CreateIndex(
                name: "IX_Complains_AssignedToId",
                table: "Complains",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Complains_ComplaineeId",
                table: "Complains",
                column: "ComplaineeId");

            migrationBuilder.CreateIndex(
                name: "IX_Complains_ComplainerId",
                table: "Complains",
                column: "ComplainerId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceUnavailabilityPeriods_OfferingId",
                table: "DeviceUnavailabilityPeriods",
                column: "OfferingId",
                unique: true,
                filter: "[OfferingId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OfferingDeviceCategories_Name",
                table: "OfferingDeviceCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OfferingDeviceCategories_ParentCategoryId",
                table: "OfferingDeviceCategories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferingDeviceImages_OfferingId",
                table: "OfferingDeviceImages",
                column: "OfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferingDevices_CategoryId",
                table: "OfferingDevices",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferingDevices_OwnerId",
                table: "OfferingDevices",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferingOfferingTag_TagsId",
                table: "OfferingOfferingTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Sharings_BorrowerId",
                table: "Sharings",
                column: "BorrowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sharings_OfferingId",
                table: "Sharings",
                column: "OfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_Name",
                table: "UserGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupId",
                table: "Users",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplainNotes");

            migrationBuilder.DropTable(
                name: "DeviceUnavailabilityPeriods");

            migrationBuilder.DropTable(
                name: "OfferingDeviceImages");

            migrationBuilder.DropTable(
                name: "OfferingOfferingTag");

            migrationBuilder.DropTable(
                name: "Sharings");

            migrationBuilder.DropTable(
                name: "Complains");

            migrationBuilder.DropTable(
                name: "OfferingTags");

            migrationBuilder.DropTable(
                name: "OfferingDevices");

            migrationBuilder.DropTable(
                name: "OfferingDeviceCategories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserGroup");
        }
    }
}
