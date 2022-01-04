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
using Microsoft.AspNetCore.Authorization;
using GuessNumber.Service;

namespace GuessNumber.Controllers
{
    [Authorize]
    public class MatchRequestsController : Controller
    {
        private readonly AuthDbContext _context;
        private readonly IUserService userService;

        public MatchRequestsController(AuthDbContext context, IUserService userService)
        {
            _context = context;
            this.userService = userService;
        }

        // GET: MatchRequests
        public IActionResult Index()
        {
            return View();
        }

        // GET: MatchRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchRequest = await _context.MatchRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matchRequest == null)
            {
                return NotFound();
            }

            return View(matchRequest);
        }

        // GET: MatchRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MatchRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMatch()
        {

            List<MatchRequest> matchRequests = new List<MatchRequest>();
            //MatchRequest matchRequest = new MatchRequest();

            //for (int i = 0; i < 1000; i++)
            //{
            //    await Task.Delay(500);
            //    matchRequest = new MatchRequest()
            //    {
            //        PlayerId = i.ToString(),
            //        RequestTime = DateTime.Now.AddSeconds(i)
            //    };

            //    _context.Add(matchRequest);

            //    await _context.SaveChangesAsync();

            //}

            MatchRequest matchRequest = new MatchRequest()
            {
                PlayerId = userService.GetUserId(),
                RequestTime = DateTime.Now
            };

            if (ModelState.IsValid)
            {
                await ClearBeforeMatchRequest();
                
                _context.Add(matchRequest);

                await _context.SaveChangesAsync();
               
            }
            return RedirectToAction("Index","MatchResponses");
        }
      
        private async Task ClearBeforeMatchRequest()
        {
            var beforeRequest = _context.MatchRequest.Where(r => r.PlayerId == userService.GetUserId());

            if (beforeRequest != null)
            {
                _context.MatchRequest.RemoveRange(beforeRequest);
                await _context.SaveChangesAsync();
            }

        }

      

        // GET: MatchRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchRequest = await _context.MatchRequest.FindAsync(id);
            if (matchRequest == null)
            {
                return NotFound();
            }
            return View(matchRequest);
        }

        // POST: MatchRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlayerId,RequestTime")] MatchRequest matchRequest)
        {
            if (id != matchRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matchRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchRequestExists(matchRequest.Id))
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
            return View(matchRequest);
        }

        // GET: MatchRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchRequest = await _context.MatchRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matchRequest == null)
            {
                return NotFound();
            }

            return View(matchRequest);
        }

        // POST: MatchRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matchRequest = await _context.MatchRequest.FindAsync(id);
            _context.MatchRequest.Remove(matchRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchRequestExists(int id)
        {
            return _context.MatchRequest.Any(e => e.Id == id);
        }
    }
}
