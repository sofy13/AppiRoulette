using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BettingRoulette.Entities
{
    public partial class Roulette
    {
        [Key]
        [Column(Order = 1, TypeName = "serial")]
        public long IdRoulette { get; set; }
        public string StateRoulette { get; set; }
    }
}
