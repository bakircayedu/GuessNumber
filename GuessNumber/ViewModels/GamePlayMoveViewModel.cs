using GuessNumber.Models;
using GuessNumber.StateModels;
using System.ComponentModel.DataAnnotations;

namespace GuessNumber.ViewModels
{
    public class GamePlayMoveViewModel
    {
        public List<MoveModel> PlayerMoves { get; set; }

        public List<MoveModel> OpponentMoves { get; set; }

        public int LastCountTime { get; set; }

        public int TurnCount { get; set; }

        [Range(100, 999)]
        public int PlayerNextGuessNumber { get; set; }

        public string Winner { get; set; } = "";


        public GamePlayMoveViewModel GetViewModel(List<GamePlayMove> playerMoves, List<GamePlayMove> opponentMoves)
        {
            if (PlayerMoves == null)
                PlayerMoves = new List<MoveModel>();
            else
                PlayerMoves.Clear();

            if (OpponentMoves == null)
                OpponentMoves = new List<MoveModel>();
            else
                OpponentMoves.Clear();

            var lastOpponentMove = opponentMoves.OrderByDescending(o => o.MoveTime).LastOrDefault();
            if (lastOpponentMove != null && lastOpponentMove.PlayerMove == playerMoves.First().PlayerMove)
                Winner = "Opponent";

            var lastPlayerMove = playerMoves.OrderByDescending(o => o.MoveTime).LastOrDefault();
            if (lastPlayerMove != null && lastPlayerMove.PlayerMove == opponentMoves.First().PlayerMove)
                Winner.Concat("Player");

            if (Winner.Length > 0)
                return this;


            foreach (var item in opponentMoves)
            {
                MoveModel model = new MoveModel();
                model.GuessNumber = playerMoves.First().PlayerMove;
                model.Number = item.PlayerMove;
                model.FindHint();
                model.PlayTime = item.MoveTime;
                OpponentMoves.Add(model);
            }

            foreach (var item in playerMoves)
            {
                MoveModel model = new MoveModel();
                model.GuessNumber = opponentMoves.First().PlayerMove;
                model.Number = item.PlayerMove;
                model.FindHint();
                model.PlayTime = item.MoveTime;
                PlayerMoves.Add(model);
            }

            TurnCount = playerMoves.Count;
            return this;
        }
    }
}
