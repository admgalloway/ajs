using System;
using System.Collections.Generic;
using System.Linq;
using WeeWorld.ADS.Data.Atrtibutes;

namespace WeeWorld.ADS.Data.Models
{
    public class User : IModel
    {
        public User()
        {
            this.Groups = new List<Group>();
        }

        public int Id { get; set; }
        public string EmailAddress { get; set; }

        [HideValue]
        public string Password { get; set; }

        [HideValue]
        public byte[] Salt { get; set; }
        
        public DateTime DateCreated { get; set; }
        public virtual IList<Group> Groups { get; set; }
        public virtual IList<Token> Tokens { get; set; }

        public IEnumerable<Application> Applications
        {
            get
            {
                return Groups.SelectMany(g => g.Applications).Distinct();
            }
        }

        public bool IsInGroup(string name)
        {
            return Groups.Select(g => g.Name.ToLower()).Contains(name.ToLower());
        }

        public bool IsAdmin
        {
            get
            {
                return IsInGroup("Administrators");
            }
        }

    }
}
