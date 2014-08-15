using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WeeWorld.ADS.Data.Models;

namespace WeeWorld.ADS.Data.Mappings
{
    public class ApplicationMapping : EntityTypeConfiguration<Application>
    {
        public ApplicationMapping()
        {
            Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasMany(a => a.Groups)
                .WithMany(g => g.Applications)
                    .Map(m => m.ToTable("GroupApplications")
                    .MapLeftKey("ApplicationId")
                    .MapRightKey("GroupId"));

            // Ignore(a => a.Builds);
        }
    }
}