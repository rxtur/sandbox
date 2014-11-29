using BlogiFire.Models;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using System;

namespace Sample.Migrations
{
    [ContextType(typeof(BlogContext))]
    public class BlogContextModelSnapshot : ModelSnapshot
    {
        public override IModel Model
        {
            get
            {
                var builder = new BasicModelBuilder();
                
                builder.Entity("BlogiFire.Models.Blog", b =>
                    {
                        b.Property<string>("AuthorEmail");
                        b.Property<string>("AuthorId");
                        b.Property<string>("AuthorName");
                        b.Property<int>("DaysToComment");
                        b.Property<string>("Description");
                        b.Property<int>("Id")
                            .GenerateValuesOnAdd();
                        b.Property<string>("Image");
                        b.Property<bool>("IsModerated");
                        b.Property<bool>("IsSelected");
                        b.Property<int>("PostsPerPage");
                        b.Property<string>("Theme");
                        b.Property<string>("Title");
                        b.Key("Id");
                        b.ForRelational().Table("bf_blogs");
                    });
                
                builder.Entity("BlogiFire.Models.Post", b =>
                    {
                        b.Property<string>("AuthorName");
                        b.Property<int>("BlogId");
                        b.Property<int>("Comments");
                        b.Property<bool>("CommentsEnabled");
                        b.Property<string>("Content");
                        b.Property<int>("Id")
                            .GenerateValuesOnAdd();
                        b.Property<bool>("IsSelected");
                        b.Property<DateTime>("Published");
                        b.Property<DateTime>("Saved");
                        b.Property<string>("Slug");
                        b.Property<string>("Tags");
                        b.Property<string>("Title");
                        b.Key("Id");
                        b.ForRelational().Table("bf_posts");
                    });
                
                builder.Entity("BlogiFire.Models.Setting", b =>
                    {
                        b.Property<int>("BlogId");
                        b.Property<int>("Id")
                            .GenerateValuesOnAdd();
                        b.Property<string>("SettingKey");
                        b.Property<string>("SettingValue");
                        b.Key("Id");
                        b.ForRelational().Table("bf_settings");
                    });
                
                return builder.Model;
            }
        }
    }
}