namespace SMD.Models.DomainModels
{
    public class Game
    {
        public long GameId { get; set; }
        public string GameName { get; set; }
        public bool? Status { get; set; }
        public int? AgeRangeStart { get; set; }
        public int? AgeRangeEnd { get; set; }
        public int? GameType { get; set; }
        public int? Complexity { get; set; }
        public string GameUrl { get; set; }

        public string GameSmallImage { get; set; }
        public string GameLargeImage { get; set; }
    }
}
