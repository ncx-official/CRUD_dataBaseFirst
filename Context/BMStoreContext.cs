using System;
using System.Collections.Generic;
using DataBaseManagerApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DataBaseManagerApplication.Context
{
    public partial class BMStoreContext : DbContext
    {
        // private readonly StreamWriter _logStream = new StreamWriter($"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}/Logs/LogFile_{DateTime.Now.ToString("MM_dd_yy")}.txt", true);
        public BMStoreContext()
        {
            // Database.EnsureCreated();
            // Database.EnsureCreatedAsync();
            // Database.CanConnect();
            // Database.CanConnectAsync();
        }

        public BMStoreContext(DbContextOptions<BMStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authorization> Authorizations { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Operation> Operations { get; set; } = null!;
        public virtual DbSet<OperationProduct> OperationProducts { get; set; } = null!;
        public virtual DbSet<OperationType> OperationTypes { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<PersonSex> PersonSexes { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ScheduleOpen> ScheduleOpens { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<StoreAddress> StoreAddresses { get; set; } = null!;
        public virtual DbSet<StoreCity> StoreCities { get; set; } = null!;
        public virtual DbSet<StoreCountry> StoreCountries { get; set; } = null!;
        public virtual DbSet<StorePhone> StorePhones { get; set; } = null!;
        public virtual DbSet<WeekDay> WeekDays { get; set; } = null!;
        public virtual DbSet<WorkPosition> WorkPositions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // building config_file that includes connectionString
                var builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile($"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}/appsettings.json");
                var config = builder.Build();
                optionsBuilder.UseMySql(connectionString: config.GetConnectionString("DefaultConnection"),
                    serverVersion: ServerVersion.Parse(config["ServerVersion"]));
                // display logs
                // optionsBuilder.LogTo(_logStream.WriteLine, LogLevel.Information); 
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Authorization>(entity =>
            {
                entity.HasKey(e => e.IdPerson)
                    .HasName("PRIMARY");

                entity.ToTable("authorization");

                entity.Property(e => e.IdPerson)
                    .ValueGeneratedNever()
                    .HasColumnName("id_person");

                entity.Property(e => e.Login)
                    .HasMaxLength(80)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .HasMaxLength(80)
                    .HasColumnName("password");

                entity.HasOne(d => d.IdPersonNavigation)
                    .WithOne(p => p.Authorization)
                    .HasForeignKey<Authorization>(d => d.IdPerson)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("authorization_ibfk_1");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.IdClass)
                    .HasName("PRIMARY");

                entity.ToTable("class");

                entity.Property(e => e.IdClass).HasColumnName("id_class");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(100)
                    .HasColumnName("class_name");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdEmployee)
                    .HasName("PRIMARY");

                entity.ToTable("employee");

                entity.HasIndex(e => e.IdPerson, "id_person");

                entity.HasIndex(e => e.IdStore, "id_store");

                entity.HasIndex(e => e.IdWorkPosition, "id_work_position");

                entity.Property(e => e.IdEmployee).HasColumnName("id_employee");

                entity.Property(e => e.IdPerson).HasColumnName("id_person");

                entity.Property(e => e.IdStore).HasColumnName("id_store");

                entity.Property(e => e.IdWorkPosition).HasColumnName("id_work_position");

                entity.HasOne(d => d.IdPersonNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdPerson)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employee_ibfk_1");

                entity.HasOne(d => d.IdStoreNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdStore)
                    .HasConstraintName("employee_ibfk_2");

                entity.HasOne(d => d.IdWorkPositionNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdWorkPosition)
                    .HasConstraintName("employee_ibfk_3");
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.HasKey(e => e.IdOperation)
                    .HasName("PRIMARY");

                entity.ToTable("operation");

                entity.HasIndex(e => e.IdEmployee, "id_employee");

                entity.HasIndex(e => e.IdOperationType, "id_operation_type");

                entity.Property(e => e.IdOperation).HasColumnName("id_operation");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IdEmployee).HasColumnName("id_employee");

                entity.Property(e => e.IdOperationType).HasColumnName("id_operation_type");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.Operations)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("operation_ibfk_2");

                entity.HasOne(d => d.IdOperationTypeNavigation)
                    .WithMany(p => p.Operations)
                    .HasForeignKey(d => d.IdOperationType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("operation_ibfk_1");
            });

            modelBuilder.Entity<OperationProduct>(entity =>
            {
                entity.HasKey(e => new { e.IdOperation, e.IdProduct })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("operation_product");

                entity.HasIndex(e => e.IdProduct, "id_product");

                entity.Property(e => e.IdOperation).HasColumnName("id_operation");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.OperationProductCount).HasColumnName("operation_product_count");

                entity.Property(e => e.OperationProductPrice)
                    .HasColumnType("decimal(8,2) unsigned")
                    .HasColumnName("operation_product_price");

                entity.HasOne(d => d.IdOperationNavigation)
                    .WithMany(p => p.OperationProducts)
                    .HasForeignKey(d => d.IdOperation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("operation_product_ibfk_1");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.OperationProducts)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("operation_product_ibfk_2");
            });

            modelBuilder.Entity<OperationType>(entity =>
            {
                entity.HasKey(e => e.IdOperationType)
                    .HasName("PRIMARY");

                entity.ToTable("operation_type");

                entity.HasIndex(e => e.TypeName, "type_name")
                    .IsUnique();

                entity.Property(e => e.IdOperationType).HasColumnName("id_operation_type");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    .HasColumnName("type_name");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.IdPerson)
                    .HasName("PRIMARY");

                entity.ToTable("person");

                entity.HasIndex(e => e.IdSex, "id_sex");

                entity.Property(e => e.IdPerson).HasColumnName("id_person");

                entity.Property(e => e.Birthdate).HasColumnName("birthdate");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("firstName");

                entity.Property(e => e.IdSex).HasColumnName("id_sex");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("lastName");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(100)
                    .HasColumnName("middleName");

                entity.HasOne(d => d.IdSexNavigation)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.IdSex)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("person_ibfk_1");
            });

            modelBuilder.Entity<PersonSex>(entity =>
            {
                entity.HasKey(e => e.IdSex)
                    .HasName("PRIMARY");

                entity.ToTable("person_sex");

                entity.HasIndex(e => e.SexValue, "sex_value")
                    .IsUnique();

                entity.Property(e => e.IdSex).HasColumnName("id_sex");

                entity.Property(e => e.SexValue)
                    .HasMaxLength(100)
                    .HasColumnName("sex_value");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct)
                    .HasName("PRIMARY");

                entity.ToTable("product");

                entity.HasIndex(e => e.IdClass, "id_class");

                entity.HasIndex(e => e.IdStore, "id_store");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.DefineCode)
                    .HasMaxLength(255)
                    .HasColumnName("define_code");

                entity.Property(e => e.IdClass).HasColumnName("id_class");

                entity.Property(e => e.IdStore).HasColumnName("id_store");

                entity.Property(e => e.IsAvailable).HasColumnName("isAvailable");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.UsdPrice)
                    .HasColumnType("decimal(8,2) unsigned")
                    .HasColumnName("usd_price");

                entity.HasOne(d => d.IdClassNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdClass)
                    .HasConstraintName("product_ibfk_1");

                entity.HasOne(d => d.IdStoreNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdStore)
                    .HasConstraintName("product_ibfk_2");
            });

            modelBuilder.Entity<ScheduleOpen>(entity =>
            {
                entity.HasKey(e => e.IdScheduleOpen)
                    .HasName("PRIMARY");

                entity.ToTable("schedule_open");

                entity.HasIndex(e => e.IdWeekDay, "id_week_day");

                entity.Property(e => e.IdScheduleOpen).HasColumnName("id_schedule_open");

                entity.Property(e => e.CloseAt)
                    .HasColumnType("time")
                    .HasColumnName("close_at");

                entity.Property(e => e.IdWeekDay).HasColumnName("id_week_day");

                entity.Property(e => e.OpenAt)
                    .HasColumnType("time")
                    .HasColumnName("open_at");

                entity.HasOne(d => d.IdWeekDayNavigation)
                    .WithMany(p => p.ScheduleOpens)
                    .HasForeignKey(d => d.IdWeekDay)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("schedule_open_ibfk_1");

                entity.HasMany(d => d.IdStores)
                    .WithMany(p => p.IdScheduleOpens)
                    .UsingEntity<Dictionary<string, object>>(
                        "StoreScheduleOpen",
                        l => l.HasOne<Store>().WithMany().HasForeignKey("IdStore").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("store_schedule_open_ibfk_2"),
                        r => r.HasOne<ScheduleOpen>().WithMany().HasForeignKey("IdScheduleOpen").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("store_schedule_open_ibfk_1"),
                        j =>
                        {
                            j.HasKey("IdScheduleOpen", "IdStore").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("store_schedule_open");

                            j.HasIndex(new[] { "IdStore" }, "id_store");

                            j.IndexerProperty<long>("IdScheduleOpen").HasColumnName("id_schedule_open");

                            j.IndexerProperty<long>("IdStore").HasColumnName("id_store");
                        });
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.IdStore)
                    .HasName("PRIMARY");

                entity.ToTable("store");

                entity.HasIndex(e => e.IdStoreAddress, "id_store_address");

                entity.HasIndex(e => e.IdStorePhone, "id_store_phone");

                entity.Property(e => e.IdStore).HasColumnName("id_store");

                entity.Property(e => e.IdScheduleOpen).HasColumnName("id_schedule_open");

                entity.Property(e => e.IdStoreAddress).HasColumnName("id_store_address");

                entity.Property(e => e.IdStorePhone).HasColumnName("id_store_phone");

                entity.Property(e => e.StoreName)
                    .HasMaxLength(80)
                    .HasColumnName("store_name");

                entity.HasOne(d => d.IdStoreAddressNavigation)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.IdStoreAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("store_ibfk_1");

                entity.HasOne(d => d.IdStorePhoneNavigation)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.IdStorePhone)
                    .HasConstraintName("store_ibfk_2");
            });

            modelBuilder.Entity<StoreAddress>(entity =>
            {
                entity.HasKey(e => e.IdStoreAddress)
                    .HasName("PRIMARY");

                entity.ToTable("store_address");

                entity.HasIndex(e => e.IdStoreCity, "id_store_city");

                entity.Property(e => e.IdStoreAddress).HasColumnName("id_store_address");

                entity.Property(e => e.IdStoreCity).HasColumnName("id_store_city");

                entity.Property(e => e.StreetName)
                    .HasMaxLength(50)
                    .HasColumnName("street_name");

                entity.Property(e => e.StreetNumberCode)
                    .HasMaxLength(20)
                    .HasColumnName("street_number_code");

                entity.HasOne(d => d.IdStoreCityNavigation)
                    .WithMany(p => p.StoreAddresses)
                    .HasForeignKey(d => d.IdStoreCity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("store_address_ibfk_1");
            });

            modelBuilder.Entity<StoreCity>(entity =>
            {
                entity.HasKey(e => e.IdStoreCity)
                    .HasName("PRIMARY");

                entity.ToTable("store_city");

                entity.HasIndex(e => e.IdStoreCountry, "id_store_country");

                entity.Property(e => e.IdStoreCity).HasColumnName("id_store_city");

                entity.Property(e => e.CityName)
                    .HasMaxLength(50)
                    .HasColumnName("city_name");

                entity.Property(e => e.IdStoreCountry).HasColumnName("id_store_country");

                entity.HasOne(d => d.IdStoreCountryNavigation)
                    .WithMany(p => p.StoreCities)
                    .HasForeignKey(d => d.IdStoreCountry)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("store_city_ibfk_1");
            });

            modelBuilder.Entity<StoreCountry>(entity =>
            {
                entity.HasKey(e => e.IdStoreCountry)
                    .HasName("PRIMARY");

                entity.ToTable("store_country");

                entity.Property(e => e.IdStoreCountry).HasColumnName("id_store_country");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(50)
                    .HasColumnName("country_name");
            });

            modelBuilder.Entity<StorePhone>(entity =>
            {
                entity.HasKey(e => e.IdStorePhone)
                    .HasName("PRIMARY");

                entity.ToTable("store_phone");

                entity.HasIndex(e => e.PhoneValue, "phone_value")
                    .IsUnique();

                entity.Property(e => e.IdStorePhone).HasColumnName("id_store_phone");

                entity.Property(e => e.PhoneValue)
                    .HasMaxLength(50)
                    .HasColumnName("phone_value");
            });

            modelBuilder.Entity<WeekDay>(entity =>
            {
                entity.HasKey(e => e.IdWeekDay)
                    .HasName("PRIMARY");

                entity.ToTable("week_day");

                entity.Property(e => e.IdWeekDay).HasColumnName("id_week_day");

                entity.Property(e => e.Value)
                    .HasMaxLength(50)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<WorkPosition>(entity =>
            {
                entity.HasKey(e => e.IdWorkPosition)
                    .HasName("PRIMARY");

                entity.ToTable("work_position");

                entity.Property(e => e.IdWorkPosition).HasColumnName("id_work_position");

                entity.Property(e => e.PositionName)
                    .HasMaxLength(50)
                    .HasColumnName("position_name");

                entity.Property(e => e.StartDate).HasColumnName("start_date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        // public override void Dispose()
        // {
        //     base.Dispose();
        //     _logStream.Dispose();
        // }
        //
        // public override async ValueTask DisposeAsync()
        // {
        //     await base.DisposeAsync();
        //     await _logStream.DisposeAsync();
        // }
    }
}
