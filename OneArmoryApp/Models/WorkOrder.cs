using System;
using System.Collections.Generic;

namespace OneArmoryApp.Models
{
    public partial class WorkOrder
    {
        public int WorkOrderId { get; set; }
        public int? WeaponId { get; set; }
        public string FaultDesc { get; set; }
        public string WeaponStatus { get; set; }
        public string WorkOrderStatus { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Weapon Weapon { get; set; }
        public virtual WeaponStatus WeaponStatusNavigation { get; set; }
        public virtual WorkOrderStatus WorkOrderStatusNavigation { get; set; }
    }
}
