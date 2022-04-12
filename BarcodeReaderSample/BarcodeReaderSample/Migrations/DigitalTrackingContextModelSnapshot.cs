﻿// <auto-generated />
using System;
using BarcodeReaderSample.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MIgrationCreator.Migrations
{
    [DbContext(typeof(DigitalTrackingContext))]
    partial class DigitalTrackingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.20");

            modelBuilder.Entity("Entities.Box", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDisAggregated")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("PalletId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PalletId");

                    b.HasIndex("ProductId");

                    b.ToTable("Boxes");
                });

            modelBuilder.Entity("Entities.CodesMapping", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("BoxId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("DataMatrixCodeId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PalletId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BoxId");

                    b.HasIndex("DataMatrixCodeId");

                    b.HasIndex("PalletId");

                    b.ToTable("CodesMappings");
                });

            modelBuilder.Entity("Entities.DataMatrix", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("BoxId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BoxId");

                    b.HasIndex("ProductId");

                    b.ToTable("DataMatrix");
                });

            modelBuilder.Entity("Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Cash")
                        .HasColumnType("REAL");

                    b.Property<string>("FiscalBoxData")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsWaitingFiscalBox")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("OrderNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PaymentType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("QrPaymentUrl")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SupplierId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Terminal")
                        .HasColumnType("REAL");

                    b.Property<double?>("Total")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Entities.OrderCodeMapping", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("BoxId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("DataMatrixCodeId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OrderDetailId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PalletId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BoxId");

                    b.HasIndex("DataMatrixCodeId");

                    b.HasIndex("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.HasIndex("PalletId");

                    b.ToTable("OrderCodeMappings");
                });

            modelBuilder.Entity("Entities.OrderDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Total")
                        .HasColumnType("REAL");

                    b.Property<int>("UnitOfMeasurement")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Entities.Pallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDisAggregated")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Pallets");
                });

            modelBuilder.Entity("Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AmountInBox")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AmountInPallet")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Barcode")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ikpu")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Entities.Setting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Port")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Setting");
                });

            modelBuilder.Entity("Entities.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ClientAddressId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("Inn")
                        .HasColumnType("TEXT");

                    b.Property<string>("LegalAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleType")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("TransportId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Entities.Box", b =>
                {
                    b.HasOne("Entities.Pallet", "Pallet")
                        .WithMany("Boxes")
                        .HasForeignKey("PalletId");

                    b.HasOne("Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.CodesMapping", b =>
                {
                    b.HasOne("Entities.Box", "Box")
                        .WithMany()
                        .HasForeignKey("BoxId");

                    b.HasOne("Entities.DataMatrix", "DataMatrix")
                        .WithMany()
                        .HasForeignKey("DataMatrixCodeId");

                    b.HasOne("Entities.Pallet", "Pallet")
                        .WithMany()
                        .HasForeignKey("PalletId");
                });

            modelBuilder.Entity("Entities.DataMatrix", b =>
                {
                    b.HasOne("Entities.Box", "Box")
                        .WithMany("DataMatrices")
                        .HasForeignKey("BoxId");

                    b.HasOne("Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Order", b =>
                {
                    b.HasOne("Entities.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.OrderCodeMapping", b =>
                {
                    b.HasOne("Entities.Box", "Box")
                        .WithMany()
                        .HasForeignKey("BoxId");

                    b.HasOne("Entities.DataMatrix", "DataMatrix")
                        .WithMany()
                        .HasForeignKey("DataMatrixCodeId");

                    b.HasOne("Entities.OrderDetail", "OrderDetail")
                        .WithMany("OrderCodeMappings")
                        .HasForeignKey("OrderDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Pallet", "Pallet")
                        .WithMany()
                        .HasForeignKey("PalletId");
                });

            modelBuilder.Entity("Entities.OrderDetail", b =>
                {
                    b.HasOne("Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Pallet", b =>
                {
                    b.HasOne("Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
