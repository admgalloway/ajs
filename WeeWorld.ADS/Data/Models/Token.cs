using System;
using System.Collections.Generic;
using System.Linq;

namespace WeeWorld.ADS.Data.Models
{
    public class Token : IModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime ExpiryDate { get; set; }

        public virtual User User { get; set; }

        public bool Expired 
        { 
            get 
            {
                return ExpiryDate < DateTime.Now;
            } 
        }

        public bool Admin
        {
            get
            {
                return (User == null) ? false : User.IsAdmin;
            }
        }

    }
}
