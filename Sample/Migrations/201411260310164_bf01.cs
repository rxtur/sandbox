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
                        Author = c.String(),
                        Description = c.String(),
                        Name = c.String()
                    })
                .PrimaryKey("PK_Blog", t => t.Id);
            
            migrationBuilder.CreateTable("Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlogId = c.Int(nullable: false),
                        Comments = c.Int(nullable: false),
                        CommentsEnabled = c.Boolean(nullable: false),
                        Content = c.String(),
                        Published = c.DateTime(nullable: false),
                        Saved = c.DateTime(nullable: false),
                        Slug = c.String(),
                        Tags = c.String(),
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