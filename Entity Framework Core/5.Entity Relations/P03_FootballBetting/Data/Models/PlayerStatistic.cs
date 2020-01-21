namespace P03_FootballBetting.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PlayerStatistic
    {
        //Mapping Table

        public int GameId { get; set; }

        public Game Game { get; set; }

        public int PlayerId { get; set; }

        public Player Player { get; set; }

        public int ScoredGoals { get; set; }

        public int Assists { get; set; }

        public int MinutesPlayed { get; set; }
    }
}
