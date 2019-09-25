using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace iWasHere.Domain.Model
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Attractions> Attractions { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Currrency> Currrency { get; set; }
        public virtual DbSet<DictionaryAttractionType> DictionaryAttractionType { get; set; }
        public virtual DbSet<DictionaryCity> DictionaryCity { get; set; }
        public virtual DbSet<DictionaryCountry> DictionaryCountry { get; set; }
        public virtual DbSet<DictionaryCounty> DictionaryCounty { get; set; }
        public virtual DbSet<DictionaryCurrencyType> DictionaryCurrencyType { get; set; }
        public virtual DbSet<DictionaryLandmarkType> DictionaryLandmarkType { get; set; }
        public virtual DbSet<DictionarySeasonType> DictionarySeasonType { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }

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

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Attractions>(entity =>
            {
                entity.HasKey(e => e.AttractionId)
                    .HasName("PK__Attracti__DAE24D5AFEB6832C");

                

                entity.Property(e => e.AttractionName)                
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.AttractionType)
                    .WithMany(p => p.Attractions)
                    .HasForeignKey(d => d.AttractionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attractions_AttractionTypeId");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Attractions)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Attractions_CityId");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Attractions)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attractions_CurrencyId");

                entity.HasOne(d => d.LandmarkType)
                    .WithMany(p => p.Attractions)
                    .HasForeignKey(d => d.LandmarkTypeId)
                    .HasConstraintName("FK_Attractions_LandmarkTypeId");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Attractions)
                    .HasForeignKey(d => d.SeasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attractions_SeasonId");

                entity.Property(e => e.Observations);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CommentText)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CommentTitle)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Attraction)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.AttractionId)
                    .HasConstraintName("FK_Comment_AttractionId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Comments_UserId");
            });

            modelBuilder.Entity<Currrency>(entity =>
            {
                entity.HasKey(e => e.CurrencyId)
                    .HasName("PK__Currrenc__14470AF0DB4AD544");

                entity.Property(e => e.CurrencyDate).HasColumnType("datetime");

                entity.HasOne(d => d.CurrencyType)
                    .WithMany(p => p.Currrency)
                    .HasForeignKey(d => d.CurrencyTypeId)
                    .HasConstraintName("FK_Currency_CurrencyTypeId");
            });

            modelBuilder.Entity<DictionaryAttractionType>(entity =>
            {
                entity.Property(e => e.DictionaryAttractionCode).IsUnicode(false);

                entity.Property(e => e.DictionaryAttractionName).IsUnicode(false);
            });

            modelBuilder.Entity<DictionaryCity>(entity =>
            {
                entity.HasIndex(e => e.DictionaryCityCode)
                    .HasName("UQ__Dictiona__D7ABC80E440C967B")
                    .IsUnique();

                entity.HasIndex(e => e.DictionaryCityName)
                    .HasName("UQ__Dictiona__2C48D7A1193DC487")
                    .IsUnique();

                entity.Property(e => e.DictionaryCityCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DictionaryCityName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.DictionaryCounty)
                    .WithMany(p => p.DictionaryCity)
                    .HasForeignKey(d => d.DictionaryCountyId)
                    .HasConstraintName("FK_Cities_CountyId");
            });

            modelBuilder.Entity<DictionaryCountry>(entity =>
            {
                entity.HasIndex(e => e.DictionaryCountryCode)
                    .HasName("UQ__Dictiona__F6AAAA4F263E1F00")
                    .IsUnique();

                entity.HasIndex(e => e.DictionaryCountryName)
                    .HasName("UQ__Dictiona__0D65D389E1E5B21B")
                    .IsUnique();

                entity.Property(e => e.DictionaryCountryCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DictionaryCountryName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DictionaryCounty>(entity =>
            {
                entity.HasIndex(e => e.DictionaryCountyCode)
                    .HasName("UQ__Dictiona__3487E44E896779C1")
                    .IsUnique();

                entity.HasIndex(e => e.DictionaryCountyName)
                    .HasName("UQ__Dictiona__B6C0AD523C0A428C")
                    .IsUnique();

                entity.Property(e => e.DictionaryCountyCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DictionaryCountyName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.DictionaryCountry)
                    .WithMany(p => p.DictionaryCounty)
                    .HasForeignKey(d => d.DictionaryCountryId)
                    .HasConstraintName("FK_Counties_CountryId");
            });

            modelBuilder.Entity<DictionaryCurrencyType>(entity =>
            {
                entity.Property(e => e.DictionaryCurrencyCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.DictionaryCurrencyName)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DictionaryLandmarkType>(entity =>
            {
                entity.HasKey(e => e.DictionaryItemId);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DictionaryItemCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DictionaryItemName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DictionarySeasonType>(entity =>
            {
                entity.HasKey(e => e.DictionarySeasonId)
                    .HasName("PK__Dictiona__585CA0BEC2C2EAE8");

                entity.Property(e => e.DictionarySeasonCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.DictionarySeasonName)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Photo>(entity =>
            {                
                entity.Property(e => e.PhotoName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Attraction)
                    .WithMany(p => p.Photo)
                    .HasForeignKey(d => d.AttractionId)
                    .HasConstraintName("FK_Photos_AttractionId");
            });
        }
    }
}
