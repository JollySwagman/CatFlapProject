
using CatFlap.Persistence;
using System;
using System.Data.Entity.Migrations;

namespace CatFlap.Migrations

{
    internal sealed class Configuration : DbMigrationsConfiguration<LogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LogContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.

            //context.Passages.AddOrUpdate(
            //  p => p.ID,
            //  new Models.Passage(Models.Passage.DirectionType.IN),
            //  new Models.Passage(Models.Passage.DirectionType.OUT),
            //  new Models.Passage(Models.Passage.DirectionType.IN),
            //  new Models.Passage(Models.Passage.DirectionType.OUT)
            //);
        }
    }
}
