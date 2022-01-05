using GuessNumber.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuessNumber.Models
{
    public class GamePlayMove
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int MatchResponseId { get; set; }
        [Required]
        [StringLength(450)]
        public string? PlayerId { get; set; }
        [Required]
        [Range(100, 999)]
        public int PlayerMove { get; set; } = 0;
        [Required]
        public int TurnCount { get; set; } = 0;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime MoveTime { get;set; }
        public MatchResponse? MatchResponse { get; set; }
        public GuessNumberUser? Player { get; set; }

    }
}
