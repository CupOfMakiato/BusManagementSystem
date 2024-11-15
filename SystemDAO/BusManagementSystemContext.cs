using BusinessObject.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SystemDAO;

public partial class BusManagementSystemContext : DbContext
{
    public BusManagementSystemContext()
    {
    }

    public BusManagementSystemContext(DbContextOptions<BusManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Bus> Buses { get; set; }

    public virtual DbSet<BusStop> BusStops { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<FreeTicket> FreeTickets { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<RouteBusStop> RouteBusStops { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951AED3229B857");

            entity.ToTable("Booking");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BookingDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("FK__Booking__TicketI__6383C8BA");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Booking__UserId__6477ECF3");
        });

        modelBuilder.Entity<Bus>(entity =>
        {
            entity.HasKey(e => e.BusId).HasName("PK__Bus__6A0F60B56AAD9631");

            entity.ToTable("Bus");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AssignedRoute).WithMany(p => p.Buses)
                .HasForeignKey(d => d.AssignedRouteId)
                .HasConstraintName("FK__Bus__AssignedRou__656C112C");

            entity.HasOne(d => d.Driver).WithMany(p => p.Buses)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK__Bus__DriverId__66603565");
        });

        modelBuilder.Entity<BusStop>(entity =>
        {
            entity.HasKey(e => e.StopId).HasName("PK__BusStop__EB6A38F4C668ADBB");

            entity.ToTable("BusStop");

            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.StopName).HasMaxLength(100);
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("PK__Driver__F1B1CD04B89DE3EC");

            entity.ToTable("Driver");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);

            entity.HasOne(d => d.Role).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Driver__RoleId__6754599E");
        });

        modelBuilder.Entity<FreeTicket>(entity =>
        {
            entity.HasKey(e => e.FreeTicketId).HasName("PK__FreeTick__432E9E7BC4C898BA");

            entity.ToTable("FreeTicket");

            entity.Property(e => e.District)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IdbackImage).HasColumnName("IDBackImage");
            entity.Property(e => e.IdfrontImage).HasColumnName("IDFrontImage");
            entity.Property(e => e.Idnumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IDNumber");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.RecipientName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RecipientType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TicketDeliveryAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Ward)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Ticket).WithMany(p => p.FreeTickets)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FreeTicke__Ticke__68487DD7");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A38DA1A13BB");

            entity.ToTable("Payment");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__Payment__Booking__693CA210");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Payment__UserId__6A30C649");
        });

        modelBuilder.Entity<PaymentDetail>(entity =>
        {
            entity.HasKey(e => e.PaymentDetailId).HasName("PK__PaymentD__7F4E340F680D596B");

            entity.ToTable("PaymentDetail");

            entity.Property(e => e.Description).HasMaxLength(255);

            entity.HasOne(d => d.Payment).WithMany(p => p.PaymentDetails)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__PaymentDe__Payme__6B24EA82");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A3F06434F");

            entity.ToTable("Role");

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.RouteId).HasName("PK__Route__80979B4D77B39E7D");

            entity.ToTable("Route");

            entity.Property(e => e.Distance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EndLocation).HasMaxLength(100);
            entity.Property(e => e.RouteName).HasMaxLength(100);
            entity.Property(e => e.StartLocation).HasMaxLength(100);
        });

        modelBuilder.Entity<RouteBusStop>(entity =>
        {
            entity.HasKey(e => e.StopId).HasName("PK__RouteBus__EB6A38F4B004A589");

            entity.ToTable("RouteBusStop");

            entity.Property(e => e.StopId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Ticket__712CC607A5E84EED");

            entity.ToTable("Ticket");

            entity.Property(e => e.IdcardBack).HasColumnName("IDCardBack");
            entity.Property(e => e.IdcardFront).HasColumnName("IDCardFront");
            entity.Property(e => e.IsFreeTicket).HasDefaultValue(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PriorityType).HasMaxLength(50);
            entity.Property(e => e.TicketType).HasMaxLength(50);

            entity.HasOne(d => d.Route).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.RouteId)
                .HasConstraintName("FK__Ticket__RouteId__6C190EBB");

            entity.HasOne(d => d.User).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Ticket__UserId__6D0D32F4");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C47BB0514");

            entity.ToTable("User");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(11);
            entity.Property(e => e.PhoneNumber).HasMaxLength(11);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__User__RoleId__6E01572D");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}