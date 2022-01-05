using GuessNumber.Data;
using GuessNumber.Service;
using GuessNumber.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuessNumber.Controllers
{
    public class GameResultsController : Controller
    {

        private readonly AuthDbContext _context;
        private readonly IUserService userService;

        public GameResultsController(AuthDbContext context, IUserService userService)
        {
            _context = context;
            this.userService = userService;

        }
        public async Task<IActionResult> Index()
        {
           await GetTopScores();
            return View();
        }

        private async Task<List<LeaderBoardViewModel>> GetTopScores()
        {

            var topUsers = await _context.GameResult
                .Include(u => u.Player)
                .GroupBy(g => g.GuessNumberUserId)
                .Select(a => new {TotalScore = a.Sum(b=> b.GamePoint),Name =a.Select(s=>s.Player.FirstName)})
                .OrderByDescending(a=>a.TotalScore)
                .AsNoTracking().ToListAsync();

            



            return null;
        }

    }
}
