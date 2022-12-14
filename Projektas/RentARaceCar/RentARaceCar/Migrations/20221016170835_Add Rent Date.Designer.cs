// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RentARaceCar.DbContext;

#nullable disable

namespace RentARaceCar.Migrations
{
    [DbContext(typeof(RentARaceCarDbContext))]
    [Migration("20221016170835_Add Rent Date")]
    partial class AddRentDate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("RentARaceCar.Models.DomainModels.CarModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("DrivetrainType")
                        .HasColumnType("int");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("RentOfficeId")
                        .HasColumnType("char(36)");

                    b.Property<int>("TransmissionType")
                        .HasColumnType("int");

                    b.Property<int>("TyreCompound")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RentOfficeId");

                    b.ToTable("Cars", (string)null);
                });

            modelBuilder.Entity("RentARaceCar.Models.DomainModels.OrderModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CarId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("RentDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("RentARaceCar.Models.DomainModels.RentOfficeModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("RentOffices", (string)null);
                });

            modelBuilder.Entity("RentARaceCar.Models.DomainModels.CarModel", b =>
                {
                    b.HasOne("RentARaceCar.Models.DomainModels.RentOfficeModel", "RentOffice")
                        .WithMany("Cars")
                        .HasForeignKey("RentOfficeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("RentOffice");
                });

            modelBuilder.Entity("RentARaceCar.Models.DomainModels.OrderModel", b =>
                {
                    b.HasOne("RentARaceCar.Models.DomainModels.CarModel", "Car")
                        .WithMany("Orders")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("RentARaceCar.Models.DomainModels.CarModel", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("RentARaceCar.Models.DomainModels.RentOfficeModel", b =>
                {
                    b.Navigation("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
