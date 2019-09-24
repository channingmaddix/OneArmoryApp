using System;
using System.Collections.Generic;

namespace OneArmoryApp.Models
{
    public partial class Paygrade
    {
        public Paygrade()
        {
            Soldier = new HashSet<Soldier>();
        }

        public int PaygradeId { get; set; }
        public string Paygrade1 { get; set; }

        public virtual ICollection<Soldier> Soldier { get; set; }
    }
}
