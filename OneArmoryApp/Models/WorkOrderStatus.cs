using System;
using System.Collections.Generic;

namespace OneArmoryApp.Models
{
    public partial class WorkOrderStatus
    {
        public WorkOrderStatus()
        {
            WorkOrder = new HashSet<WorkOrder>();
        }

        public int WorkOrderStatusId { get; set; }
        public string WorkOrderStatus1 { get; set; }

        public virtual ICollection<WorkOrder> WorkOrder { get; set; }
    }
}
