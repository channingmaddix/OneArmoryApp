using System;
using System.Collections.Generic;

namespace OneArmoryApp.Models
{
    public partial class WeaponStatus
    {
        public WeaponStatus()
        {
            WorkOrder = new HashSet<WorkOrder>();
        }

        public int WeaponStatusId { get; set; }
        public string WeaponStatus1 { get; set; }

        public virtual ICollection<WorkOrder> WorkOrder { get; set; }
    }
}
