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

namespace GuessNumber.Controllers
{
    public class GamePlayMovesController : Controller
    {
        private readonly AuthDbContext _context;

        public GamePlayMovesController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: GamePlayMoves
        public async Task<IActionResult> Index()
        {
            return View(await _context.GamePlayMove.ToListAsync());
        }

        // GET: GamePlayMoves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePlayMove = await _context.GamePlayMove
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamePlayMove == null)
            {
                return NotFound();
            }

            return View(gamePlayMove);
        }

        // GET: GamePlayMoves/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GamePlayMoves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MatchId,PlayerId,PlayerMove,TurnCount,MoveTime")] GamePlayMove gamePlayMove)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gamePlayMove);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gamePlayMove);
        }

        // GET: GamePlayMoves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePlayMove = await _context.GamePlayMove.FindAsync(id);
            if (gamePlayMove == null)
            {
                return NotFound();
            }
            return View(gamePlayMove);
        }

        // POST: GamePlayMoves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MatchId,PlayerId,PlayerMove,TurnCount,MoveTime")] GamePlayMove gamePlayMove)
        {
            if (id != gamePlayMove.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gamePlayMove);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamePlayMoveExists(gamePlayMove.Id))
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
            return View(gamePlayMove);
        }

        // GET: GamePlayMoves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePlayMove = await _context.GamePlayMove
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamePlayMove == null)
            {
                return NotFound();
            }

            return View(gamePlayMove);
        }

        // POST: GamePlayMoves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gamePlayMove = await _context.GamePlayMove.FindAsync(id);
            _context.GamePlayMove.Remove(gamePlayMove);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GamePlayMoveExists(int id)
        {
            return _context.GamePlayMove.Any(e => e.Id == id);
        }
    }
}
