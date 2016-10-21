using System;
using System.Linq;
using System.Collections.Generic;
using CatFlap.Persistence;

namespace CatFlap.Models
{
    public class CatFlapData
    {
        public static void Save(Passage passage)
        {
            using (var ctx = new LogContext())
            {
                //Passage stud = new Passage(Passage.DirectionType.IN);
                ctx.Passages.Add(passage);
                ctx.SaveChanges();
            }
        }

        public static IList<Passage> GetAll()
        {
            using (var ctx = new LogContext())
            {
                //Passage stud = new Passage(Passage.DirectionType.IN);
                return ctx.Passages.ToList();
            }
        }

    }
}
