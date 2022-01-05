using GuessNumber.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuessNumber.Models
{
    public class MatchResponse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(450)]
        [Required]
        public string? PlayerId { get; set; }

        [Required]
        [StringLength(450)]
        public string GamePlayId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime RequestTime { get; set; }
        public GuessNumberUser? Player { get; set; }
    }
}
