using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WeeWorld.ADS.Data.Models;

namespace WeeWorld.ADS.Data.Mappings
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            Property(u => u.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasMany(u => u.Groups)
                .WithMany(g => g.Users)
                .Map(m => m.ToTable("GroupUsers")
                    .MapLeftKey("UserId")
                    .MapRightKey("GroupId"));
        }
    }
}