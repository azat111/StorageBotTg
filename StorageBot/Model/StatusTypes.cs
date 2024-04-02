using System;
using System.Collections.Generic;
namespace StorageBot.Model
{
    public partial class StatusTypes
    {
        public StatusTypes()
        {
            Catering = new HashSet<Catering>();
            Orders = new HashSet<Orders>();
        }

        public int IdStatus { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Catering> Catering { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
