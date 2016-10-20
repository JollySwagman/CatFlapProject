using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace CatFlap.Models
{
    //public class Flap
    //{
    //    public string Name { get; set; }

    //    public Flap()
    //    {
    //        this.Name = "CatFlap V0.1";
    //    }

    //    public static void Save()
    //    {
    //        using (var ctx = new LogContext())
    //        {
    //            Passage stud = new Passage(Passage.DirectionType.IN);
    //            ctx.Passages.Add(stud);
    //            ctx.SaveChanges();
    //        }
    //    }
    //}

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

        public DateTime? PassageTimeAEST { get; private set; }

        public Passage()
        {
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
            var AEST = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi);

            this.PassageTimeAEST = AEST;
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

        public DbSet<Passage> Passages { get; set; }
        //public DbSet<Standard> Standards { get; set; }
    }
}
