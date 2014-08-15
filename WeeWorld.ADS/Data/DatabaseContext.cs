
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WeeWorld.ADS.Data.Mappings;
using WeeWorld.ADS.Data.Models;

namespace WeeWorld.ADS.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("AdsDb")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Build> Builds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMapping());
            modelBuilder.Configurations.Add(new ApplicationMapping());
            modelBuilder.Configurations.Add(new GroupMapping());
            modelBuilder.Configurations.Add(new TokenMapping());
            modelBuilder.Configurations.Add(new BuildMapping());
        }
    }
}