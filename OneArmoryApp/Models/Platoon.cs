using System;
using System.Collections.Generic;

namespace OneArmoryApp.Models
{
    public partial class Platoon
    {
        public Platoon()
        {
            Weapon = new HashSet<Weapon>();
        }

        public int PlatoonId { get; set; }
        public string Platoon1 { get; set; }

        public virtual ICollection<Weapon> Weapon { get; set; }
    }
}
