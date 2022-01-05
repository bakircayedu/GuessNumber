using GuessNumber.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuessNumber.Models
{
    public class MatchResponse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Player1 { get; set; }
        public string? Player2 { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime RequestTime { get; set; }
    }
}
