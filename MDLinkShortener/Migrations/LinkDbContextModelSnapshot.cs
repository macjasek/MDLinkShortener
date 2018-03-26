﻿// <auto-generated />
using MDLinkShortener;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace MDLinkShortener.Migrations
{
    [DbContext(typeof(LinkDbContext))]
    partial class LinkDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("MDLinkShortener.Models.IpAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientIp");

                    b.HasKey("Id");

                    b.ToTable("IpAddresses");
                });

            modelBuilder.Entity("MDLinkShortener.Models.IpLink", b =>
                {
                    b.Property<int>("LinkId");

                    b.Property<int>("IpAddressId");

                    b.HasKey("LinkId", "IpAddressId");

                    b.HasIndex("IpAddressId");

                    b.ToTable("IpLink");
                });

            modelBuilder.Entity("MDLinkShortener.Models.Link", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Clicks");

                    b.Property<string>("FullLink")
                        .IsRequired();

                    b.Property<string>("ShortLink");

                    b.Property<int>("UniqueClicks");

                    b.HasKey("Id");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("MDLinkShortener.Models.IpLink", b =>
                {
                    b.HasOne("MDLinkShortener.Models.IpAddress", "IpAddress")
                        .WithMany("IpLinks")
                        .HasForeignKey("IpAddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MDLinkShortener.Models.Link", "Link")
                        .WithMany("IpLinks")
                        .HasForeignKey("LinkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
