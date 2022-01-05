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

        public string? GameResult { set; get; } = "";


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


            if (opponentMoves.Count > 1)
            {
                var lastOpponentMove = opponentMoves[^1];
                if (lastOpponentMove != null&& lastOpponentMove.PlayerMove == playerMoves[0].PlayerMove)
                    Winner = "Opponent";

                var lastPlayerMove = playerMoves[^1];
                if (lastPlayerMove != null && lastPlayerMove.PlayerMove == opponentMoves[0].PlayerMove)
                    Winner+="Player";

                //if (Winner.Length > 0)
                //    //return this;

            }
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
