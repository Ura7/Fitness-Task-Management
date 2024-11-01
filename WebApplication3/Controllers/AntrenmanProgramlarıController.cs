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
    public class AntrenmanProgramlarıController : Controller
    {
        private readonly FitnessDB _context;
        private readonly UserManager<AppUser> _userManager;

        public AntrenmanProgramlarıController(FitnessDB context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AntrenmanProgramları
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (await _userManager.IsInRoleAsync(user, "Danışman"))
            {
                var danismanid = _userManager.GetUserId(User);

                var danismanmusterileri = _context.DanismanMusteriAtamas.Where(c => c.DanismanID == danismanid).ToList();

               

                var filtered = _context.AntrenmanProgramlarıs.Where(a => danismanmusterileri.Select(d => d.MusteriID ).Contains(a.MusteriID)).ToList();
                
                return View(filtered);
                         
            }
            
            else if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    var adminid = _userManager.GetUserId(User);

                    return _context.AntrenmanProgramlarıs != null ?
                        View(await _context.AntrenmanProgramlarıs.ToListAsync()) :
                        Problem("Entity set 'FitnessDB.MusteriProgresses'  is null.");


                }


            

            var userid = _userManager.GetUserId(User);
            return _context.AntrenmanProgramlarıs != null ?
                          View(await _context.AntrenmanProgramlarıs.Where(a => a.MusteriID == userid).ToListAsync()) :
                          Problem("Entity set 'FitnessDB.AntrenmanProgramlarıs'  is null.");




        }

        // GET: AntrenmanProgramları/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AntrenmanProgramlarıs == null)
            {
                return NotFound();
            }

            var antrenmanProgramları = await _context.AntrenmanProgramlarıs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (antrenmanProgramları == null)
            {
                return NotFound();
            }

            return View(antrenmanProgramları);
        }
        [Authorize(Roles ="Danışman")]
        // GET: AntrenmanProgramları/Create
        public IActionResult Create()
        {
            var danismanid = _userManager.GetUserId(User);
            var danismanmusterileri = _context.DanismanMusteriAtamas.Where(c => c.DanismanID == danismanid).Select(c => c.MusteriID)
                .ToList();
            ViewBag.userid = danismanid;
            ViewBag.Musteriler = new SelectList(danismanmusterileri, "MusteriID");

           

            return View();
        }

        // POST: AntrenmanProgramları/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Danışman")]
        public async Task<IActionResult> Create([Bind("Id,UserId,MusteriID,EgzersizAdı,Hedef,Setsayısı,VideoURL,Baslangic,ProgramSüresi")] AntrenmanProgramları antrenmanProgramları)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                antrenmanProgramları.UserId = userId;

                //var atama = _context.DanismanMusteriAtamas.FirstOrDefault(a => a.DanismanID == danismanid);









                _context.Add(antrenmanProgramları);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(antrenmanProgramları);
        }

        // GET: AntrenmanProgramları/Edit/5
        [Authorize(Roles = "Danışman,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AntrenmanProgramlarıs == null)
            {
                return NotFound();
            }

            var antrenmanProgramları = await _context.AntrenmanProgramlarıs.FindAsync(id);
            if (antrenmanProgramları == null)
            {
                return NotFound();
            }
            return View(antrenmanProgramları);
        }

        // POST: AntrenmanProgramları/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Danışman,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,MusteriID,EgzersizAdı,Hedef,Setsayısı,VideoURL,Baslangic,ProgramSüresi")] AntrenmanProgramları antrenmanProgramları)
        {
            if (id != antrenmanProgramları.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(antrenmanProgramları);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AntrenmanProgramlarıExists(antrenmanProgramları.Id))
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
            return View(antrenmanProgramları);
        }

        // GET: AntrenmanProgramları/Delete/5
        [Authorize(Roles = "Danışman,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AntrenmanProgramlarıs == null)
            {
                return NotFound();
            }

            var antrenmanProgramları = await _context.AntrenmanProgramlarıs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (antrenmanProgramları == null)
            {
                return NotFound();
            }

            return View(antrenmanProgramları);
        }

        // POST: AntrenmanProgramları/Delete/5
        [Authorize(Roles = "Danışman,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AntrenmanProgramlarıs == null)
            {
                return Problem("Entity set 'FitnessDB.AntrenmanProgramlarıs'  is null.");
            }
            var antrenmanProgramları = await _context.AntrenmanProgramlarıs.FindAsync(id);
            if (antrenmanProgramları != null)
            {
                _context.AntrenmanProgramlarıs.Remove(antrenmanProgramları);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AntrenmanProgramlarıExists(int id)
        {
          return (_context.AntrenmanProgramlarıs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
