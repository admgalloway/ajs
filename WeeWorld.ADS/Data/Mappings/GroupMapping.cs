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
    public class GroupMapping : EntityTypeConfiguration<Group>
    {
        public GroupMapping()
        {
            Property(u => u.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}