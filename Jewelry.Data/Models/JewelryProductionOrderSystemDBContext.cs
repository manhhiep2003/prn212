﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Jewelry.Data.Models;

public partial class JewelryProductionOrderSystemDBContext : DbContext
{
    public JewelryProductionOrderSystemDBContext()
    {
    }

    public JewelryProductionOrderSystemDBContext(DbContextOptions<JewelryProductionOrderSystemDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestDetail> RequestDetails { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=MSI;Initial Catalog=JewelryProductionOrderSystemDB;User ID=sa;Password=12345;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.CustomerAddress)
                .HasMaxLength(50)
                .HasColumnName("customer_address");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .HasColumnName("customer_name");
            entity.Property(e => e.CustomerPhone)
                .HasMaxLength(50)
                .HasColumnName("customer_phone");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.ToTable("Request");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.CusId)
                .HasMaxLength(50)
                .HasColumnName("cus_id");
            entity.Property(e => e.TotalPrice)
                .HasMaxLength(50)
                .HasColumnName("total_price");

            entity.HasOne(d => d.Cus).WithMany(p => p.Requests)
                .HasForeignKey(d => d.CusId)
                .HasConstraintName("FK_Request_Customer");
        });

        modelBuilder.Entity<RequestDetail>(entity =>
        {
            entity.ToTable("Request Detail");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Jewelry)
                .HasMaxLength(50)
                .HasColumnName("jewelry");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasMaxLength(50)
                .HasColumnName("price");
            entity.Property(e => e.ReqId)
                .HasMaxLength(50)
                .HasColumnName("req_id");

            entity.HasOne(d => d.Req).WithMany(p => p.RequestDetails)
                .HasForeignKey(d => d.ReqId)
                .HasConstraintName("FK_Request Detail_Request");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity
                .ToTable("Result");

            entity.Property(e => e.CusId)
                .HasMaxLength(50)
                .HasColumnName("cus_id");
            entity.Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.ProductImage)
                .HasMaxLength(50)
                .HasColumnName("product_image");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .HasColumnName("product_name");
            entity.Property(e => e.ReqId)
                .HasMaxLength(50)
                .HasColumnName("req_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.TotalPrice)
                .HasMaxLength(50)
                .HasColumnName("total_price");
            entity.Property(e => e.TransferType)
                .HasMaxLength(50)
                .HasColumnName("transfer_type");

            entity.HasOne(d => d.Cus).WithMany()
                .HasForeignKey(d => d.CusId)
                .HasConstraintName("FK_Result_Customer");

            entity.HasOne(d => d.Req).WithMany()
                .HasForeignKey(d => d.ReqId)
                .HasConstraintName("FK_Result_Request");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}