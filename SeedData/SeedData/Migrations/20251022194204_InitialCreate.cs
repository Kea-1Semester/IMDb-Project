using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeedData.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    attribute_id = table.Column<Guid>(type: "char(36)", nullable: false, defaultValueSql: "(uuid_to_bin(uuid(),1))", collation: "ascii_general_ci"),
                    attribute = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.attribute_id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    genre_id = table.Column<Guid>(type: "char(36)", nullable: false, defaultValueSql: "(uuid_to_bin(uuid(),1))", collation: "ascii_general_ci"),
                    genre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.genre_id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Loggings",
                columns: table => new
                {
                    logging_id = table.Column<Guid>(type: "char(36)", nullable: false, defaultValueSql: "(uuid_to_bin(uuid(),1))", collation: "ascii_general_ci"),
                    table_name = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    command = table.Column<string>(type: "enum('INSERT','UPDATE','DELETE')", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    new_value = table.Column<string>(type: "json", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    old_value = table.Column<string>(type: "json", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    executed_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    executed_at = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.logging_id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    person_id = table.Column<Guid>(type: "char(36)", nullable: false, defaultValueSql: "(uuid_to_bin(uuid(),1))", collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    birth_year = table.Column<int>(type: "int", nullable: false),
                    end_year = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.person_id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    title_id = table.Column<Guid>(type: "char(36)", nullable: false, defaultValueSql: "(uuid_to_bin(uuid(),1))", collation: "ascii_general_ci"),
                    title_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    primary_title = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    original_title = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    is_adult = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    start_year = table.Column<int>(type: "int", nullable: false),
                    end_year = table.Column<int>(type: "int", nullable: true),
                    runtime_minutes = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.title_id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    type_id = table.Column<Guid>(type: "char(36)", nullable: false, defaultValueSql: "(uuid_to_bin(uuid(),1))", collation: "ascii_general_ci"),
                    type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.type_id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Professions",
                columns: table => new
                {
                    profession_id = table.Column<Guid>(type: "char(36)", nullable: false, defaultValueSql: "(uuid_to_bin(uuid(),1))", collation: "ascii_general_ci"),
                    person_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    profession = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.profession_id);
                    table.ForeignKey(
                        name: "fk_Professions_Persons1",
                        column: x => x.person_id,
                        principalTable: "Persons",
                        principalColumn: "person_id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    actor_id = table.Column<Guid>(type: "char(36)", nullable: false, defaultValueSql: "(uuid_to_bin(uuid(),1))", collation: "ascii_general_ci"),
                    Titles_title_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Persons_person_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Role = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.actor_id, x.Titles_title_id, x.Persons_person_id })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });
                    table.ForeignKey(
                        name: "fk_Titles_has_Persons_Persons3",
                        column: x => x.Persons_person_id,
                        principalTable: "Persons",
                        principalColumn: "person_id");
                    table.ForeignKey(
                        name: "fk_Titles_has_Persons_Titles3",
                        column: x => x.Titles_title_id,
                        principalTable: "Titles",
                        principalColumn: "title_id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Aliases",
                columns: table => new
                {
                    alias_id = table.Column<Guid>(type: "char(36)", nullable: false, defaultValueSql: "(uuid_to_bin(uuid(),1))", collation: "ascii_general_ci"),
                    title_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    region = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    language = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    is_original_title = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    title = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.alias_id);
                    table.ForeignKey(
                        name: "fk_title_akas_title_basics",
                        column: x => x.title_id,
                        principalTable: "Titles",
                        principalColumn: "title_id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    comment_id = table.Column<Guid>(type: "char(36)", nullable: false, defaultValueSql: "(uuid_to_bin(uuid(),1))", collation: "ascii_general_ci"),
                    title_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    comment = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.comment_id);
                    table.ForeignKey(
                        name: "fk_title_comments_title_basics1",
                        column: x => x.title_id,
                        principalTable: "Titles",
                        principalColumn: "title_id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    directors_id = table.Column<Guid>(type: "char(36)", nullable: false, defaultValueSql: "(uuid_to_bin(uuid(),1))", collation: "ascii_general_ci"),
                    Titles_title_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Persons_person_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.directors_id, x.Titles_title_id, x.Persons_person_id })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });
                    table.ForeignKey(
                        name: "fk_Titles_has_Persons_Persons1",
                        column: x => x.Persons_person_id,
                        principalTable: "Persons",
                        principalColumn: "person_id");
                    table.ForeignKey(
                        name: "fk_Titles_has_Persons_Titles1",
                        column: x => x.Titles_title_id,
                        principalTable: "Titles",
                        principalColumn: "title_id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    episode_id = table.Column<Guid>(type: "char(36)", nullable: false, defaultValueSql: "(uuid_to_bin(uuid(),1))", collation: "ascii_general_ci"),
                    title_id_parent = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    title_id_child = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    season_number = table.Column<int>(type: "int", nullable: false),
                    episode_number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.episode_id);
                    table.ForeignKey(
                        name: "fk_title_episodes_title_basics1",
                        column: x => x.title_id_parent,
                        principalTable: "Titles",
                        principalColumn: "title_id");
                    table.ForeignKey(
                        name: "fk_title_episodes_title_basics2",
                        column: x => x.title_id_child,
                        principalTable: "Titles",
                        principalColumn: "title_id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Known_for",
                columns: table => new
                {
                    known_for_id = table.Column<Guid>(type: "char(36)", nullable: false, defaultValueSql: "(uuid_to_bin(uuid(),1))", collation: "ascii_general_ci"),
                    Titles_title_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Persons_person_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.known_for_id, x.Titles_title_id, x.Persons_person_id })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });
                    table.ForeignKey(
                        name: "fk_Titles_has_Persons_Persons2",
                        column: x => x.Persons_person_id,
                        principalTable: "Persons",
                        principalColumn: "person_id");
                    table.ForeignKey(
                        name: "fk_Titles_has_Persons_Titles2",
                        column: x => x.Titles_title_id,
                        principalTable: "Titles",
                        principalColumn: "title_id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    rating_id = table.Column<Guid>(type: "char(36)", nullable: false, defaultValueSql: "(uuid_to_bin(uuid(),1))", collation: "ascii_general_ci"),
                    title_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    average_rating = table.Column<double>(type: "double", nullable: false),
                    num_votes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.rating_id);
                    table.ForeignKey(
                        name: "fk_title_ratings_title_basics1",
                        column: x => x.title_id,
                        principalTable: "Titles",
                        principalColumn: "title_id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Titles_has_Genres",
                columns: table => new
                {
                    Titles_title_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Genres_genre_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.Titles_title_id, x.Genres_genre_id })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_Titles_has_Genres_Genres1",
                        column: x => x.Genres_genre_id,
                        principalTable: "Genres",
                        principalColumn: "genre_id");
                    table.ForeignKey(
                        name: "fk_Titles_has_Genres_Titles1",
                        column: x => x.Titles_title_id,
                        principalTable: "Titles",
                        principalColumn: "title_id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Writers",
                columns: table => new
                {
                    writers_id = table.Column<Guid>(type: "char(36)", nullable: false, defaultValueSql: "(uuid_to_bin(uuid(),1))", collation: "ascii_general_ci"),
                    Titles_title_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Persons_person_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.writers_id, x.Titles_title_id, x.Persons_person_id })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });
                    table.ForeignKey(
                        name: "fk_Titles_has_Persons_Persons4",
                        column: x => x.Persons_person_id,
                        principalTable: "Persons",
                        principalColumn: "person_id");
                    table.ForeignKey(
                        name: "fk_Titles_has_Persons_Titles4",
                        column: x => x.Titles_title_id,
                        principalTable: "Titles",
                        principalColumn: "title_id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Aliases_has_Attributes",
                columns: table => new
                {
                    Aliases_alias_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Attributes_attribute_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.Aliases_alias_id, x.Attributes_attribute_id })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_Aliases_has_Attributes_Aliases1",
                        column: x => x.Aliases_alias_id,
                        principalTable: "Aliases",
                        principalColumn: "alias_id");
                    table.ForeignKey(
                        name: "fk_Aliases_has_Attributes_Attributes1",
                        column: x => x.Attributes_attribute_id,
                        principalTable: "Attributes",
                        principalColumn: "attribute_id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Aliases_has_Types",
                columns: table => new
                {
                    Aliases_alias_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Types_type_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.Aliases_alias_id, x.Types_type_id })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_Aliases_has_Types_Aliases1",
                        column: x => x.Aliases_alias_id,
                        principalTable: "Aliases",
                        principalColumn: "alias_id");
                    table.ForeignKey(
                        name: "fk_Aliases_has_Types_Types1",
                        column: x => x.Types_type_id,
                        principalTable: "Types",
                        principalColumn: "type_id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateIndex(
                name: "fk_Titles_has_Persons_Persons3_idx",
                table: "Actors",
                column: "Persons_person_id");

            migrationBuilder.CreateIndex(
                name: "fk_Titles_has_Persons_Titles3_idx",
                table: "Actors",
                column: "Titles_title_id");

            migrationBuilder.CreateIndex(
                name: "fk_title_akas_title_basics_idx",
                table: "Aliases",
                column: "title_id");

            migrationBuilder.CreateIndex(
                name: "title_index",
                table: "Aliases",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "fk_Aliases_has_Attributes_Aliases1_idx",
                table: "Aliases_has_Attributes",
                column: "Aliases_alias_id");

            migrationBuilder.CreateIndex(
                name: "fk_Aliases_has_Attributes_Attributes1_idx",
                table: "Aliases_has_Attributes",
                column: "Attributes_attribute_id");

            migrationBuilder.CreateIndex(
                name: "fk_Aliases_has_Types_Aliases1_idx",
                table: "Aliases_has_Types",
                column: "Aliases_alias_id");

            migrationBuilder.CreateIndex(
                name: "fk_Aliases_has_Types_Types1_idx",
                table: "Aliases_has_Types",
                column: "Types_type_id");

            migrationBuilder.CreateIndex(
                name: "attribute_UNIQUE",
                table: "Attributes",
                column: "attribute",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_title_comments_title_basics1_idx",
                table: "Comments",
                column: "title_id");

            migrationBuilder.CreateIndex(
                name: "fk_Titles_has_Persons_Persons1_idx",
                table: "Directors",
                column: "Persons_person_id");

            migrationBuilder.CreateIndex(
                name: "fk_Titles_has_Persons_Titles1_idx",
                table: "Directors",
                column: "Titles_title_id");

            migrationBuilder.CreateIndex(
                name: "fk_title_episodes_title_basics1",
                table: "Episodes",
                column: "title_id_parent");

            migrationBuilder.CreateIndex(
                name: "fk_title_episodes_title_basics2_idx",
                table: "Episodes",
                column: "title_id_child");

            migrationBuilder.CreateIndex(
                name: "genre_UNIQUE",
                table: "Genres",
                column: "genre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_Titles_has_Persons_Persons2_idx",
                table: "Known_for",
                column: "Persons_person_id");

            migrationBuilder.CreateIndex(
                name: "fk_Titles_has_Persons_Titles2_idx",
                table: "Known_for",
                column: "Titles_title_id");

            migrationBuilder.CreateIndex(
                name: "executed_at_index",
                table: "Loggings",
                column: "executed_at");

            migrationBuilder.CreateIndex(
                name: "table_name_index",
                table: "Loggings",
                column: "table_name");

            migrationBuilder.CreateIndex(
                name: "fk_Professions_Persons1_idx",
                table: "Professions",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "profession_UNIQUE",
                table: "Professions",
                column: "profession",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_title_ratings_title_basics1",
                table: "Ratings",
                column: "title_id");

            migrationBuilder.CreateIndex(
                name: "original_title_index",
                table: "Titles",
                column: "original_title");

            migrationBuilder.CreateIndex(
                name: "primary_title_index",
                table: "Titles",
                column: "primary_title");

            migrationBuilder.CreateIndex(
                name: "title_type_index",
                table: "Titles",
                column: "title_type");

            migrationBuilder.CreateIndex(
                name: "fk_Titles_has_Genres_Genres1_idx",
                table: "Titles_has_Genres",
                column: "Genres_genre_id");

            migrationBuilder.CreateIndex(
                name: "fk_Titles_has_Genres_Titles1_idx",
                table: "Titles_has_Genres",
                column: "Titles_title_id");

            migrationBuilder.CreateIndex(
                name: "type_UNIQUE",
                table: "Types",
                column: "type",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_Titles_has_Persons_Persons4_idx",
                table: "Writers",
                column: "Persons_person_id");

            migrationBuilder.CreateIndex(
                name: "fk_Titles_has_Persons_Titles4_idx",
                table: "Writers",
                column: "Titles_title_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Aliases_has_Attributes");

            migrationBuilder.DropTable(
                name: "Aliases_has_Types");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Directors");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Known_for");

            migrationBuilder.DropTable(
                name: "Loggings");

            migrationBuilder.DropTable(
                name: "Professions");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Titles_has_Genres");

            migrationBuilder.DropTable(
                name: "Writers");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "Aliases");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Titles");
        }
    }
}
