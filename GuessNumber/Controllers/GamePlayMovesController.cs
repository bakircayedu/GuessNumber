#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GuessNumber.Data;
using GuessNumber.Models;
using Newtonsoft.Json;
using GuessNumber.ViewModels;
using GuessNumber.Service;
using GuessNumber.StateModels;

namespace GuessNumber.Controllers
{
    public class GamePlayMovesController : Controller
    {
        #region AspCoreAttribute
        private readonly AuthDbContext _context;
        private readonly IUserService userService;
        #endregion



        #region GameStateAttributes


        #endregion

        public GamePlayMovesController(AuthDbContext context, IUserService userService)
        {
            _context = context;
            this.userService = userService;

        }

        // GET: GamePlayMoves
        public async Task<IActionResult> Index()
        {
            return View(await _context.GamePlayMove.ToListAsync());
        }

        public async Task<IActionResult> GameResult()
        {

            return View(await GetGameResult());
        }

        private async Task<GameResultViewModel> GetGameResult()
        {
            string playerId = userService.GetUserId();

            var gr = await _context.GameResult.Where(g => g.PlayerId == playerId)
                  .OrderByDescending(O => O.Time).FirstOrDefaultAsync();
            string r = "";
            if (gr != null)
            {
                if (gr.GamePoint == 0)
                    r = "You Lose";
                else if (gr.GamePoint == 3)
                    r = "You Win";
                else
                    r = "You Draw";
            }
            return new GameResultViewModel()
            {
                GameResult = r
            };
        }



        // GET: GamePlayMoves/Create
        public async Task<IActionResult> Create()
        {
            var gpm = await GetOpponentGuess();
            if (gpm.Winner.Length > 0)
            {
                // TODO kazanma kaybetme ekranı yapılacak
                // puanlar yazılacak
                //db temizlenecek ++


                GameResult gr = new GameResult();
                MatchResponseViewModel vm = await GetMatchResponseViewModel();
                gr.GamePlayId = vm.Id;
                gr.PlayerId = userService.GetUserId();
                gr.Time = DateTime.Now;

                if (gpm.Winner.Equals("Player"))
                {
                    gr.GamePoint = 3;
                    gpm.GameResult = "You Win:)";
                }
                else if (gpm.Winner.Equals("Opponent"))
                {
                    gr.GamePoint = 0;
                    gpm.GameResult = "You Lose:(";
                }
                else  // berabere bitti....
                {
                    gr.GamePoint = 1;
                    gpm.GameResult = "Draw";
                }
                await AddMatchPoint(gr);
                //return View(gpm);
            }

            var dateTime = gpm.PlayerMoves.OrderByDescending(o => o.PlayTime).FirstOrDefault();

            if (dateTime != null)
            {
                int lastSec = 15;//-((int)((DateTime.Now.Ticks - dateTime.PlayTime.Ticks) / 1000));
                ViewBag.LastSecond = lastSec;
            }
            else
            {
                ViewBag.LastSecond = 15;
            }
            

            return View(gpm);
        }

        private async Task ClearDbAfterMatch()
        {
            MatchResponseViewModel vm = await GetMatchResponseViewModel();
            _context.GamePlayMove.RemoveRange(_context.GamePlayMove.Where(m => m.GamePlayId == vm.Id &&
                                                       m.PlayerId == userService.GetUserId()));

            await _context.SaveChangesAsync();
        }

        private async Task AddMatchPoint(GameResult gameResult)
        {
            await _context.GameResult.AddAsync(gameResult);
            await _context.SaveChangesAsync();
        }


        private async Task<GamePlayMoveViewModel> GetOpponentGuess()
        {

            string userId = userService.GetUserId();
            bool isFound = false;
            GamePlayMoveViewModel gpm = new GamePlayMoveViewModel();
            MatchResponseViewModel vm = await GetMatchResponseViewModel();
            while (!isFound)
            {
                await Task.Delay(1000);

                var opponentGpm = await _context.GamePlayMove.
                                    Where(m => m.GamePlayId == vm.Id &&
                                                        m.PlayerId == vm.OpponentId
                                                       ).OrderBy(o => o.TurnCount).ToListAsync();

                var playerGpm = await _context.GamePlayMove.
                                    Where(m => m.GamePlayId == vm.Id &&
                                                        m.PlayerId == userId
                                                       ).OrderBy(o => o.TurnCount).ToListAsync();
                if (playerGpm == null || opponentGpm == null || playerGpm.Count != opponentGpm.Count || playerGpm.Count < 1 || opponentGpm.Count < 1)
                    continue;

                gpm.GetViewModel(playerGpm, opponentGpm);
                isFound = true;
            }

            return gpm;
        }

        // POST: GamePlayMoves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayerNextGuessNumber")] GamePlayMoveViewModel gpVm)
        {
            GamePlayMove gamePlayMove = new GamePlayMove();
            MatchResponseViewModel vm = await GetMatchResponseViewModel();

            gamePlayMove.PlayerId = userService.GetUserId();
            gamePlayMove.GamePlayId = vm.Id;
            gamePlayMove.TurnCount = 0;
            gamePlayMove.PlayerMove = gpVm.PlayerNextGuessNumber;
            gamePlayMove.MoveTime = DateTime.Now; ;

            _context.Add(gamePlayMove);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }



        // GET: GamePlayMoves/Create
        public async Task<IActionResult> ChooseNumber()
        {
            string playerId = userService.GetUserId();
            MatchResponseViewModel vm = await GetMatchResponseViewModel();
            int lastSec = 15;// -((int)((DateTime.Now.Ticks - vm.RequestTime.Ticks) / 1000));

            ViewBag.LastSecond = lastSec;

            return View();
        }




        // POST: GamePlayMoves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChooseNumber([Bind("GuessNumber")] MoveModel moveModel)
        {

            string playerId = userService.GetUserId();
            GamePlayMove gamePlayMove = new GamePlayMove();
            MatchResponseViewModel vm = await GetMatchResponseViewModel();

            gamePlayMove.PlayerId = playerId;
            gamePlayMove.GamePlayId = vm.Id;
            gamePlayMove.TurnCount = 0;
            gamePlayMove.MoveTime = DateTime.Now; ;

            gamePlayMove.PlayerMove = moveModel.GuessNumber;

            _context.Add(gamePlayMove);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }



        private bool GamePlayMoveExists(int id)
        {
            return _context.GamePlayMove.Any(e => e.Id == id);
        }

        private async Task<MatchResponseViewModel> GetMatchResponseViewModel()
        {
            string playerId = userService.GetUserId();
            bool isFound = false;
            MatchResponse playerResponse = null;
            MatchResponse opponentResponse = null;


            while (!isFound)
            {       //&&(f => f.Player1 == playerId || f.Player2 == playerId)
                await Task.Delay(1000);
                playerResponse = await _context.MatchResponse
                    .FirstOrDefaultAsync(f => f.PlayerId == playerId);
                if (playerResponse == null)
                    continue;

                opponentResponse = await _context.MatchResponse
                   .FirstOrDefaultAsync(f => f.GamePlayId == playerResponse.GamePlayId && f.PlayerId != playerId);
                if (playerResponse == null)
                    continue;


                isFound = true;
            }

            MatchResponseViewModel vm = new MatchResponseViewModel()
            {
                Id = playerResponse.GamePlayId,
                OpponentId = opponentResponse.PlayerId,
                PlayerQuee = "",
                RequestTime = playerResponse.RequestTime,
                ResponseTime = DateTime.Now
            };
            return vm;
        }
    }
}
