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
            migrationBuilder.CreateTable("bf_blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorEmail = c.String(),
                        AuthorId = c.String(),
                        AuthorName = c.String(),
                        DaysToComment = c.Int(nullable: false),
                        Description = c.String(),
                        Image = c.String(),
                        IsModerated = c.Boolean(nullable: false),
                        IsSelected = c.Boolean(nullable: false),
                        PostsPerPage = c.Int(nullable: false),
                        Theme = c.String(),
                        Title = c.String()
                    })
                .PrimaryKey("PK_bf_blogs", t => t.Id);
            
            migrationBuilder.CreateTable("bf_posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorName = c.String(),
                        BlogId = c.Int(nullable: false),
                        Comments = c.Int(nullable: false),
                        CommentsEnabled = c.Boolean(nullable: false),
                        Content = c.String(),
                        IsSelected = c.Boolean(nullable: false),
                        Published = c.DateTime(nullable: false),
                        Saved = c.DateTime(nullable: false),
                        Slug = c.String(),
                        Tags = c.String(),
                        Title = c.String()
                    })
                .PrimaryKey("PK_bf_posts", t => t.Id);
            
            migrationBuilder.CreateTable("bf_settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlogId = c.Int(nullable: false),
                        SettingKey = c.String(),
                        SettingValue = c.String()
                    })
                .PrimaryKey("PK_bf_settings", t => t.Id);
        }
        
        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("bf_blogs");
            
            migrationBuilder.DropTable("bf_posts");
            
            migrationBuilder.DropTable("bf_settings");
        }
    }
}