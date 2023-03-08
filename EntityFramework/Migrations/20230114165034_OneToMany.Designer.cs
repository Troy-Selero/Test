﻿// <auto-generated />
using EntityFramework.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EntityFramework.Migrations
{
    [DbContext(typeof(MVCContext))]
    [Migration("20230114165034_OneToMany")]
    partial class OneToMany
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EntityFramework.Models.Entity", b =>
                {
                    b.Property<long>("EntityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("EntityID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("EntityID");

                    b.ToTable("Entities");
                });

            modelBuilder.Entity("EntityFramework.Models.EntityTPA", b =>
                {
                    b.Property<long>("EntityTPAID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("EntityTPAID"));

                    b.Property<long>("EntityID")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("EntityTPAID");

                    b.HasIndex("EntityID");

                    b.ToTable("EntityTPAs");
                });

            modelBuilder.Entity("EntityFramework.Models.EntityTPA", b =>
                {
                    b.HasOne("EntityFramework.Models.Entity", "Entity")
                        .WithMany("EntityTPAs")
                        .HasForeignKey("EntityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("EntityFramework.Models.Entity", b =>
                {
                    b.Navigation("EntityTPAs");
                });
#pragma warning restore 612, 618
        }
    }
}