using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeedData.Migrations
{
    /// <inheritdoc />
    public partial class initialCreateMysqlV2Design : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Persons_person_id",
                table: "Writers",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "Titles_title_id",
                table: "Writers",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "writers_id",
                table: "Writers",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid())",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid_to_bin(uuid(),1))")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "type_id",
                table: "Types",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid())",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid_to_bin(uuid(),1))")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "Genres_genre_id",
                table: "Titles_has_Genres",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "Titles_title_id",
                table: "Titles_has_Genres",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "title_id",
                table: "Titles",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid())",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid_to_bin(uuid(),1))")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "title_id",
                table: "Ratings",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "rating_id",
                table: "Ratings",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid())",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid_to_bin(uuid(),1))")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "person_id",
                table: "Professions",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "profession_id",
                table: "Professions",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid())",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid_to_bin(uuid(),1))")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "person_id",
                table: "Persons",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid())",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid_to_bin(uuid(),1))")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "table_name",
                table: "Loggings",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "logging_id",
                table: "Loggings",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid_to_bin(uuid(),1))")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "Persons_person_id",
                table: "Known_for",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "Titles_title_id",
                table: "Known_for",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "known_for_id",
                table: "Known_for",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid())",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid_to_bin(uuid(),1))")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "genre_id",
                table: "Genres",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid())",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid_to_bin(uuid(),1))")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "title_id_parent",
                table: "Episodes",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "title_id_child",
                table: "Episodes",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "episode_id",
                table: "Episodes",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid())",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid_to_bin(uuid(),1))")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "Persons_person_id",
                table: "Directors",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "Titles_title_id",
                table: "Directors",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "directors_id",
                table: "Directors",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid())",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid_to_bin(uuid(),1))")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "title_id",
                table: "Comments",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "comment_id",
                table: "Comments",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid())",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid_to_bin(uuid(),1))")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "attribute_id",
                table: "Attributes",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid())",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid_to_bin(uuid(),1))")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "Types_type_id",
                table: "Aliases_has_Types",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "Aliases_alias_id",
                table: "Aliases_has_Types",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "Attributes_attribute_id",
                table: "Aliases_has_Attributes",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "Aliases_alias_id",
                table: "Aliases_has_Attributes",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "title_id",
                table: "Aliases",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "alias_id",
                table: "Aliases",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid())",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid_to_bin(uuid(),1))")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "Persons_person_id",
                table: "Actors",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "Titles_title_id",
                table: "Actors",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "actor_id",
                table: "Actors",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid())",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid_to_bin(uuid(),1))")
                .Annotation("MySql:CharSet", "ascii")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "idx_Titles_primary_title_original_title",
                table: "Titles",
                columns: new[] { "primary_title", "original_title" })
                .Annotation("MySql:FullTextIndex", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_Titles_primary_title_original_title",
                table: "Titles");

            migrationBuilder.AlterColumn<Guid>(
                name: "Persons_person_id",
                table: "Writers",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "Titles_title_id",
                table: "Writers",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "writers_id",
                table: "Writers",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid_to_bin(uuid(),1))",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid())",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "type_id",
                table: "Types",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid_to_bin(uuid(),1))",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid())",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "Genres_genre_id",
                table: "Titles_has_Genres",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "Titles_title_id",
                table: "Titles_has_Genres",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "title_id",
                table: "Titles",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid_to_bin(uuid(),1))",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid())",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "title_id",
                table: "Ratings",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "rating_id",
                table: "Ratings",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid_to_bin(uuid(),1))",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid())",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "person_id",
                table: "Professions",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "profession_id",
                table: "Professions",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid_to_bin(uuid(),1))",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid())",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "person_id",
                table: "Persons",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid_to_bin(uuid(),1))",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid())",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "table_name",
                table: "Loggings",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "logging_id",
                table: "Loggings",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid_to_bin(uuid(),1))",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "Persons_person_id",
                table: "Known_for",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "Titles_title_id",
                table: "Known_for",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "known_for_id",
                table: "Known_for",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid_to_bin(uuid(),1))",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid())",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "genre_id",
                table: "Genres",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid_to_bin(uuid(),1))",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid())",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "title_id_parent",
                table: "Episodes",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "title_id_child",
                table: "Episodes",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "episode_id",
                table: "Episodes",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid_to_bin(uuid(),1))",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid())",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "Persons_person_id",
                table: "Directors",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "Titles_title_id",
                table: "Directors",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "directors_id",
                table: "Directors",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid_to_bin(uuid(),1))",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid())",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "title_id",
                table: "Comments",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "comment_id",
                table: "Comments",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid_to_bin(uuid(),1))",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid())",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "attribute_id",
                table: "Attributes",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid_to_bin(uuid(),1))",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid())",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "Types_type_id",
                table: "Aliases_has_Types",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "Aliases_alias_id",
                table: "Aliases_has_Types",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "Attributes_attribute_id",
                table: "Aliases_has_Attributes",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "Aliases_alias_id",
                table: "Aliases_has_Attributes",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "title_id",
                table: "Aliases",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "alias_id",
                table: "Aliases",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid_to_bin(uuid(),1))",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid())",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "Persons_person_id",
                table: "Actors",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "Titles_title_id",
                table: "Actors",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");

            migrationBuilder.AlterColumn<Guid>(
                name: "actor_id",
                table: "Actors",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(uuid_to_bin(uuid(),1))",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(uuid())",
                oldCollation: "ascii_general_ci")
                .OldAnnotation("MySql:CharSet", "ascii");
        }
    }
}
