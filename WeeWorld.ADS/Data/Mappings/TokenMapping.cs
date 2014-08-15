using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WeeWorld.ADS.Data.Models;

namespace WeeWorld.ADS.Data.Mappings
{
    public class TokenMapping : EntityTypeConfiguration<Token>
    {
        public TokenMapping()
        {
            HasRequired(p => p.User)
                .WithMany(b => b.Tokens).Map(f => f.MapKey("UserId"));
        }
    }
}