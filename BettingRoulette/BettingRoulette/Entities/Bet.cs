using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BettingRoulette.Entities
{
    public partial class Bet
    {
        public Bet()
        {
            NumberBet = -1;
            StateBet = true;
        }

        [Key]
        [Column(Order = 1, TypeName = "serial")]
        public long IdBet { get; set; }
        public long AmountBet { get; set; }
        public long IdRouletteBet { get; set; }
        public string idUserBet { get; set; }
        public int NumberBet { get; set; }
        public string ColorBet { get; set; }
        public bool StateBet { get; set; }
    }
}
