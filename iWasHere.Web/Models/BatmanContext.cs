using System;
using iWasHere.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace iWasHere.Web.Models
{
    public partial class BatmanContext : DbContext
    {
        public BatmanContext()
        {
        }

        public BatmanContext(DbContextOptions<BatmanContext> options)
            : base(options)
        {
        }

       

        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }

     

        public virtual DbSet<DictionaryCity> DictionaryCity { get; set; }

        public virtual DbSet<DictionaryCountry> DictionaryCountry { get; set; }

        public virtual DbSet<DictionaryCounty> DictionaryCounty { get; set; }

        public virtual DbSet<Attractions> Attractions { get; set; }

        public virtual DbSet<Currrency> Currency { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<DictionaryCurrencyType> DictionaryCurrencyType { get; set; }
        public virtual DbSet<DictionaryLandmarkType> DictionaryLandmarkType { get; set; }
        public virtual DbSet<DictionarySeasonType> DictionarySeasonType { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<DictionaryAttractionType> DictionaryAttractionType { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=ts-internship-2019.database.windows.net;Initial Catalog=Batman;Persist Security Info=False;User ID=sa_admin;Password=A123456a;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<DictionaryCity>(entity =>
            {
                entity.HasKey(e => e.DictionaryCityId)
                    .HasName("PK__Dictiona__F5D30E9316953687");
                entity.HasIndex(e => e.DictionaryCityCode)
                     .HasName("UQ__Dictiona__D7ABC80E440C967B");
                entity.HasIndex(e => e.DictionaryCityName)
                      .HasName("UQ__Dictiona__2C48D7A1193DC487");
                entity.HasOne(d => d.DictionaryCounty)
                    .WithMany(p => p.DictionaryCity)
                    .HasForeignKey(d => d.DictionaryCountyId)
                    .HasConstraintName("FK_Cities_CountyId");
            });
            modelBuilder.Entity<DictionaryCountry>(entity =>
            {
                entity.HasKey(e => e.DictionaryCountryId)
                    .HasName("PK__Dictiona__AA09346089EF529D");
                entity.HasIndex(e => e.DictionaryCountryCode)
                    .HasName("UQ__Dictiona__F6AAAA4F263E1F00")
                    .IsUnique();
                entity.HasIndex(e => e.DictionaryCountryName)
                    .HasName("UQ__Dictiona__0D65D389E1E5B21B")
                    .IsUnique();
            });

            modelBuilder.Entity<DictionaryCounty>(entity =>
            {
                entity.HasKey(e => e.DictionaryCountyId)
                    .HasName("PK__Dictiona__B782E34925BB4764");
                entity.HasIndex(e => e.DictionaryCountyCode)
                    .HasName("UQ__Dictiona__3487E44E896779C1")
                    .IsUnique();
                entity.HasIndex(e => e.DictionaryCountyName)
                    .HasName("UQ__Dictiona__B6C0AD523C0A428C")
                    .IsUnique();
                entity.HasOne(d => d.DictionaryCountry)
                    .WithMany(p => p.DictionaryCounty)
                    .HasForeignKey(d => d.DictionaryCountryId)
                    .HasConstraintName("FK_Counties_CountryId");
            });

            modelBuilder.Entity<Attractions>(entity =>
            {
                entity.HasKey(e => e.AttractionId)
                        .HasName("PK__Attracti__DAE24D5AFEB6832C");
                entity.Property(e => e.Observations)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.AttractionName).HasMaxLength(256);
                entity.Property(e => e.Latitude).HasMaxLength(256);
                entity.Property(e => e.Longitude).HasMaxLength(256);
                entity.Property(e => e.Price).HasMaxLength(256);
                entity.HasOne(d => d.City)
                    .WithMany(p => p.Attractions)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Attractions_CityId");
                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Attractions)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK_Attractions_CurrencyId");
                entity.HasOne(d => d.LandmarkType)
                    .WithMany(p => p.Attractions)
                    .HasForeignKey(d => d.LandmarkTypeId)
                    .HasConstraintName("FK_Attractions_LandmarkTypeId");
                entity.HasOne(d => d.AttractionType)
                    .WithMany(p => p.Attractions)
                    .HasForeignKey(d => d.AttractionTypeId)
                    .HasConstraintName("FK_Attractions_AttractionTypeId");
                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Attractions)
                    .HasForeignKey(d => d.SeasonId)
                    .HasConstraintName("FK_Attractions_SeasonId");
            });

            modelBuilder.Entity<Currrency>(entity =>
            {
                entity.HasKey(e => e.CurrencyId)
                    .HasName("PK__Currrenc__14470AF0DB4AD544");
                entity.HasIndex(e => e.CurrencyDate);
                entity.HasOne(d => d.CurrencyType)
                    .WithMany(p => p.Currrency)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK_Currency_CurrencyTypeId");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PK__Comment__C3B4DFCAA96945D0");
                entity.HasIndex(e => e.CommentTitle);
                entity.HasIndex(e => e.CommentText);
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Comments_UserId");
                entity.HasOne(d => d.Attraction)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.AttractionId)
                    .HasConstraintName("FK_Comment_AttractionId");
            });

            modelBuilder.Entity<DictionaryAttractionType>(entity =>
            {
                entity.HasKey(e => e.DictionaryAttractionTypeId)
                    .HasName("PK__Dictiona__5D1EA74F194EA879");
                entity.HasIndex(e => e.DictionaryAttractionName).IsUnique();
                entity.HasIndex(e => e.DictionaryAttractionCode).IsUnique();
            });

            modelBuilder.Entity<DictionaryCurrencyType>(entity =>
            {
                entity.HasKey(e => e.DictionaryCurrencyTypeId)
                    .HasName("PK__Dictiona__AD1CD96F9FC2F748");
                entity.HasIndex(e => e.DictionaryCurrencyName).IsUnique();
                entity.HasIndex(e => e.DictionaryCurrencyCode).IsUnique();
            });

            modelBuilder.Entity<DictionaryCurrencyType>(entity =>
            {
                entity.HasKey(e => e.DictionaryCurrencyTypeId)
                    .HasName("PK__Dictiona__AD1CD96F9FC2F748");
                entity.HasIndex(e => e.DictionaryCurrencyName).IsUnique();
                entity.HasIndex(e => e.DictionaryCurrencyCode).IsUnique();
            });

            modelBuilder.Entity<DictionaryLandmarkType>(entity =>
            {
                entity.HasKey(e => e.DictionaryItemId)
                    .HasName("PK_DictionaryLandmarkType");
                entity.HasIndex(e => e.DictionaryItemCode).IsUnique();
                entity.HasIndex(e => e.DictionaryItemName).IsUnique();
                entity.HasIndex(e => e.Description).IsUnique();
            });

            modelBuilder.Entity<DictionarySeasonType>(entity =>
            {
                entity.HasKey(e => e.DictionarySeasonId)
                    .HasName("PK__Dictiona__585CA0BEC2C2EAE8");
                entity.HasIndex(e => e.DictionarySeasonCode).IsUnique();
                entity.HasIndex(e => e.DictionarySeasonName).IsUnique();
            });
        }
    }
}
