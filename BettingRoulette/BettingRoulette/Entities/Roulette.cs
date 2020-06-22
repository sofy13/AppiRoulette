using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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