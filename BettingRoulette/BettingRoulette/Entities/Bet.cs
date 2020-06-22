using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BettingRoulette.Entities
{
    public partial class Bet
    {
        public Bet()
        {
            NumberBet = -1;
        }
        [Key]
        [Column(Order = 1, TypeName = "serial")]
        public long IdBet { get; set; }
        public long AmountBet { get; set; }
        public long IdRouletteBet { get; set; }
        public string idUserBet { get; set; }
        public int NumberBet { get; set; }
        public string ColorBet { get; set; }
    }
}