﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaymentAPI.Context;

namespace PaymentAPI.Migrations
{
    [DbContext(typeof(ProcessPaymentsContext))]
    partial class ProcessPaymentsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PaymentAPI.Context.ProcessPaymentDetail", b =>
                {
                    b.Property<int>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("CardHolder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreditCardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentID");

                    b.ToTable("ProcessPaymentDetail");
                });
#pragma warning restore 612, 618
        }
    }
}
