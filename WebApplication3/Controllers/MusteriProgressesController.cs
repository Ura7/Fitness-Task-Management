using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Areas.Identity.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Authorize(Roles ="Admin,Müşteri,Danışman")]
    public class MusteriProgressesController : Controller
    {
        private readonly FitnessDB _context;
        private readonly UserManager<AppUser> _userManager;

        public MusteriProgressesController(FitnessDB context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: MusteriProgresses

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if(await _userManager.IsInRoleAsync(user,"Admin"))
            {
                var adminid = _userManager.GetUserId(User);
                
                return _context.MusteriProgresses != null ?
                    View(await _context.MusteriProgresses.ToListAsync()):
                    Problem("Entity set 'FitnessDB.MusteriProgresses'  is null.");


            }

            else if(await _userManager.IsInRoleAsync(user,"Danışman"))
            {
                var danismanid = _userManager.GetUserId(User);

                var danismanmusterileri = _context.DanismanMusteriAtamas.Where(c => c.DanismanID == danismanid).ToList();


                
                var filteredview = _context.MusteriProgresses.Where(a => danismanmusterileri.Select(d => d.MusteriID).Contains(a.UserId)).ToList();

                return View(filteredview);
            }



            var userid = _userManager.GetUserId(User);
                

            return _context.MusteriProgresses != null ? 
                          View(await _context.MusteriProgresses.Where(a=>a.UserId==userid).ToListAsync()) :
                          Problem("Entity set 'FitnessDB.MusteriProgresses'  is null.");
        }

        // GET: MusteriProgresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MusteriProgresses == null)
            {
                return NotFound();
            }

            var musteriProgress = await _context.MusteriProgresses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musteriProgress == null)
            {
                return NotFound();
            }

            return View(musteriProgress);
        }

        // GET: MusteriProgresses/Create
        [Authorize(Roles ="Müşteri")]
        public IActionResult Create()
        {
            var userid = _userManager.GetUserId(User);
            ViewBag.userid = userid;
            return View();
        }

        // POST: MusteriProgresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Müşteri")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Kilo,Boy,YagOrani,KasKutlesi,VucutKitleEndeksi,KayitTarihi")] MusteriProgress musteriProgress)
        {
            if (ModelState.IsValid)
            {
                var userid = _userManager.GetUserId(User);
                musteriProgress.UserId = userid;
                _context.Add(musteriProgress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(musteriProgress);
        }

        // GET: MusteriProgresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MusteriProgresses == null)
            {
                return NotFound();
            }

            var musteriProgress = await _context.MusteriProgresses.FindAsync(id);
            if (musteriProgress == null)
            {
                return NotFound();
            }
            return View(musteriProgress);
        }

        // POST: MusteriProgresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Kilo,Boy,YagOrani,KasKutlesi,VucutKitleEndeksi,KayitTarihi")] MusteriProgress musteriProgress)
        {
            if (id != musteriProgress.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musteriProgress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusteriProgressExists(musteriProgress.Id))
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
            return View(musteriProgress);
        }

        // GET: MusteriProgresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MusteriProgresses == null)
            {
                return NotFound();
            }

            var musteriProgress = await _context.MusteriProgresses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musteriProgress == null)
            {
                return NotFound();
            }

            return View(musteriProgress);
        }

        // POST: MusteriProgresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MusteriProgresses == null)
            {
                return Problem("Entity set 'FitnessDB.MusteriProgresses'  is null.");
            }
            var musteriProgress = await _context.MusteriProgresses.FindAsync(id);
            if (musteriProgress != null)
            {
                _context.MusteriProgresses.Remove(musteriProgress);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusteriProgressExists(int id)
        {
          return (_context.MusteriProgresses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
