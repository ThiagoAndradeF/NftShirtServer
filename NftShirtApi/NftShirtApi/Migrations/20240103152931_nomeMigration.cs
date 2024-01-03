using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NftShirtApi.Migrations
{
    /// <inheritdoc />
    public partial class nomeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Adress = table.Column<string>(type: "text", nullable: false),
                    Abi = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Adress);
                });

            migrationBuilder.CreateTable(
                name: "Itens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ContractAdress = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colections_Contracts_ContractAdress",
                        column: x => x.ContractAdress,
                        principalTable: "Contracts",
                        principalColumn: "Adress",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Adress = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nfts",
                columns: table => new
                {
                    TokenId = table.Column<string>(type: "text", nullable: false),
                    TokenURI = table.Column<string>(type: "text", nullable: false),
                    OwnerAddress = table.Column<string>(type: "text", nullable: false),
                    ColectionID = table.Column<int>(type: "integer", nullable: false),
                    WalletID = table.Column<int>(type: "integer", nullable: false),
                    Metadata = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nfts", x => x.TokenId);
                    table.ForeignKey(
                        name: "FK_Nfts_Colections_ColectionID",
                        column: x => x.ColectionID,
                        principalTable: "Colections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nfts_Wallets_WalletID",
                        column: x => x.WalletID,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nftags",
                columns: table => new
                {
                    LinkTag = table.Column<string>(type: "text", nullable: false),
                    ItenId = table.Column<int>(type: "integer", nullable: false),
                    ActiveId = table.Column<int>(type: "integer", nullable: false),
                    NftHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nftags", x => x.LinkTag);
                    table.ForeignKey(
                        name: "FK_Nftags_Itens_ItenId",
                        column: x => x.ItenId,
                        principalTable: "Itens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nftags_Nfts_NftHash",
                        column: x => x.NftHash,
                        principalTable: "Nfts",
                        principalColumn: "TokenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colections_ContractAdress",
                table: "Colections",
                column: "ContractAdress");

            migrationBuilder.CreateIndex(
                name: "IX_Nftags_ItenId",
                table: "Nftags",
                column: "ItenId");

            migrationBuilder.CreateIndex(
                name: "IX_Nftags_NftHash",
                table: "Nftags",
                column: "NftHash");

            migrationBuilder.CreateIndex(
                name: "IX_Nfts_ColectionID",
                table: "Nfts",
                column: "ColectionID");

            migrationBuilder.CreateIndex(
                name: "IX_Nfts_WalletID",
                table: "Nfts",
                column: "WalletID");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nftags");

            migrationBuilder.DropTable(
                name: "Itens");

            migrationBuilder.DropTable(
                name: "Nfts");

            migrationBuilder.DropTable(
                name: "Colections");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
