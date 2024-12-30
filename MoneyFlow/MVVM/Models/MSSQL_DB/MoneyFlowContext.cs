using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MoneyFlow.MVVM.Models.MSSQL_DB;

public partial class MoneyFlowContext : DbContext
{
    public MoneyFlowContext()
    {
    }

    public MoneyFlowContext(DbContextOptions<MoneyFlowContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<FinancialRecord> FinancialRecords { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Subcategory> Subcategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=STYX;Database=MoneyFlow;Trusted_Connection=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory);

            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.CategoryName).HasColumnName("category_name");
            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .HasColumnName("color");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Image).HasColumnName("image");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Categories)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Categories_User");
        });

        modelBuilder.Entity<FinancialRecord>(entity =>
        {
            entity.HasKey(e => e.IdFinancialRecord);

            entity.ToTable("Financial_records");

            entity.Property(e => e.IdFinancialRecord).HasColumnName("id_financial_record");
            entity.Property(e => e.Amount)
                .HasColumnType("money")
                .HasColumnName("amount");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.IdSubcategory).HasColumnName("id_subcategory");
            entity.Property(e => e.RecordName).HasColumnName("record_Name");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.FinancialRecords)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Financial_records_Categories");

            entity.HasOne(d => d.IdSubcategoryNavigation).WithMany(p => p.FinancialRecords)
                .HasForeignKey(d => d.IdSubcategory)
                .HasConstraintName("FK_Financial_records_Subcategories");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.IdGender);

            entity.Property(e => e.IdGender).HasColumnName("id_gender");
            entity.Property(e => e.GenderName)
                .HasMaxLength(50)
                .HasColumnName("gender_name");
        });

        modelBuilder.Entity<Subcategory>(entity =>
        {
            entity.HasKey(e => e.IdSubcategory);

            entity.Property(e => e.IdSubcategory).HasColumnName("id_subcategory");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.SubcategoryName).HasColumnName("subcategory_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.ToTable("User");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IdGender).HasColumnName("Id_gender");
            entity.Property(e => e.Login).HasColumnName("login");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.UserName).HasColumnName("user_name");

            entity.HasOne(d => d.IdGenderNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdGender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Genders");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
