using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace administracionUsuarios.Migrations
{
    public partial class v100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cargos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true),
                    idUsuarioCreacion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cargos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "departamentos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true),
                    idUsuarioCreacion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departamentos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    primerNombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    segundoNombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    primerApellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    segundoApellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    idDepartamento = table.Column<int>(type: "int", nullable: true),
                    idCargo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK__users__idCargo__29572725",
                        column: x => x.idCargo,
                        principalTable: "cargos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__users__idDeparta__286302EC",
                        column: x => x.idDepartamento,
                        principalTable: "departamentos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_idCargo",
                table: "users",
                column: "idCargo");

            migrationBuilder.CreateIndex(
                name: "IX_users_idDepartamento",
                table: "users",
                column: "idDepartamento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "cargos");

            migrationBuilder.DropTable(
                name: "departamentos");
        }
    }
}
