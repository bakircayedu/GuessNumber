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
        private static MatchResponseViewModel matchResponseViewModel;
        private static Dictionary<string, List<MoveModel>> PlayersMoves;
        #endregion

        public GamePlayMovesController(AuthDbContext context, IUserService userService)
        {
            _context = context;
            this.userService = userService;
            PlayersMoves = new Dictionary<string, List<MoveModel>>()
            {
                {"Player",new List<MoveModel>() },
                {"Opponent",new List<MoveModel>() }
            };

        }

        // GET: GamePlayMoves
        public async Task<IActionResult> Index()
        {
            return View(await _context.GamePlayMove.ToListAsync());
        }



        // GET: GamePlayMoves/Create
        public async Task<IActionResult> Create()
        {
            var gpm = await GetOpponentGuess();
            if (gpm.Winner.Length > 0)
            {
                // TODO kazanma kaybetme ekranı yapılacak
                // puanlar yazılacak
                //db temizlenecek 

                if (gpm.Winner.Equals("Player"))
                    ViewBag.Winner = 1;
                else if (gpm.Winner.Equals("Opponent"))
                    ViewBag.Winner = 0;
                else
                    ViewBag.Winner = 2;
                return View(gpm);
            }

            var dateTime = gpm.PlayerMoves.OrderByDescending(o => o.PlayTime).FirstOrDefault();

            int lastSec = (int)((DateTime.Now.Ticks - dateTime.PlayTime.Ticks) / 1000);

            ViewBag.LastSecond = lastSec;

            return View(gpm);
        }



        private async Task<GamePlayMoveViewModel> GetOpponentGuess()
        {
            bool isFound = false;
            GamePlayMoveViewModel gpm = new GamePlayMoveViewModel();
            while (!isFound)
            {
                await Task.Delay(1000);

                var opponentGpm = await _context.GamePlayMove.
                                    Where(m => m.MatchId == matchResponseViewModel.Id &&
                                                        m.PlayerId == matchResponseViewModel.OpponentId 
                                                       ).OrderBy(o=>o.TurnCount).ToListAsync();

                var playerGpm = await _context.GamePlayMove.
                                    Where(m => m.MatchId == matchResponseViewModel.Id &&
                                                        m.PlayerId == userService.GetUserId()
                                                       ).OrderBy(o => o.TurnCount).ToListAsync();
                if (playerGpm == null)
                    continue;
                isFound = true;


                gpm.GetViewModel(playerGpm, opponentGpm);

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

            gamePlayMove.PlayerId = userService.GetUserId();
            gamePlayMove.MatchId = matchResponseViewModel.Id;
            gamePlayMove.TurnCount = 0;
            gamePlayMove.PlayerMove = gpVm.PlayerNextGuessNumber;
            gamePlayMove.MoveTime = DateTime.Now; ;

            _context.Add(gamePlayMove);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }



        // GET: GamePlayMoves/Create
        public IActionResult ChooseNumber()
        {
            if (TempData["newGame"] is string s)
            {
                matchResponseViewModel = JsonConvert.DeserializeObject<MatchResponseViewModel>(s);
                if (matchResponseViewModel.Id == 0)
                    matchResponseViewModel.Id = 100;
            }


            return View();
        }




        // POST: GamePlayMoves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChooseNumber([Bind("GuessNumber")] MoveModel moveModel)
        {
            GamePlayMove gamePlayMove = new GamePlayMove();

            gamePlayMove.PlayerId = userService.GetUserId();
            gamePlayMove.MatchId = matchResponseViewModel.Id;
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
    }
}
