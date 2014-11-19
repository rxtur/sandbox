using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Model;
using System;

namespace Sample.Migrations
{
    public partial class bf01 : Migration
    {
        public override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("Blog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String()
                    })
                .PrimaryKey("PK_Blog", t => t.Id);
            
            migrationBuilder.CreateTable("Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlogId = c.Int(nullable: false),
                        Comments = c.Int(nullable: false),
                        Content = c.String(),
                        Created = c.DateTime(nullable: false),
                        Published = c.Boolean(nullable: false),
                        Title = c.String()
                    })
                .PrimaryKey("PK_Post", t => t.Id);
        }
        
        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Blog");
            
            migrationBuilder.DropTable("Post");
        }
    }
}