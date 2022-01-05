using GuessNumber.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuessNumber.Models
{
    public class MatchRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? PlayerId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime RequestTime { get; set; }

        public int? IsPlayerRequestHandled { get; set; } = 0;

        public GuessNumberUser? Player { get; set; }
    }
}
