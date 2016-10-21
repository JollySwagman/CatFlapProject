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

        [Key]
        public int ID { get; set; }

        public DirectionType Direction { get; set; }

        public string Message { get; set; }

        public DateTime? PassageTimeAEST { get; set; }

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

}
