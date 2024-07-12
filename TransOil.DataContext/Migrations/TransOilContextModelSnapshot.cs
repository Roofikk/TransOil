﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TransOil.DataContext;

#nullable disable

namespace TransOil.DataContext.Migrations
{
    [DbContext(typeof(TransOilContext))]
    partial class TransOilContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("TransOil.DataContext.EntityModels.CompanyBase", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CompanyId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(128)");

                    b.HasKey("CompanyId");

                    b.ToTable("Companies", (string)null);

                    b.HasDiscriminator().HasValue("CompanyBase");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.CounterBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("MeasurementPointId")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("VerifyDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("MeasurementPointId", "Discriminator")
                        .IsUnique()
                        .HasDatabaseName("IX_Counters_MeasurementPointId_Discriminator");

                    b.ToTable("Counters", (string)null);

                    b.HasDiscriminator().HasValue("CounterBase");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("CustomerId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.Measurement", b =>
                {
                    b.Property<int>("MeasurementPointId")
                        .HasColumnType("int");

                    b.Property<int>("MeasurementDeviceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.HasKey("MeasurementPointId", "MeasurementDeviceId", "Date");

                    b.HasIndex("MeasurementDeviceId");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.MeasurementDevice", b =>
                {
                    b.Property<int>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("DeviceId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("SupplyPointId")
                        .HasColumnType("int");

                    b.HasKey("DeviceId");

                    b.HasIndex("SupplyPointId")
                        .IsUnique();

                    b.ToTable("MeasurementDevices");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.MeasurementPoint", b =>
                {
                    b.Property<int>("MeasurementPointId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("MeasurementPointId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("MeasurementPointId");

                    b.HasIndex("CustomerId");

                    b.ToTable("MeasurementPoints");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.SupplyPoint", b =>
                {
                    b.Property<int>("SupplyPointId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("SupplyPointId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<double>("MaxVoltage")
                        .HasColumnType("double");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("SupplyPointId");

                    b.HasIndex("CustomerId");

                    b.ToTable("SupplyPoints");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.ChildCompany", b =>
                {
                    b.HasBaseType("TransOil.DataContext.EntityModels.CompanyBase");

                    b.Property<int>("ParentCompanyId")
                        .HasColumnType("int");

                    b.HasIndex("ParentCompanyId");

                    b.HasDiscriminator().HasValue("ChildCompany");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.Company", b =>
                {
                    b.HasBaseType("TransOil.DataContext.EntityModels.CompanyBase");

                    b.HasDiscriminator().HasValue("Company");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.ElectricityCounter", b =>
                {
                    b.HasBaseType("TransOil.DataContext.EntityModels.CounterBase");

                    b.HasIndex("MeasurementPointId");

                    b.HasDiscriminator().HasValue("ElectricityCounter");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.TransformerCounterBase", b =>
                {
                    b.HasBaseType("TransOil.DataContext.EntityModels.CounterBase");

                    b.Property<double>("TransformerRatio")
                        .HasColumnType("double");

                    b.HasDiscriminator().HasValue("TransformerCounterBase");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.CurrentTransformer", b =>
                {
                    b.HasBaseType("TransOil.DataContext.EntityModels.TransformerCounterBase");

                    b.HasIndex("MeasurementPointId");

                    b.HasDiscriminator().HasValue("CurrentTransformer");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.VoltageTransformer", b =>
                {
                    b.HasBaseType("TransOil.DataContext.EntityModels.TransformerCounterBase");

                    b.HasIndex("MeasurementPointId");

                    b.HasDiscriminator().HasValue("VoltageTransformer");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.Customer", b =>
                {
                    b.HasOne("TransOil.DataContext.EntityModels.ChildCompany", "Company")
                        .WithMany("Customers")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.Measurement", b =>
                {
                    b.HasOne("TransOil.DataContext.EntityModels.MeasurementDevice", "MeasurementDevice")
                        .WithMany("Measurements")
                        .HasForeignKey("MeasurementDeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TransOil.DataContext.EntityModels.MeasurementPoint", "MeasurementPoint")
                        .WithMany("Measurements")
                        .HasForeignKey("MeasurementPointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MeasurementDevice");

                    b.Navigation("MeasurementPoint");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.MeasurementDevice", b =>
                {
                    b.HasOne("TransOil.DataContext.EntityModels.SupplyPoint", "SupplyPoint")
                        .WithOne("MeasurementDevice")
                        .HasForeignKey("TransOil.DataContext.EntityModels.MeasurementDevice", "SupplyPointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SupplyPoint");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.MeasurementPoint", b =>
                {
                    b.HasOne("TransOil.DataContext.EntityModels.Customer", "Customer")
                        .WithMany("Measurments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.SupplyPoint", b =>
                {
                    b.HasOne("TransOil.DataContext.EntityModels.Customer", "Customer")
                        .WithMany("Supplies")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.ChildCompany", b =>
                {
                    b.HasOne("TransOil.DataContext.EntityModels.Company", "ParentCompany")
                        .WithMany("ChildCompanies")
                        .HasForeignKey("ParentCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentCompany");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.ElectricityCounter", b =>
                {
                    b.HasOne("TransOil.DataContext.EntityModels.MeasurementPoint", "MeasurementPoint")
                        .WithOne("ElectricityCounter")
                        .HasForeignKey("TransOil.DataContext.EntityModels.ElectricityCounter", "MeasurementPointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Counters_MeasurementPoints_MeasurementPointId");

                    b.Navigation("MeasurementPoint");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.CurrentTransformer", b =>
                {
                    b.HasOne("TransOil.DataContext.EntityModels.MeasurementPoint", "MeasurementPoint")
                        .WithOne("CurrentTransformer")
                        .HasForeignKey("TransOil.DataContext.EntityModels.CurrentTransformer", "MeasurementPointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Counters_MeasurementPoints_MeasurementPointId");

                    b.Navigation("MeasurementPoint");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.VoltageTransformer", b =>
                {
                    b.HasOne("TransOil.DataContext.EntityModels.MeasurementPoint", "MeasurementPoint")
                        .WithOne("VoltageTransformer")
                        .HasForeignKey("TransOil.DataContext.EntityModels.VoltageTransformer", "MeasurementPointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Counters_MeasurementPoints_MeasurementPointId");

                    b.Navigation("MeasurementPoint");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.Customer", b =>
                {
                    b.Navigation("Measurments");

                    b.Navigation("Supplies");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.MeasurementDevice", b =>
                {
                    b.Navigation("Measurements");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.MeasurementPoint", b =>
                {
                    b.Navigation("CurrentTransformer");

                    b.Navigation("ElectricityCounter");

                    b.Navigation("Measurements");

                    b.Navigation("VoltageTransformer");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.SupplyPoint", b =>
                {
                    b.Navigation("MeasurementDevice")
                        .IsRequired();
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.ChildCompany", b =>
                {
                    b.Navigation("Customers");
                });

            modelBuilder.Entity("TransOil.DataContext.EntityModels.Company", b =>
                {
                    b.Navigation("ChildCompanies");
                });
#pragma warning restore 612, 618
        }
    }
}
