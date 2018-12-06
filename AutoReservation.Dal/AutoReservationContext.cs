﻿using System.Configuration;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace AutoReservation.Dal
{
    public class AutoReservationContext
        : DbContext
    {
        public DbSet<Auto> Autos { get; set; }

        public DbSet<Kunde> Kunden { get; set; }

        public DbSet<Reservation> Reservationen { get; set; }


        public static readonly LoggerFactory LoggerFactory = new LoggerFactory(
            new[] {new ConsoleLoggerProvider((_, logLevel) => logLevel >= LogLevel.Information, true)}
        );

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .EnableSensitiveDataLogging()
                    .UseLoggerFactory(LoggerFactory) // Warning: Do not create a new ILoggerFactory instance each time
                    .UseSqlServer(ConfigurationManager.ConnectionStrings[nameof(AutoReservationContext)]
                        .ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auto>()
                .ToTable("Auto", "dbo")
                .HasDiscriminator<int>("AutoKlasse")
                .HasValue<LuxusklasseAuto>(0)
                .HasValue<MittelklasseAuto>(1)
                .HasValue<StandardAuto>(2)
                ;
                
            modelBuilder.Entity<Kunde>().ToTable("Kunde", schema: "dbo");
            modelBuilder.Entity<Reservation>().ToTable("Reservation", schema: "dbo");
        }
    }
}