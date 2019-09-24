using System;
using System.Collections.Generic;

namespace OneArmoryApp.Models
{
    public partial class Nomenclature
    {
        public Nomenclature()
        {
            Weapon = new HashSet<Weapon>();
        }

        public int NomenclatureId { get; set; }
        public string Nomenclature1 { get; set; }

        public virtual ICollection<Weapon> Weapon { get; set; }
    }
}
