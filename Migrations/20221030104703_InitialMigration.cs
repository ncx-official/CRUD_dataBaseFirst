using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseManagerApplication.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "class",
                columns: table => new
                {
                    id_class = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    class_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_class);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "operation_type",
                columns: table => new
                {
                    id_operation_type = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    type_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_operation_type);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "person_sex",
                columns: table => new
                {
                    id_sex = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sex_value = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_sex);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "store_country",
                columns: table => new
                {
                    id_store_country = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    country_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_store_country);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "store_phone",
                columns: table => new
                {
                    id_store_phone = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    phone_value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_store_phone);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "week_day",
                columns: table => new
                {
                    id_week_day = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_week_day);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "work_position",
                columns: table => new
                {
                    id_work_position = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    position_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_work_position);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "person",
                columns: table => new
                {
                    id_person = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    firstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    middleName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_sex = table.Column<uint>(type: "int unsigned", nullable: false),
                    birthdate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_person);
                    table.ForeignKey(
                        name: "person_ibfk_1",
                        column: x => x.id_sex,
                        principalTable: "person_sex",
                        principalColumn: "id_sex");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "store_city",
                columns: table => new
                {
                    id_store_city = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_store_country = table.Column<long>(type: "bigint", nullable: false),
                    city_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_store_city);
                    table.ForeignKey(
                        name: "store_city_ibfk_1",
                        column: x => x.id_store_country,
                        principalTable: "store_country",
                        principalColumn: "id_store_country");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "schedule_open",
                columns: table => new
                {
                    id_schedule_open = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_week_day = table.Column<long>(type: "bigint", nullable: false),
                    open_at = table.Column<TimeOnly>(type: "time", nullable: false),
                    close_at = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_schedule_open);
                    table.ForeignKey(
                        name: "schedule_open_ibfk_1",
                        column: x => x.id_week_day,
                        principalTable: "week_day",
                        principalColumn: "id_week_day");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "authorization",
                columns: table => new
                {
                    id_person = table.Column<long>(type: "bigint", nullable: false),
                    login = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_person);
                    table.ForeignKey(
                        name: "authorization_ibfk_1",
                        column: x => x.id_person,
                        principalTable: "person",
                        principalColumn: "id_person");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "store_address",
                columns: table => new
                {
                    id_store_address = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_store_city = table.Column<long>(type: "bigint", nullable: false),
                    street_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    street_number_code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_store_address);
                    table.ForeignKey(
                        name: "store_address_ibfk_1",
                        column: x => x.id_store_city,
                        principalTable: "store_city",
                        principalColumn: "id_store_city");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "store",
                columns: table => new
                {
                    id_store = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_schedule_open = table.Column<long>(type: "bigint", nullable: false),
                    store_name = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_store_address = table.Column<long>(type: "bigint", nullable: false),
                    id_store_phone = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_store);
                    table.ForeignKey(
                        name: "store_ibfk_1",
                        column: x => x.id_store_address,
                        principalTable: "store_address",
                        principalColumn: "id_store_address");
                    table.ForeignKey(
                        name: "store_ibfk_2",
                        column: x => x.id_store_phone,
                        principalTable: "store_phone",
                        principalColumn: "id_store_phone");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    id_employee = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_person = table.Column<long>(type: "bigint", nullable: false),
                    id_store = table.Column<long>(type: "bigint", nullable: true),
                    id_work_position = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_employee);
                    table.ForeignKey(
                        name: "employee_ibfk_1",
                        column: x => x.id_person,
                        principalTable: "person",
                        principalColumn: "id_person");
                    table.ForeignKey(
                        name: "employee_ibfk_2",
                        column: x => x.id_store,
                        principalTable: "store",
                        principalColumn: "id_store");
                    table.ForeignKey(
                        name: "employee_ibfk_3",
                        column: x => x.id_work_position,
                        principalTable: "work_position",
                        principalColumn: "id_work_position");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id_product = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_store = table.Column<long>(type: "bigint", nullable: true),
                    id_class = table.Column<long>(type: "bigint", nullable: true),
                    usd_price = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    define_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isAvailable = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    count = table.Column<uint>(type: "int unsigned", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_product);
                    table.ForeignKey(
                        name: "product_ibfk_1",
                        column: x => x.id_class,
                        principalTable: "class",
                        principalColumn: "id_class");
                    table.ForeignKey(
                        name: "product_ibfk_2",
                        column: x => x.id_store,
                        principalTable: "store",
                        principalColumn: "id_store");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "store_schedule_open",
                columns: table => new
                {
                    id_schedule_open = table.Column<long>(type: "bigint", nullable: false),
                    id_store = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.id_schedule_open, x.id_store })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "store_schedule_open_ibfk_1",
                        column: x => x.id_schedule_open,
                        principalTable: "schedule_open",
                        principalColumn: "id_schedule_open");
                    table.ForeignKey(
                        name: "store_schedule_open_ibfk_2",
                        column: x => x.id_store,
                        principalTable: "store",
                        principalColumn: "id_store");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "operation",
                columns: table => new
                {
                    id_operation = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_operation_type = table.Column<long>(type: "bigint", nullable: false),
                    id_employee = table.Column<long>(type: "bigint", nullable: true),
                    id_product = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_operation);
                    table.ForeignKey(
                        name: "operation_ibfk_1",
                        column: x => x.id_operation_type,
                        principalTable: "operation_type",
                        principalColumn: "id_operation_type");
                    table.ForeignKey(
                        name: "operation_ibfk_2",
                        column: x => x.id_employee,
                        principalTable: "employee",
                        principalColumn: "id_employee");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "operation_product",
                columns: table => new
                {
                    id_operation = table.Column<long>(type: "bigint", nullable: false),
                    id_product = table.Column<long>(type: "bigint", nullable: false),
                    operation_product_price = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    operation_product_count = table.Column<uint>(type: "int unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.id_operation, x.id_product })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "operation_product_ibfk_1",
                        column: x => x.id_operation,
                        principalTable: "operation",
                        principalColumn: "id_operation");
                    table.ForeignKey(
                        name: "operation_product_ibfk_2",
                        column: x => x.id_product,
                        principalTable: "product",
                        principalColumn: "id_product");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "id_person",
                table: "employee",
                column: "id_person");

            migrationBuilder.CreateIndex(
                name: "id_store",
                table: "employee",
                column: "id_store");

            migrationBuilder.CreateIndex(
                name: "id_work_position",
                table: "employee",
                column: "id_work_position");

            migrationBuilder.CreateIndex(
                name: "id_employee",
                table: "operation",
                column: "id_employee");

            migrationBuilder.CreateIndex(
                name: "id_operation_type",
                table: "operation",
                column: "id_operation_type");

            migrationBuilder.CreateIndex(
                name: "id_product",
                table: "operation_product",
                column: "id_product");

            migrationBuilder.CreateIndex(
                name: "type_name",
                table: "operation_type",
                column: "type_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_sex",
                table: "person",
                column: "id_sex");

            migrationBuilder.CreateIndex(
                name: "sex_value",
                table: "person_sex",
                column: "sex_value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_class",
                table: "product",
                column: "id_class");

            migrationBuilder.CreateIndex(
                name: "id_store1",
                table: "product",
                column: "id_store");

            migrationBuilder.CreateIndex(
                name: "id_week_day",
                table: "schedule_open",
                column: "id_week_day");

            migrationBuilder.CreateIndex(
                name: "id_store_address",
                table: "store",
                column: "id_store_address");

            migrationBuilder.CreateIndex(
                name: "id_store_phone",
                table: "store",
                column: "id_store_phone");

            migrationBuilder.CreateIndex(
                name: "id_store_city",
                table: "store_address",
                column: "id_store_city");

            migrationBuilder.CreateIndex(
                name: "id_store_country",
                table: "store_city",
                column: "id_store_country");

            migrationBuilder.CreateIndex(
                name: "phone_value",
                table: "store_phone",
                column: "phone_value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_store2",
                table: "store_schedule_open",
                column: "id_store");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "authorization");

            migrationBuilder.DropTable(
                name: "operation_product");

            migrationBuilder.DropTable(
                name: "store_schedule_open");

            migrationBuilder.DropTable(
                name: "operation");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "schedule_open");

            migrationBuilder.DropTable(
                name: "operation_type");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "class");

            migrationBuilder.DropTable(
                name: "week_day");

            migrationBuilder.DropTable(
                name: "person");

            migrationBuilder.DropTable(
                name: "store");

            migrationBuilder.DropTable(
                name: "work_position");

            migrationBuilder.DropTable(
                name: "person_sex");

            migrationBuilder.DropTable(
                name: "store_address");

            migrationBuilder.DropTable(
                name: "store_phone");

            migrationBuilder.DropTable(
                name: "store_city");

            migrationBuilder.DropTable(
                name: "store_country");
        }
    }
}
