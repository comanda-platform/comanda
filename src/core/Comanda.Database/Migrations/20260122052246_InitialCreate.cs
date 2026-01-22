using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comanda.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApiKeyCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ToBaseMultiplier = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasCreditLine = table.Column<bool>(type: "bit", nullable: false),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Account_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Account_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
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
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
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
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "ClientGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasCreditLine = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientGroup_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientGroup_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientGroup_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Person_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Person_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductType_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductType_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductType_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Side",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Side", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Side_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Side_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Side_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supplier_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Supplier_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Supplier_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InventoryItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true),
                    BaseUnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryItem_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryItem_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryItem_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryItem_Unit_BaseUnitId",
                        column: x => x.BaseUnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true),
                    ClientGroupId = table.Column<int>(type: "int", nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Client_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Client_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Client_ClientGroup_GroupId",
                        column: x => x.GroupId,
                        principalTable: "ClientGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Authorization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authorization_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Authorization_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Authorization_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Authorization_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Authorization_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonContact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonContact_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonContact_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonContact_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonContact_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuggestedForDailyMenu = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_ProductType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailySideAvailability",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    SideId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailySideAvailability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailySideAvailability_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailySideAvailability_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailySideAvailability_Side_SideId",
                        column: x => x.SideId,
                        principalTable: "Side",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedFields = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangedById = table.Column<int>(type: "int", nullable: true),
                    ChangedByClientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLog_AspNetUsers_ChangedById",
                        column: x => x.ChangedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuditLog_Client_ChangedByClientId",
                        column: x => x.ChangedByClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientContact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactTypeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientContact_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClientContact_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClientContact_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClientContact_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationTypeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    AddressLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    ClientGroupId = table.Column<int>(type: "int", nullable: true),
                    AccountDatabaseEntityId = table.Column<int>(type: "int", nullable: true),
                    PersonDatabaseEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Account_AccountDatabaseEntityId",
                        column: x => x.AccountDatabaseEntityId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Location_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Location_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Location_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Location_ClientGroup_ClientGroupId",
                        column: x => x.ClientGroupId,
                        principalTable: "ClientGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Location_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Location_Person_PersonDatabaseEntityId",
                        column: x => x.PersonDatabaseEntityId,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductPriceHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPriceHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPriceHistory_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductPriceHistory_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedPortions = table.Column<int>(type: "int", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipe_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Recipe_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Recipe_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Recipe_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyMenu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyMenu_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyMenu_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyMenu_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyMenu_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpenseTypeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayOfMonth = table.Column<int>(type: "int", nullable: true),
                    DayOfWeek = table.Column<int>(type: "int", nullable: true),
                    SpecificPayableDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaysWorkedPerWeek = table.Column<int>(type: "int", nullable: true),
                    CalculateDailyRate = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expense_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expense_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expense_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expense_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expense_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InventoryPurchase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchasedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    StoreLocationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryPurchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryPurchase_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryPurchase_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryPurchase_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryPurchase_Location_StoreLocationId",
                        column: x => x.StoreLocationId,
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryPurchase_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderStatusTypeId = table.Column<int>(type: "int", nullable: false),
                    FulfillmentTypeId = table.Column<int>(type: "int", nullable: false),
                    OrderSourceId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedByClientId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    ClientGroupId = table.Column<int>(type: "int", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_ClientGroup_ClientGroupId",
                        column: x => x.ClientGroupId,
                        principalTable: "ClientGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Client_CreatedByClientId",
                        column: x => x.CreatedByClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    InventoryItemId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_InventoryItem_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "InventoryItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DailyMenuItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SequenceOrder = table.Column<int>(type: "int", nullable: false),
                    OverriddenName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverriddenPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true),
                    DailyMenuId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyMenuItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyMenuItem_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyMenuItem_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyMenuItem_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyMenuItem_DailyMenu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "DailyMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyMenuItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionBatch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    DailyMenuId = table.Column<int>(type: "int", nullable: false),
                    ProductionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Yield = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartedById = table.Column<int>(type: "int", nullable: true),
                    CompletedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionBatch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionBatch_AspNetUsers_CompletedById",
                        column: x => x.CompletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductionBatch_AspNetUsers_StartedById",
                        column: x => x.StartedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductionBatch_DailyMenu_DailyMenuId",
                        column: x => x.DailyMenuId,
                        principalTable: "DailyMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionBatch_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryPurchaseLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true),
                    InventoryPurchaseId = table.Column<int>(type: "int", nullable: false),
                    PurchaseId = table.Column<int>(type: "int", nullable: false),
                    InventoryItemId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryPurchaseLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryPurchaseLine_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryPurchaseLine_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryPurchaseLine_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryPurchaseLine_InventoryItem_ItemId",
                        column: x => x.ItemId,
                        principalTable: "InventoryItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryPurchaseLine_InventoryPurchase_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "InventoryPurchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryPurchaseLine_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrepStatus = table.Column<int>(type: "int", nullable: false),
                    PrepStartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrepCompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContainerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectedSidesJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountReasonId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLine_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderLine_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderLine_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderLine_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderLine_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderLine_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatusHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderStatusTypeId = table.Column<int>(type: "int", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedById = table.Column<int>(type: "int", nullable: true),
                    ChangedByClientId = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderStatusHistory_AspNetUsers_ChangedById",
                        column: x => x.ChangedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderStatusHistory_Client_ChangedByClientId",
                        column: x => x.ChangedByClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderStatusHistory_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientLedgerEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LedgerEntryTypeId = table.Column<int>(type: "int", nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    OrderLineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientLedgerEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientLedgerEntry_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientLedgerEntry_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientLedgerEntry_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientLedgerEntry_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientLedgerEntry_OrderLine_OrderLineId",
                        column: x => x.OrderLineId,
                        principalTable: "OrderLine",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderLineOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsIncluded = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    DeletedById = table.Column<int>(type: "int", nullable: true),
                    OrderLineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLineOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLineOption_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderLineOption_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderLineOption_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderLineOption_OrderLine_OrderLineId",
                        column: x => x.OrderLineId,
                        principalTable: "OrderLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderLineSide",
                columns: table => new
                {
                    OrderLineId = table.Column<int>(type: "int", nullable: false),
                    SideId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLineSide", x => new { x.OrderLineId, x.SideId });
                    table.ForeignKey(
                        name: "FK_OrderLineSide_OrderLine_OrderLineId",
                        column: x => x.OrderLineId,
                        principalTable: "OrderLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderLineSide_Side_SideId",
                        column: x => x.SideId,
                        principalTable: "Side",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    ClientGroupId = table.Column<int>(type: "int", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    OrderLineId = table.Column<int>(type: "int", nullable: true),
                    OrderStatusHistoryId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    ProductPriceHistoryId = table.Column<int>(type: "int", nullable: true),
                    ClientLedgerEntryId = table.Column<int>(type: "int", nullable: true),
                    SideId = table.Column<int>(type: "int", nullable: true),
                    AccountDatabaseEntityId = table.Column<int>(type: "int", nullable: true),
                    DailyMenuDatabaseEntityId = table.Column<int>(type: "int", nullable: true),
                    DailyMenuItemDatabaseEntityId = table.Column<int>(type: "int", nullable: true),
                    ExpenseDatabaseEntityId = table.Column<int>(type: "int", nullable: true),
                    InventoryItemDatabaseEntityId = table.Column<int>(type: "int", nullable: true),
                    InventoryPurchaseLineDatabaseEntityId = table.Column<int>(type: "int", nullable: true),
                    PersonDatabaseEntityId = table.Column<int>(type: "int", nullable: true),
                    RecipeDatabaseEntityId = table.Column<int>(type: "int", nullable: true),
                    RecipeIngredientDatabaseEntityId = table.Column<int>(type: "int", nullable: true),
                    SupplierDatabaseEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Note_Account_AccountDatabaseEntityId",
                        column: x => x.AccountDatabaseEntityId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_ClientGroup_ClientGroupId",
                        column: x => x.ClientGroupId,
                        principalTable: "ClientGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_ClientLedgerEntry_ClientLedgerEntryId",
                        column: x => x.ClientLedgerEntryId,
                        principalTable: "ClientLedgerEntry",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_DailyMenuItem_DailyMenuItemDatabaseEntityId",
                        column: x => x.DailyMenuItemDatabaseEntityId,
                        principalTable: "DailyMenuItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_DailyMenu_DailyMenuDatabaseEntityId",
                        column: x => x.DailyMenuDatabaseEntityId,
                        principalTable: "DailyMenu",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_Expense_ExpenseDatabaseEntityId",
                        column: x => x.ExpenseDatabaseEntityId,
                        principalTable: "Expense",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_InventoryItem_InventoryItemDatabaseEntityId",
                        column: x => x.InventoryItemDatabaseEntityId,
                        principalTable: "InventoryItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_InventoryPurchaseLine_InventoryPurchaseLineDatabaseEntityId",
                        column: x => x.InventoryPurchaseLineDatabaseEntityId,
                        principalTable: "InventoryPurchaseLine",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_OrderLine_OrderLineId",
                        column: x => x.OrderLineId,
                        principalTable: "OrderLine",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_OrderStatusHistory_OrderStatusHistoryId",
                        column: x => x.OrderStatusHistoryId,
                        principalTable: "OrderStatusHistory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_Person_PersonDatabaseEntityId",
                        column: x => x.PersonDatabaseEntityId,
                        principalTable: "Person",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_ProductPriceHistory_ProductPriceHistoryId",
                        column: x => x.ProductPriceHistoryId,
                        principalTable: "ProductPriceHistory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_RecipeIngredient_RecipeIngredientDatabaseEntityId",
                        column: x => x.RecipeIngredientDatabaseEntityId,
                        principalTable: "RecipeIngredient",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_Recipe_RecipeDatabaseEntityId",
                        column: x => x.RecipeDatabaseEntityId,
                        principalTable: "Recipe",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_Side_SideId",
                        column: x => x.SideId,
                        principalTable: "Side",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_Supplier_SupplierDatabaseEntityId",
                        column: x => x.SupplierDatabaseEntityId,
                        principalTable: "Supplier",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_CreatedById",
                table: "Account",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Account_DeletedById",
                table: "Account",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Account_LastModifiedById",
                table: "Account",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                name: "IX_AspNetUsers_CreatedById",
                table: "AspNetUsers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DeletedById",
                table: "AspNetUsers",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LastModifiedById",
                table: "AspNetUsers",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_ChangedByClientId",
                table: "AuditLog",
                column: "ChangedByClientId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_ChangedById",
                table: "AuditLog",
                column: "ChangedById");

            migrationBuilder.CreateIndex(
                name: "IX_Authorization_AccountId",
                table: "Authorization",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Authorization_CreatedById",
                table: "Authorization",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Authorization_DeletedById",
                table: "Authorization",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Authorization_LastModifiedById",
                table: "Authorization",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Authorization_PersonId",
                table: "Authorization",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_CreatedById",
                table: "Client",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Client_DeletedById",
                table: "Client",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Client_GroupId",
                table: "Client",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_LastModifiedById",
                table: "Client",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientContact_ClientId",
                table: "ClientContact",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientContact_CreatedById",
                table: "ClientContact",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientContact_DeletedById",
                table: "ClientContact",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientContact_LastModifiedById",
                table: "ClientContact",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientGroup_CreatedById",
                table: "ClientGroup",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientGroup_DeletedById",
                table: "ClientGroup",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientGroup_LastModifiedById",
                table: "ClientGroup",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientLedgerEntry_ClientId",
                table: "ClientLedgerEntry",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientLedgerEntry_CreatedById",
                table: "ClientLedgerEntry",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientLedgerEntry_DeletedById",
                table: "ClientLedgerEntry",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientLedgerEntry_LastModifiedById",
                table: "ClientLedgerEntry",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientLedgerEntry_OrderLineId",
                table: "ClientLedgerEntry",
                column: "OrderLineId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMenu_CreatedById",
                table: "DailyMenu",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMenu_DeletedById",
                table: "DailyMenu",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMenu_LastModifiedById",
                table: "DailyMenu",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMenu_LocationId",
                table: "DailyMenu",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMenuItem_CreatedById",
                table: "DailyMenuItem",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMenuItem_DeletedById",
                table: "DailyMenuItem",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMenuItem_LastModifiedById",
                table: "DailyMenuItem",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMenuItem_MenuId",
                table: "DailyMenuItem",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMenuItem_ProductId",
                table: "DailyMenuItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DailySideAvailability_CreatedById",
                table: "DailySideAvailability",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DailySideAvailability_LastModifiedById",
                table: "DailySideAvailability",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DailySideAvailability_SideId",
                table: "DailySideAvailability",
                column: "SideId");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_CreatedById",
                table: "Expense",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_DeletedById",
                table: "Expense",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_EmployeeId",
                table: "Expense",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_LastModifiedById",
                table: "Expense",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_LocationId",
                table: "Expense",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_BaseUnitId",
                table: "InventoryItem",
                column: "BaseUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_CreatedById",
                table: "InventoryItem",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_DeletedById",
                table: "InventoryItem",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_LastModifiedById",
                table: "InventoryItem",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryPurchase_CreatedById",
                table: "InventoryPurchase",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryPurchase_DeletedById",
                table: "InventoryPurchase",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryPurchase_LastModifiedById",
                table: "InventoryPurchase",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryPurchase_StoreLocationId",
                table: "InventoryPurchase",
                column: "StoreLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryPurchase_SupplierId",
                table: "InventoryPurchase",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryPurchaseLine_CreatedById",
                table: "InventoryPurchaseLine",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryPurchaseLine_DeletedById",
                table: "InventoryPurchaseLine",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryPurchaseLine_ItemId",
                table: "InventoryPurchaseLine",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryPurchaseLine_LastModifiedById",
                table: "InventoryPurchaseLine",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryPurchaseLine_PurchaseId",
                table: "InventoryPurchaseLine",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryPurchaseLine_UnitId",
                table: "InventoryPurchaseLine",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_AccountDatabaseEntityId",
                table: "Location",
                column: "AccountDatabaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_ClientGroupId",
                table: "Location",
                column: "ClientGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_ClientId",
                table: "Location",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_CreatedById",
                table: "Location",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Location_DeletedById",
                table: "Location",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Location_LastModifiedById",
                table: "Location",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Location_PersonDatabaseEntityId",
                table: "Location",
                column: "PersonDatabaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_AccountDatabaseEntityId",
                table: "Note",
                column: "AccountDatabaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_ClientGroupId",
                table: "Note",
                column: "ClientGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_ClientId",
                table: "Note",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_ClientLedgerEntryId",
                table: "Note",
                column: "ClientLedgerEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_CreatedById",
                table: "Note",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Note_DailyMenuDatabaseEntityId",
                table: "Note",
                column: "DailyMenuDatabaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_DailyMenuItemDatabaseEntityId",
                table: "Note",
                column: "DailyMenuItemDatabaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_ExpenseDatabaseEntityId",
                table: "Note",
                column: "ExpenseDatabaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_InventoryItemDatabaseEntityId",
                table: "Note",
                column: "InventoryItemDatabaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_InventoryPurchaseLineDatabaseEntityId",
                table: "Note",
                column: "InventoryPurchaseLineDatabaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_LocationId",
                table: "Note",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_OrderId",
                table: "Note",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_OrderLineId",
                table: "Note",
                column: "OrderLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_OrderStatusHistoryId",
                table: "Note",
                column: "OrderStatusHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_PersonDatabaseEntityId",
                table: "Note",
                column: "PersonDatabaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_ProductId",
                table: "Note",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_ProductPriceHistoryId",
                table: "Note",
                column: "ProductPriceHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_RecipeDatabaseEntityId",
                table: "Note",
                column: "RecipeDatabaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_RecipeIngredientDatabaseEntityId",
                table: "Note",
                column: "RecipeIngredientDatabaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_SideId",
                table: "Note",
                column: "SideId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_SupplierDatabaseEntityId",
                table: "Note",
                column: "SupplierDatabaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ClientGroupId",
                table: "Order",
                column: "ClientGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ClientId",
                table: "Order",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CreatedByClientId",
                table: "Order",
                column: "CreatedByClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CreatedById",
                table: "Order",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Order_DeletedById",
                table: "Order",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Order_LastModifiedById",
                table: "Order",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Order_LocationId",
                table: "Order",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_ClientId",
                table: "OrderLine",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_CreatedById",
                table: "OrderLine",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_DeletedById",
                table: "OrderLine",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_LastModifiedById",
                table: "OrderLine",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_OrderId",
                table: "OrderLine",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_ProductId",
                table: "OrderLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineOption_CreatedById",
                table: "OrderLineOption",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineOption_DeletedById",
                table: "OrderLineOption",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineOption_LastModifiedById",
                table: "OrderLineOption",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineOption_OrderLineId",
                table: "OrderLineOption",
                column: "OrderLineId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineSide_SideId",
                table: "OrderLineSide",
                column: "SideId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusHistory_ChangedByClientId",
                table: "OrderStatusHistory",
                column: "ChangedByClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusHistory_ChangedById",
                table: "OrderStatusHistory",
                column: "ChangedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusHistory_OrderId",
                table: "OrderStatusHistory",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_CreatedById",
                table: "Person",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Person_DeletedById",
                table: "Person",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Person_LastModifiedById",
                table: "Person",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PersonContact_CreatedById",
                table: "PersonContact",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PersonContact_DeletedById",
                table: "PersonContact",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_PersonContact_LastModifiedById",
                table: "PersonContact",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PersonContact_PersonId",
                table: "PersonContact",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CreatedById",
                table: "Product",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DeletedById",
                table: "Product",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Product_LastModifiedById",
                table: "Product",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Product_TypeId",
                table: "Product",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionBatch_CompletedById",
                table: "ProductionBatch",
                column: "CompletedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionBatch_DailyMenuId",
                table: "ProductionBatch",
                column: "DailyMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionBatch_ProductId",
                table: "ProductionBatch",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionBatch_StartedById",
                table: "ProductionBatch",
                column: "StartedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPriceHistory_CreatedById",
                table: "ProductPriceHistory",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPriceHistory_ProductId",
                table: "ProductPriceHistory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductType_CreatedById",
                table: "ProductType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductType_DeletedById",
                table: "ProductType",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductType_LastModifiedById",
                table: "ProductType",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_CreatedById",
                table: "Recipe",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_DeletedById",
                table: "Recipe",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_LastModifiedById",
                table: "Recipe",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_ProductId",
                table: "Recipe",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_CreatedById",
                table: "RecipeIngredient",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_DeletedById",
                table: "RecipeIngredient",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_InventoryItemId",
                table: "RecipeIngredient",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_LastModifiedById",
                table: "RecipeIngredient",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_RecipeId",
                table: "RecipeIngredient",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_UnitId",
                table: "RecipeIngredient",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Side_CreatedById",
                table: "Side",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Side_DeletedById",
                table: "Side",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Side_LastModifiedById",
                table: "Side",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_CreatedById",
                table: "Supplier",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_DeletedById",
                table: "Supplier",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_LastModifiedById",
                table: "Supplier",
                column: "LastModifiedById");
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
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "Authorization");

            migrationBuilder.DropTable(
                name: "ClientContact");

            migrationBuilder.DropTable(
                name: "DailySideAvailability");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "OrderLineOption");

            migrationBuilder.DropTable(
                name: "OrderLineSide");

            migrationBuilder.DropTable(
                name: "PersonContact");

            migrationBuilder.DropTable(
                name: "ProductionBatch");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ClientLedgerEntry");

            migrationBuilder.DropTable(
                name: "DailyMenuItem");

            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropTable(
                name: "InventoryPurchaseLine");

            migrationBuilder.DropTable(
                name: "OrderStatusHistory");

            migrationBuilder.DropTable(
                name: "ProductPriceHistory");

            migrationBuilder.DropTable(
                name: "RecipeIngredient");

            migrationBuilder.DropTable(
                name: "Side");

            migrationBuilder.DropTable(
                name: "OrderLine");

            migrationBuilder.DropTable(
                name: "DailyMenu");

            migrationBuilder.DropTable(
                name: "InventoryPurchase");

            migrationBuilder.DropTable(
                name: "InventoryItem");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "ClientGroup");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
