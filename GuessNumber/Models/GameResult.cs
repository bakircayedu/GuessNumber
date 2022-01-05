﻿using GuessNumber.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuessNumber.Models
{
    public class GameResult
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? GuessNumberUserId { get; set; }
        [Required]
        public int MathcResponseId { get; set; }
        [Range(0, 2)]
        public int GamePoint { get; set; }
        public MatchResponse? MatchResponse { get; set; }
        public GuessNumberUser? Player { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Time { get; set; }

    }
}