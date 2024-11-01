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
    public class BeslenmePlanıController : Controller
    {
        private readonly FitnessDB _context;
        private readonly UserManager<AppUser> _userManager;

        public BeslenmePlanıController(FitnessDB context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: BeslenmePlanı
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (await _userManager.IsInRoleAsync(user, "Danışman"))
            {
                var danismanid = _userManager.GetUserId(User);

                var danismanmusterileri = _context.DanismanMusteriAtamas.Where(c => c.DanismanID == danismanid).ToList();



                var filtered = _context.BeslenmePlan.Where(a => danismanmusterileri.Select(d => d.MusteriID).Contains(a.MusteriID)).ToList();

                return View(filtered);

            }
            else if(await  _userManager.IsInRoleAsync(user,"Admin"))
            {
                var adminid = _userManager.GetUserId(User);

                return _context.BeslenmePlan != null ?
                    View(await _context.BeslenmePlan.ToListAsync()) :
                    Problem("Entity set 'FitnessDB.MusteriProgresses'  is null.");


            }
            var userid = _userManager.GetUserId(User);
            return _context.BeslenmePlan != null ?
                          View(await _context.BeslenmePlan.Where(a => a.MusteriID == userid).ToListAsync()) :
                          Problem("Entity set 'FitnessDB.AntrenmanProgramlarıs'  is null.");
        }

        // GET: BeslenmePlanı/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BeslenmePlanı == null)
            {
                return NotFound();
            }

            var beslenmePlanı = await _context.BeslenmePlanı
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beslenmePlanı == null)
            {
                return NotFound();
            }

            return View(beslenmePlanı);
        }

        // GET: BeslenmePlanı/Create
        [Authorize(Roles = "Danışman")]
        public IActionResult Create()
        {
            var danismanid = _userManager.GetUserId(User);
            var danismanmusterileri = _context.DanismanMusteriAtamas.Where(c => c.DanismanID == danismanid).Select(c => c.MusteriID)
                .ToList();
            ViewBag.userid = danismanid;
            ViewBag.Musteriler = new SelectList(danismanmusterileri, "MusteriID");
            return View();
        }

        // POST: BeslenmePlanı/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Danışman")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,MusteriID,Hedef,GunlukBesin,KaloriHedefi")] BeslenmePlanı beslenmePlanı)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                beslenmePlanı.UserId = userId;
                _context.Add(beslenmePlanı);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(beslenmePlanı);
        }

        // GET: BeslenmePlanı/Edit/5
        [Authorize(Roles = "Danışman,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BeslenmePlanı == null)
            {
                return NotFound();
            }

            var beslenmePlanı = await _context.BeslenmePlanı.FindAsync(id);
            if (beslenmePlanı == null)
            {
                return NotFound();
            }
            return View(beslenmePlanı);
        }

        // POST: BeslenmePlanı/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Danışman,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,MusteriID,Hedef,GunlukBesin,KaloriHedefi")] BeslenmePlanı beslenmePlanı)
        {
            if (id != beslenmePlanı.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beslenmePlanı);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeslenmePlanıExists(beslenmePlanı.Id))
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
            return View(beslenmePlanı);
        }

        // GET: BeslenmePlanı/Delete/5
        [Authorize(Roles = "Danışman,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BeslenmePlanı == null)
            {
                return NotFound();
            }

            var beslenmePlanı = await _context.BeslenmePlanı
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beslenmePlanı == null)
            {
                return NotFound();
            }

            return View(beslenmePlanı);
        }

        // POST: BeslenmePlanı/Delete/5
        [Authorize(Roles = "Danışman,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BeslenmePlanı == null)
            {
                return Problem("Entity set 'FitnessDB.BeslenmePlanı'  is null.");
            }
            var beslenmePlanı = await _context.BeslenmePlanı.FindAsync(id);
            if (beslenmePlanı != null)
            {
                _context.BeslenmePlanı.Remove(beslenmePlanı);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeslenmePlanıExists(int id)
        {
          return (_context.BeslenmePlanı?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
