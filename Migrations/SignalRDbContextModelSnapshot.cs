﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SignalR_Asp.netCoreWebAppDemo.Context;

#nullable disable

namespace SignalRAsp.netCoreWebAppDemo.Migrations
{
    [DbContext(typeof(SignalRDbContext))]
    partial class SignalRDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SignalR_Asp.netCoreWebAppDemo.Model.ClientInfo", b =>
                {
                    b.Property<string>("MacAddress")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConnectionId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MacAddress", "ConnectionId");

                    b.ToTable("UserConnections");
                });
#pragma warning restore 612, 618
        }
    }
}