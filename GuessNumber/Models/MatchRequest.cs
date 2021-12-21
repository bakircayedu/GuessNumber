namespace GuessNumber.Models
{
    public class MatchRequest
    {

        public int Id { get; set; }
        public string? PlayerId { get; set; }
        public DateTime RequestTime { get; set; }

    }
}
