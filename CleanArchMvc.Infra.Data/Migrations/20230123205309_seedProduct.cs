using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel;

#nullable disable

namespace CleanArchMvc.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Name", "Description", "Price", "Stock", "Image", "CategoryId" },
                values: new object[,]
                {
                    { "Caderno espiral", "Caderno espiral 100 fls", 7.45, 50, "caderno1.jpg", 1 },
                    { "Estojo escolar", "Estojo escolar cinza", 5.65, 70, "estejo1.jpg", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from products");
        }
    }
}
