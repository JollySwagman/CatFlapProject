using CatFlap.Models;
using System;
using System.Data.Entity;

namespace CatFlap.Persistence
{
    public class LogContext : DbContext    {
        public LogContext() : base()
        {        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)

        {            //Database.SetInitializer<LogContext>(null);            base.OnModelCreating(modelBuilder);        }
        public DbSet<Passage> Passages { get; set; }
        //public DbSet<Standard> Standards { get; set; }
    }}
