using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace CatFlap.Models
{
    public class Passage
    {
        public enum DirectionType

        {
            IN,
            OUT
        }

        public DirectionType Direction { get; set; }

        [Key]
        public int id { get; set; }

        //public DateTime? PassageTimeAEST { get; private set; }

        public Passage()
        {
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
            var AEST = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi);

            //this.PassageTimeAEST = AEST;
        }

        public Passage(DirectionType direction)
        {
            this.Direction = direction;
            //this.Name = "CatFlap V0.1";
        }
    }

    public class LogContext : DbContext
    {
        public LogContext() : base()
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<LogContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Passage> Passages { get; set; }
        //public DbSet<Standard> Standards { get; set; }
    }
}
