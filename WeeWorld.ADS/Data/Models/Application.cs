using System;
using System.Collections.Generic;
using System.Linq;

namespace WeeWorld.ADS.Data.Models
{
    public class Application : IModel
    {
        public Application()
        {
            this.Groups = new List<Group>();
            this.Builds = new List<Build>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public string Platform { get; set; }

        public virtual IList<Group> Groups { get; set; }
        public virtual IList<Build> Builds { get; set; }

        public IEnumerable<User> Users
        {
            get
            {
                return Groups.SelectMany(g => g.Users).Distinct();
            }
        }


    }
}
