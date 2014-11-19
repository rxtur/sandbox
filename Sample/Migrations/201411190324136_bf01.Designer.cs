using BlogiFire.Models;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using System;

namespace Sample.Migrations
{
    [ContextType(typeof(BlogContext))]
    public partial class bf01 : IMigrationMetadata
    {
        string IMigrationMetadata.MigrationId
        {
            get
            {
                return "201411190324136_bf01";
            }
        }
        
        string IMigrationMetadata.ProductVersion
        {
            get
            {
                return "7.0.0-beta1-11518";
            }
        }
        
        IModel IMigrationMetadata.TargetModel
        {
            get
            {
                var builder = new BasicModelBuilder();
                
                builder.Entity("BlogiFire.Models.Blog", b =>
                    {
                        b.Property<int>("Id")
                            .GenerateValuesOnAdd();
                        b.Property<string>("Name");
                        b.Key("Id");
                    });
                
                builder.Entity("BlogiFire.Models.Post", b =>
                    {
                        b.Property<int>("BlogId");
                        b.Property<int>("Comments");
                        b.Property<string>("Content");
                        b.Property<DateTime>("Created");
                        b.Property<int>("Id")
                            .GenerateValuesOnAdd();
                        b.Property<bool>("Published");
                        b.Property<string>("Title");
                        b.Key("Id");
                    });
                
                return builder.Model;
            }
        }
    }
}