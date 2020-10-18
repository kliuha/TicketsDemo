using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.EF
{
    public class TicketsContext : DbContext
    {
        public DbSet<Train> Trains { get; set; }
        public DbSet<Place> Places {get;set;}
        public DbSet<Carriage> Carriages { get; set; }
        
        public DbSet<Run> Runs {get;set;}
        public DbSet<PlaceInRun> PlacesInRuns {get;set;}
        public DbSet<Reservation> Reservations {get;set;}
        public DbSet<Ticket> Tickets {get;set; }

        public DbSet<PriceComponent> PriceComponents { get; set; }

        public TicketsContext() : base()
        {
            Database.Log = (string logMe) => Debug.WriteLine(logMe);
            Configuration.LazyLoadingEnabled = true;
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Train>().HasMany(t => t.Carriages).WithRequired(c => c.Train);
            modelBuilder.Entity<Train>().HasMany(t => t.Runs).WithRequired(c => c.Train).WillCascadeOnDelete(false);

            modelBuilder.Entity<Carriage>().HasMany(c => c.Places).WithRequired(p => p.Carriage);
            
            modelBuilder.Entity<PlaceInRun>().HasRequired(p => p.Run).WithMany(r => r.Places);

            modelBuilder.Entity<Ticket>().HasKey(x => x.ReservationId);
            modelBuilder.Entity<Ticket>().HasMany(x => x.PriceComponents).WithRequired(x => x.Ticket).WillCascadeOnDelete(false);

            modelBuilder.Entity<Reservation>().HasOptional(x => x.Ticket).WithRequired(x => x.Reservation).WillCascadeOnDelete(false);
            modelBuilder.Entity<Reservation>().HasRequired(x => x.PlaceInRun).WithMany(x => x.Reservations).WillCascadeOnDelete(false);
        } 

    }
}
