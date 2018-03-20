﻿// <auto-generated />
using Core.News.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Extensions.Logging;
using News.Core.SqlServer.Models;
using System;

namespace News.Core.SqlServer.Migrations
{
    [DbContext(typeof(NewsDbContext))]
    partial class NewsDbModelSnapshot : ModelSnapshot
    {
        
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.News.Web.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Core.News.Web.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("ItemContentId");

                    b.Property<DateTime?>("ModifiedDate");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ItemContentId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Core.News.Web.Entities.ItemContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BigImage")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("MediumImage")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<long?>("NumOfView");

                    b.Property<string>("RawData");

                    b.Property<string>("SmallImage")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("SortDescription")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.ToTable("ItemContents");
                });

            modelBuilder.Entity("Core.News.Web.Entities.Item", b =>
                {
                    b.HasOne("Core.News.Web.Entities.Category", "Category")
                        .WithMany("Items")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Core.News.Web.Entities.ItemContent", "ItemContent")
                        .WithMany("Items")
                        .HasForeignKey("ItemContentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
