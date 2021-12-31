using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuessNumber.Models
{
    public class GamePlayMove
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int MatchId { get; set; }
        [Required]
        public string? PlayerId { get; set; }
        [Required]
        [Range(100, 999)]
        public int PlayerMove { get; set; } = 0;
        [Required]
        public int TurnCount { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime MoveTime { get;set; }


        public MatchResponse? MatchResponse { get; set; }
        
    }
}
