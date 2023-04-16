using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoGrado.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pelicula",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Generos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Puntuacion = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelicula", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNac = table.Column<DateTime>(type: "date", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fav",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPelicula = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fav", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fav_Pelicula_IdPelicula",
                        column: x => x.IdPelicula,
                        principalTable: "Pelicula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fav_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lista",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lista_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resena",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdPelicula = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Valoracion = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resena", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resena_Pelicula_IdPelicula",
                        column: x => x.IdPelicula,
                        principalTable: "Pelicula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resena_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ver",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPelicula = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ver_Pelicula_IdPelicula",
                        column: x => x.IdPelicula,
                        principalTable: "Pelicula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ver_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contiene",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPelicula = table.Column<int>(type: "int", nullable: false),
                    IdLista = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contiene", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contiene_Lista_IdLista",
                        column: x => x.IdLista,
                        principalTable: "Lista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contiene_Pelicula_IdPelicula",
                        column: x => x.IdPelicula,
                        principalTable: "Pelicula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contiene_IdLista",
                table: "Contiene",
                column: "IdLista");

            migrationBuilder.CreateIndex(
                name: "IX_Contiene_IdPelicula",
                table: "Contiene",
                column: "IdPelicula");

            migrationBuilder.CreateIndex(
                name: "IX_Fav_IdPelicula",
                table: "Fav",
                column: "IdPelicula");

            migrationBuilder.CreateIndex(
                name: "IX_Fav_IdUsuario",
                table: "Fav",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Lista_IdUsuario",
                table: "Lista",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Resena_IdPelicula",
                table: "Resena",
                column: "IdPelicula");

            migrationBuilder.CreateIndex(
                name: "IX_Resena_IdUsuario",
                table: "Resena",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Ver_IdPelicula",
                table: "Ver",
                column: "IdPelicula");

            migrationBuilder.CreateIndex(
                name: "IX_Ver_IdUsuario",
                table: "Ver",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contiene");

            migrationBuilder.DropTable(
                name: "Fav");

            migrationBuilder.DropTable(
                name: "Resena");

            migrationBuilder.DropTable(
                name: "Ver");

            migrationBuilder.DropTable(
                name: "Lista");

            migrationBuilder.DropTable(
                name: "Pelicula");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
