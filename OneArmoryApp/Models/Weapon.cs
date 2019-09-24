using System;
using System.Collections.Generic;

namespace OneArmoryApp.Models
{
    public partial class Weapon
    {
        public Weapon()
        {
            Soldier = new HashSet<Soldier>();
            WorkOrder = new HashSet<WorkOrder>();
        }

        public int WeaponId { get; set; }
        public string Serial { get; set; }
        public string Nomenclature { get; set; }
        public string EquipmentType { get; set; }
        public string Platoon { get; set; }
        public DateTime? ArrivalDate { get; set; }

        public virtual EquipmentType EquipmentTypeNavigation { get; set; }
        public virtual Nomenclature NomenclatureNavigation { get; set; }
        public virtual Platoon PlatoonNavigation { get; set; }
        public virtual ICollection<Soldier> Soldier { get; set; }
        public virtual ICollection<WorkOrder> WorkOrder { get; set; }
    }
}
