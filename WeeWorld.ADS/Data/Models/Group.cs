using System;
using System.Collections.Generic;
using WeeWorld.ADS.Data.Enums;

namespace WeeWorld.ADS.Data.Models
{

    public class Group : IModel
    {
        public Group()
        {
            Applications = new List<Application>();
            Users = new List<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public AlertStatus AlertStatus { get; set; }

        public virtual IList<Application> Applications { get; set; }
        public virtual IList<User> Users { get; set; }

    }
}
