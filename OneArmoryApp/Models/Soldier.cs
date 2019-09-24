using System;
using System.Collections.Generic;

namespace OneArmoryApp.Models
{
    public partial class Soldier
    {
        public int SoldierId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Paygrade { get; set; }
        public int? WeaponId { get; set; }
        public long? DoDid { get; set; }
        public DateTime? ArrivalDate { get; set; }

        public virtual Paygrade PaygradeNavigation { get; set; }
        public virtual Weapon Weapon { get; set; }
    }
}
