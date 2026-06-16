using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_System_2._0.Models;
using Travel_Agency_System_2._0.Enums;

namespace Travel_Agency_System_2._0.sql_connection
{
    public class TravelAgencyDbContext : DbContext
    {
        public TravelAgencyDbContext()
        {
        }

        public TravelAgencyDbContext(DbContextOptions<TravelAgencyDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ExtraService> ExtraServices { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TravelAgencyDb2.0;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Client)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Trip)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.TripId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .Property(b => b.Status)
                .HasDefaultValue(BookingStatus.Pending)
                .HasSentinel(BookingStatus.Pending);

            modelBuilder.Entity<Booking>()
                .Property(b => b.FinalPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ExtraService>()
                .HasOne(e => e.Booking)
                .WithMany(b => b.ExtraServices)
                .HasForeignKey(e => e.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExtraService>()
                .Property(e => e.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Trip>()
                .Property(t => t.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Trip>()
                .Property(t => t.BasePrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.EmailAddress)
                .IsUnique();

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Booking>()
            .HasOne(b => b.Trip)
            .WithMany(t => t.Bookings) 
            .HasForeignKey(b => b.TripId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}