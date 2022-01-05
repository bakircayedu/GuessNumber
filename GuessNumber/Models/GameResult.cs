using GuessNumber.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuessNumber.Models
{
    public class GameResult
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(450)]
        public string? PlayerId { get; set; }
        [Required]
        [StringLength(450)]
        public string GamePlayId { get; set; }
        [Range(0, 2)]
        public int GamePoint { get; set; }
        public GuessNumberUser? Player { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Time { get; set; }

    }
}
