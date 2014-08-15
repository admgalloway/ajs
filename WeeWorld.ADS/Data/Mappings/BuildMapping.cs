using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WeeWorld.ADS.Data.Models;

namespace WeeWorld.ADS.Data.Mappings
{
    public class BuildMapping : EntityTypeConfiguration<Build>
    {
        public BuildMapping()
        {
            HasRequired(p => p.Application)
                .WithMany(b => b.Builds).Map(f => f.MapKey("ApplicationId"));
        }
    }
}