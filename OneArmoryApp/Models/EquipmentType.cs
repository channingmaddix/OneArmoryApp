using System;
using System.Collections.Generic;

namespace OneArmoryApp.Models
{
    public partial class EquipmentType
    {
        public EquipmentType()
        {
            Weapon = new HashSet<Weapon>();
        }

        public int EquipmentTypeId { get; set; }
        public string EquipmentType1 { get; set; }

        public virtual ICollection<Weapon> Weapon { get; set; }
    }
}
