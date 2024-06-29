using dtaquito_backend_web_app.Payments.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Reservations.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Configuration.Extensions;
using dtaquito_backend_web_app.Users.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using dtaquito_backend_web_app.SportSpaces.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.Entities;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.ValueObjects;
using dtaquito_backend_web_app.Users.Domain.Model.Entities;
using dtaquito_backend_web_app.Users.Domain.Model.ValueObjects;

namespace dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Configuration;

public class AppDBContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Payment> Payments { get; set; }
    
    public DbSet<Plan> Plans { get; set; }

    public DbSet<Suscription> Suscriptions { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
       
        
        base.OnModelCreating(builder);
        
        builder.Entity<User>().ToTable("users");
        builder.Entity<User>().HasKey(f => f.Id);
        builder.Entity<User>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(f => f.Name).IsRequired().HasMaxLength(100);
        builder.Entity<User>().Property(f => f.Password).IsRequired().HasMaxLength(100);
        builder.Entity<User>().HasOne(f => f.Role).WithMany(f => f.Users).HasForeignKey(f => f.RoleId);
        builder.Entity<Role>().HasData(
            Enum.GetValues(typeof(RoleTypes))
                .Cast<RoleTypes>()
                .Select(e => new Role
                {
                    Id = (int)e,
                    Type = e.ToString()
                })
        );

        builder.Entity<SportSpace>().ToTable("sport_spaces");
        builder.Entity<SportSpace>().HasKey(f => f.Id);
        builder.Entity<SportSpace>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<SportSpace>().Property(f => f.Name).IsRequired().HasMaxLength(100);
        builder.Entity<SportSpace>().Property(f => f.ImageUrl).IsRequired().HasMaxLength(100);
        builder.Entity<SportSpace>().Property(f => f.Price).IsRequired();
        builder.Entity<SportSpace>().Property(f => f.Description).IsRequired().HasMaxLength(100);
        builder.Entity<SportSpace>().Property(f => f.UserId).IsRequired();
        builder.Entity<SportSpace>().Property(f => f.StartTime).IsRequired().HasMaxLength(100);
        builder.Entity<SportSpace>().Property(f => f.EndTime).IsRequired().HasMaxLength(100);
        builder.Entity<SportSpace>().Property(f => f.Rating).IsRequired();
        builder.Entity<SportSpace>().HasOne(f => f.User).WithMany().HasForeignKey(f => f.UserId);

        builder.Entity<Suscription>().ToTable("suscriptions");
        builder.Entity<Suscription>().HasKey(f => f.Id);
        builder.Entity<Suscription>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Suscription>().Property(f => f.Plan).IsRequired().HasConversion<string>(); // Cambia el tipo de Plan a PlanTypes y conviértelo a string para almacenarlo en la base de datos
        builder.Entity<Suscription>().Property(f => f.UserId).IsRequired();
        builder.Entity<Suscription>().HasOne(f => f.User).WithMany().HasForeignKey(f => f.UserId);
        builder.Entity<Plan>().HasData(Enum.GetValues(typeof(PlanTypes)).Cast<PlanTypes>().Select(e => new Plan
        {
            Id = (int)e,
            Type = e.ToString()
        })
        );
        
        builder.Entity<Payment>().ToTable("payments");
        builder.Entity<Payment>().HasKey(f => f.Id);
        builder.Entity<Payment>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Payment>().Property(f => f.CardNumber).IsRequired().HasMaxLength(100);
        builder.Entity<Payment>().Property(f => f.ExpirationDate).IsRequired();
        builder.Entity<Payment>().Property(f => f.CardHolder).IsRequired().HasMaxLength(100);
        builder.Entity<Payment>().Property(f => f.CardIssuer).IsRequired().HasMaxLength(100);
        builder.Entity<Payment>().Property(f => f.CVV).IsRequired().HasMaxLength(100);
        builder.Entity<Payment>().Property(f => f.UserId).IsRequired();
        builder.Entity<Payment>().Property(f => f.Balance).IsRequired();
        builder.Entity<Payment>().HasOne(f => f.User).WithMany().HasForeignKey(f => f.UserId);
        
        builder.Entity<Reservation>().ToTable("reservations");
        builder.Entity<Reservation>().HasKey(f => f.Id);
        builder.Entity<Reservation>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Reservation>().Property(f => f.UserId).IsRequired();
        builder.Entity<Reservation>().Property(f => f.SportSpaceId).IsRequired();
        builder.Entity<Reservation>().Property(f => f.Time).IsRequired();
        builder.Entity<Reservation>().HasOne(f => f.User).WithMany().HasForeignKey(f => f.UserId);
        builder.Entity<Reservation>().HasOne(f => f.SportSpace).WithMany().HasForeignKey(f => f.SportSpaceId);
        

        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
}