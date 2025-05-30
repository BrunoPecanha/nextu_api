using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UFF.Infra.Migrations
{
    /// <inheritdoc />
    public partial class inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    img_path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    icon = table.Column<string>(type: "varchar(15)", maxLength: 255, nullable: true),
                    registering_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'"),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    img_path = table.Column<string>(type: "text", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    icon = table.Column<string>(type: "text", nullable: true),
                    registering_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "service_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    img_path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    icon = table.Column<string>(type: "varchar(15)", maxLength: 255, nullable: true),
                    registering_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'"),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_service_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    city = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    state_id = table.Column<string>(type: "char(2)", maxLength: 2, nullable: true),
                    cpf = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    status = table.Column<string>(type: "varchar(20)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    profile = table.Column<int>(type: "integer", nullable: false),
                    subtitle = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    services_provided = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    accept_aways_minor_queue = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    registering_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'"),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stores",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cnpj = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    city = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    state = table.Column<string>(type: "char(2)", maxLength: 2, nullable: false),
                    open_automatic = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    attend_simultaneously = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    store_subtitle = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    accept_other_queues = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    answer_out_of_order = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    answer_scheduled_time = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    time_removal = table.Column<int>(type: "integer", nullable: true),
                    whatsapp_notice = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    logo_path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    wallpaper_path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    rating = table.Column<decimal>(type: "numeric(3,2)", nullable: false, defaultValue: 0m),
                    votes = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    instagram = table.Column<string>(type: "text", nullable: true),
                    facebook = table.Column<string>(type: "text", nullable: true),
                    youtube = table.Column<string>(type: "text", nullable: true),
                    site = table.Column<string>(type: "text", nullable: true),
                    owner_id = table.Column<int>(type: "integer", nullable: false),
                    verified = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    status = table.Column<string>(type: "varchar(20)", nullable: false),
                    registering_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'"),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stores", x => x.id);
                    table.ForeignKey(
                        name: "fk_stores_category",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stores_owner",
                        column: x => x.owner_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "employee_stores",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    store_id = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    request_answered = table.Column<bool>(type: "boolean", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    registering_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'"),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employee_stores", x => new { x.user_id, x.store_id });
                    table.ForeignKey(
                        name: "fk_employee_stores_stores",
                        column: x => x.store_id,
                        principalTable: "stores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_employee_stores_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "highlights",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    phrase = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    icon = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    activated = table.Column<bool>(type: "boolean", nullable: false),
                    store_id = table.Column<int>(type: "integer", nullable: false),
                    registering_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'"),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_highlights", x => x.id);
                    table.ForeignKey(
                        name: "fk_highlights_store",
                        column: x => x.store_id,
                        principalTable: "stores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "opening_hours",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    week_day = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    start_time = table.Column<TimeSpan>(type: "time without time zone", nullable: true),
                    end_time = table.Column<TimeSpan>(type: "time without time zone", nullable: true),
                    activated = table.Column<bool>(type: "boolean", nullable: false),
                    store_id = table.Column<int>(type: "integer", nullable: false),
                    registering_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'"),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_opening_hours", x => x.id);
                    table.ForeignKey(
                        name: "fk_opening_hours_store",
                        column: x => x.store_id,
                        principalTable: "stores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "queues",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    store_id = table.Column<int>(type: "integer", nullable: false),
                    employee_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'"),
                    closing_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", nullable: false),
                    pause_reason = table.Column<string>(type: "varchar(40)", nullable: true),
                    close_reason = table.Column<string>(type: "varchar(40)", nullable: true),
                    registering_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'"),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_queues", x => x.id);
                    table.ForeignKey(
                        name: "fk_queues_stores",
                        column: x => x.store_id,
                        principalTable: "stores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_queues_users",
                        column: x => x.employee_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    store_id = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    img_path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    variable_time = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    variable_price = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    activated = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    registering_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'"),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_services", x => x.id);
                    table.ForeignKey(
                        name: "fk_services_category",
                        column: x => x.category_id,
                        principalTable: "service_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_services_store",
                        column: x => x.store_id,
                        principalTable: "stores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    queue_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    payment_id = table.Column<int>(type: "integer", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    rating = table.Column<int>(type: "integer", nullable: true),
                    review = table.Column<string>(type: "text", nullable: true),
                    position = table.Column<int>(type: "integer", nullable: false),
                    estimated_start_time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    time_entered_queue = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'"),
                    time_called_in_queue = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    missing_customer_removal_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    service_start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    service_end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<string>(type: "varchar(30)", nullable: false),
                    random_customer_name = table.Column<string>(type: "varchar(30)", nullable: true),
                    is_priority = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    remove_reason = table.Column<string>(type: "text", nullable: true),
                    registering_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'"),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customers", x => x.id);
                    table.ForeignKey(
                        name: "fk_customers_payments",
                        column: x => x.payment_id,
                        principalTable: "payment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_customers_queues",
                        column: x => x.queue_id,
                        principalTable: "queues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_customers_user",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "customer_services",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    service_id = table.Column<int>(type: "integer", nullable: false),
                    queue_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    final_price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false),
                    registering_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer_services", x => new { x.customer_id, x.service_id, x.queue_id });
                    table.ForeignKey(
                        name: "fk_customer_services_customers",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_customer_services_queues",
                        column: x => x.queue_id,
                        principalTable: "queues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_customer_services_services",
                        column: x => x.service_id,
                        principalTable: "services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_categories_name",
                table: "categories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_customer_services_customer_id",
                table: "customer_services",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_customer_services_queue_id",
                table: "customer_services",
                column: "queue_id");

            migrationBuilder.CreateIndex(
                name: "ix_customer_services_service_id",
                table: "customer_services",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "ix_customers_payment_id",
                table: "customers",
                column: "payment_id");

            migrationBuilder.CreateIndex(
                name: "ix_customers_queue_id",
                table: "customers",
                column: "queue_id");

            migrationBuilder.CreateIndex(
                name: "ix_customers_rating",
                table: "customers",
                column: "rating",
                filter: "rating IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_customers_user_id",
                table: "customers",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_queue_customers_position",
                table: "customers",
                column: "position");

            migrationBuilder.CreateIndex(
                name: "ix_queue_customers_queue_position",
                table: "customers",
                columns: new[] { "queue_id", "position" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "ix_queue_customers_status",
                table: "customers",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_queue_customers_time_entered",
                table: "customers",
                column: "time_entered_queue");

            migrationBuilder.CreateIndex(
                name: "ix_employee_stores_is_active",
                table: "employee_stores",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "ix_employee_stores_registering_date",
                table: "employee_stores",
                column: "registering_date");

            migrationBuilder.CreateIndex(
                name: "ix_employee_stores_store_id",
                table: "employee_stores",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "ix_employee_stores_user_id",
                table: "employee_stores",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_highlights_registering_date",
                table: "highlights",
                column: "registering_date");

            migrationBuilder.CreateIndex(
                name: "ix_highlights_store_id",
                table: "highlights",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "ix_opening_hours_store_id",
                table: "opening_hours",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "ix_opening_hours_week_day",
                table: "opening_hours",
                column: "week_day");

            migrationBuilder.CreateIndex(
                name: "ix_opening_hours_week_schedule",
                table: "opening_hours",
                columns: new[] { "week_day", "start_time", "end_time" });

            migrationBuilder.CreateIndex(
                name: "ix_queues_date",
                table: "queues",
                column: "date");

            migrationBuilder.CreateIndex(
                name: "IX_queues_employee_id",
                table: "queues",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "ix_queues_status",
                table: "queues",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_queues_store_id",
                table: "queues",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "ix_queues_store_status",
                table: "queues",
                columns: new[] { "store_id", "status" });

            migrationBuilder.CreateIndex(
                name: "ix_service_categories_img_path",
                table: "service_categories",
                column: "img_path",
                filter: "img_path IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_service_categories_name",
                table: "service_categories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_services_activated",
                table: "services",
                column: "activated");

            migrationBuilder.CreateIndex(
                name: "ix_services_category_id",
                table: "services",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_services_name",
                table: "services",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_services_store_active",
                table: "services",
                columns: new[] { "store_id", "activated" });

            migrationBuilder.CreateIndex(
                name: "ix_services_store_id",
                table: "services",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "ix_stores_category_id",
                table: "stores",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_stores_cnpj",
                table: "stores",
                column: "cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_stores_location",
                table: "stores",
                columns: new[] { "state", "city" });

            migrationBuilder.CreateIndex(
                name: "ix_stores_name",
                table: "stores",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_stores_owner_id",
                table: "stores",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "ix_stores_status",
                table: "stores",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_users_cpf",
                table: "users",
                column: "cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_location",
                table: "users",
                columns: new[] { "state_id", "city" });

            migrationBuilder.CreateIndex(
                name: "ix_users_status",
                table: "users",
                column: "status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer_services");

            migrationBuilder.DropTable(
                name: "employee_stores");

            migrationBuilder.DropTable(
                name: "highlights");

            migrationBuilder.DropTable(
                name: "opening_hours");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "services");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "queues");

            migrationBuilder.DropTable(
                name: "service_categories");

            migrationBuilder.DropTable(
                name: "stores");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
