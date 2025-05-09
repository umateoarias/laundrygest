using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Webservice_Laundrygest.Models;

public partial class LaundrygestContext : DbContext
{
    public LaundrygestContext()
    {
    }

    public LaundrygestContext(DbContextOptions<LaundrygestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Collection> Collections { get; set; }

    public virtual DbSet<CollectionItem> CollectionItems { get; set; }

    public virtual DbSet<CollectionType> CollectionTypes { get; set; }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Pricelist> Pricelists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\sqlexpress; Trusted_Connection=True; Encrypt=false; Database=Laundrygest");
        //=> optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=Laundrygest;User ID=sa;Password=Unikb6847;Authentication=SqlPassword;Encrypt=True;TrustServerCertificate=True;);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__client__357D4CF8B36564BD");

            entity.ToTable("client");

            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Locality)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("locality");
            entity.Property(e => e.Nif)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nif");
            entity.Property(e => e.PasswordApp)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_app");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("postal_code");
            entity.Property(e => e.Telephone)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("telephone");
            entity.Property(e => e.UsernameApp)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username_app");
            entity.Property(e=>e.LastLoginMobile).HasColumnType("datetime").HasColumnName("last_login_mobile");
        });

        modelBuilder.Entity<Collection>(entity =>
        {
            entity.HasKey(e => e.Number).HasName("PK__collecti__FD291E40431E1DF3");

            entity.ToTable("collection");

            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.ClientCode).HasColumnName("client_code");
            entity.Property(e => e.CollectionTypeCode).HasColumnName("collection_type_code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DueDate)
                .HasColumnType("datetime")
                .HasColumnName("due_date");
            entity.Property(e => e.DueTotal)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("due_total");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.PaymentMode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("payment_mode");
            entity.Property(e => e.TaxAmount)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("tax_amount");
            entity.Property(e => e.TaxBase)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("tax_base");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("total");

            entity.HasOne(d => d.ClientCodeNavigation).WithMany(p => p.Collections)
                .HasForeignKey(d => d.ClientCode)
                .HasConstraintName("FK__collectio__clien__5629CD9C");

            entity.HasOne(d => d.CollectionTypeCodeNavigation).WithMany(p => p.Collections)
                .HasForeignKey(d => d.CollectionTypeCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__collectio__colle__571DF1D5");

            entity.HasOne(d => d.Invoice).WithMany(p => p.Collections)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK__collectio__invoi__5812160E");
        });

        modelBuilder.Entity<CollectionItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__collecti__3213E83FCE27920C");

            entity.ToTable("collection_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CollectedAt)
                .HasColumnType("datetime")
                .HasColumnName("collected_at");
            entity.Property(e => e.CollectionNumber).HasColumnName("collection_number");
            entity.Property(e => e.DeliveryNumber).HasColumnName("delivery_number");
            entity.Property(e => e.NumPieces).HasColumnName("num_pieces");
            entity.Property(e => e.Observation)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("observation");
            entity.Property(e => e.PricelistCode).HasColumnName("pricelist_code");
            entity.Property(e => e.StoreLocation)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("store_location");

            entity.HasOne(d => d.CollectionNumberNavigation).WithMany(p => p.CollectionItems)
                .HasForeignKey(d => d.CollectionNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__collectio__colle__5AEE82B9");

            entity.HasOne(d => d.DeliveryNumberNavigation).WithMany(p => p.CollectionItems)
                .HasForeignKey(d => d.DeliveryNumber)
                .HasConstraintName("FK_deliveryItems");

            entity.HasOne(d => d.PricelistCodeNavigation).WithMany(p => p.CollectionItems)
                .HasForeignKey(d => d.PricelistCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__collectio__price__5BE2A6F2");
        });

        modelBuilder.Entity<CollectionType>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__collecti__357D4CF875281A1F");

            entity.ToTable("collection_type");

            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.Number).HasName("PK__delivery__FD291E40FE83A363");

            entity.ToTable("delivery");

            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.DeliveryDate)
                .HasColumnType("datetime")
                .HasColumnName("delivery_date");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__invoice__3213E83F004381E0");

            entity.ToTable("invoice");

            entity.HasIndex(e => e.Number, "UQ__invoice__FD291E413F7A0006").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientCode).HasColumnName("client_code");
            entity.Property(e => e.Number)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("number");
            entity.Property(e => e.TaxAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("tax_amount");
            entity.Property(e => e.TaxBase)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("tax_base");
            entity.Property(e => e.TotalBase)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total_base");

            entity.HasOne(d => d.ClientCodeNavigation).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.ClientCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__invoice__client___52593CB8");
        });

        modelBuilder.Entity<Pricelist>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__pricelis__357D4CF8B48FA89A");

            entity.ToTable("pricelist");

            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.CollectionTypeCode).HasColumnName("collection_type_code");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.NumPieces).HasColumnName("num_pieces");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("unit_price");

            entity.HasOne(d => d.CollectionTypeCodeNavigation).WithMany(p => p.Pricelists)
                .HasForeignKey(d => d.CollectionTypeCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__pricelist__colle__4E88ABD4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
