namespace GuessNumber.ViewModels
{
    public class MatchResponseViewModel
    {

        public string Id { get; set; }
        public string? OpponentId { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime ResponseTime { get; set; }  
        public string? PlayerQuee { get; set; }
    }
}
