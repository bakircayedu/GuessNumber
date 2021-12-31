namespace GuessNumber.ViewModels
{
    public class MatchResponseViewModel
    {

        public int Id { get; set; }
        public string? Player1 { get; set; }
        public string? Player2 { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime ResponseTime { get; set; }  
        public string? PlayerQuee { get; set; }
    }
}
