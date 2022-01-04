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
using GuessNumber.Service;
using GuessNumber.ViewModels;

namespace GuessNumber.Controllers
{
    public class MatchResponsesController : Controller
    {
        private readonly AuthDbContext _context;
        private readonly IUserService userService;

        public MatchResponsesController(AuthDbContext context, IUserService userService)
        {
            _context = context;
            this.userService = userService;
        }
        public IActionResult FindOpponent()
        {
            return View();
        }

        // GET: MatchResponses
        public  async Task<IActionResult> Index()
        {
            await  WaitUntilOpponentFound();
            return RedirectToAction(nameof(GamePlayMovesController.ChooseNumber),"GamePlayMoves");
        }
        private async Task WaitUntilOpponentFound()
        {
            string playerId = userService.GetUserId();
            bool isFound = false;
            MatchResponse response = null;

            var contextOptions = new DbContextOptionsBuilder<AuthDbContext>()
                 .UseSqlServer(@"Server=DESKTOP-JKA3N2L;Database=GuessNumber")
                 .Options;

            using var context = new AuthDbContext(contextOptions);




            while (!isFound)
            {
                await Task.Delay(1000);
                response = await context.MatchResponse.FirstOrDefaultAsync(f => f.Player1 == playerId || f.Player2 == playerId);
                if (response == null)
                    continue;
                isFound = true;
            }

            MatchResponseViewModel vm = new MatchResponseViewModel()
            {
                Id = response.Id,
                OpponentId = response.Player1 != playerId ? response.Player1 : response.Player2,
                PlayerQuee = response.Player1 == playerId ? "Player1" : "Player2",
                RequestTime = response.RequestTime,
                ResponseTime = DateTime.Now
            };

            var s = Newtonsoft.Json.JsonConvert.SerializeObject(vm);
            TempData["newGame"] = s;
          
        }


        // GET: MatchResponses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchResponse = await _context.MatchResponse
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matchResponse == null)
            {
                return NotFound();
            }

            return View(matchResponse);
        }

        // GET: MatchResponses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MatchResponses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Player1Id,Player2Id,RequestTime")] MatchResponse matchResponse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matchResponse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(matchResponse);
        }

        // GET: MatchResponses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchResponse = await _context.MatchResponse.FindAsync(id);
            if (matchResponse == null)
            {
                return NotFound();
            }
            return View(matchResponse);
        }

        // POST: MatchResponses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Player1Id,Player2Id,RequestTime")] MatchResponse matchResponse)
        {
            if (id != matchResponse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matchResponse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchResponseExists(matchResponse.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(matchResponse);
        }

        // GET: MatchResponses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchResponse = await _context.MatchResponse
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matchResponse == null)
            {
                return NotFound();
            }

            return View(matchResponse);
        }

        // POST: MatchResponses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matchResponse = await _context.MatchResponse.FindAsync(id);
            _context.MatchResponse.Remove(matchResponse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchResponseExists(int id)
        {
            return _context.MatchResponse.Any(e => e.Id == id);
        }
    }
}
