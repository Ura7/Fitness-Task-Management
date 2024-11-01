using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Areas.Identity.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class MesajlarsController : Controller
    {
        private readonly FitnessDB _context;
        private readonly UserManager<AppUser> _userManager; 

        public MesajlarsController(FitnessDB context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Mesajlars
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (await _userManager.IsInRoleAsync(user,"Müşteri"))
            {
                var userid = _userManager.GetUserId(User);
                return _context.Mesajlar != null ?
                              View(await _context.Mesajlar.Where(a => a.GönderenID == userid || a.AlıcıID == userid).ToListAsync()) :
                              Problem("Entity set 'FitnessDB.AntrenmanProgramlarıs'  is null.");
            }

            else if(await _userManager.IsInRoleAsync(user,"Danışman"))
            {
                var userid = _userManager.GetUserId(User);
                return _context.Mesajlar != null ?
                              View(await _context.Mesajlar.Where(a => a.GönderenID == userid || a.AlıcıID == userid).ToListAsync()) :
                              Problem("Entity set 'FitnessDB.AntrenmanProgramlarıs'  is null.");
            }

              return _context.Mesajlar != null ? 
                          View(await _context.Mesajlar.ToListAsync()) :
                          Problem("Entity set 'FitnessDB.Mesajlar'  is null.");
        }

        // GET: Mesajlars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mesajlar == null)
            {
                return NotFound();
            }

            var mesajlar = await _context.Mesajlar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mesajlar == null)
            {
                return NotFound();
            }

            return View(mesajlar);
        }

        // GET: Mesajlars/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            if (await _userManager.IsInRoleAsync(user, "Danışman"))
            {
                var danismanid = _userManager.GetUserId(User);
                var danismanmusterileri = _context.DanismanMusteriAtamas.Where(c => c.DanismanID == danismanid).Select(c => c.MusteriID)
                    .ToList();
                
                var danismanmusteri = _context.Users.Where(u=>danismanmusterileri.Contains(u.Id)).Select(u=> new {u.Id, u.Firstname}).ToList();
                var atama = _context.DanismanMusteriAtamas.FirstOrDefault(a => a.DanismanID == danismanid);
                var name = atama.Danisman.Firstname;
                
                //var name = _context.Users.FirstOrDefault(a => a.Id == danismanid);
                
                ViewBag.Seçenekler = new SelectList(danismanmusterileri,"");
                ViewBag.Name = name;
                ViewBag.id = danismanid;
                
            }
            else if(await _userManager.IsInRoleAsync(user,"Müşteri"))
            {
                var musteriid = _userManager.GetUserId(User);
                var danismanmusterileri = _context.DanismanMusteriAtamas.Where(c => c.MusteriID == musteriid).Select(c => c.DanismanID)
                    .ToList();
                var atama = _context.DanismanMusteriAtamas.FirstOrDefault(a=>a.MusteriID==musteriid);
                var name = atama.Musteri.Firstname;
                ViewBag.Seçenekler = new SelectList(danismanmusterileri, "DanismanID");
                ViewBag.Name = name;
                ViewBag.id = musteriid;
            }

            return View();
        }

        // POST: Mesajlars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AlıcıID,AlıcıName,GönderenID,GönderenName,Mesajİcerigi")] Mesajlar mesajlar)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                mesajlar.GönderenID = userId;
                _context.Add(mesajlar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mesajlar);
        }

        // GET: Mesajlars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mesajlar == null)
            {
                return NotFound();
            }

            var mesajlar = await _context.Mesajlar.FindAsync(id);
            if (mesajlar == null)
            {
                return NotFound();
            }
            return View(mesajlar);
        }

        // POST: Mesajlars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AlıcıID,AlıcıName,GönderenID,GönderenName,Mesajİcerigi")] Mesajlar mesajlar)
        {
            if (id != mesajlar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mesajlar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MesajlarExists(mesajlar.Id))
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
            return View(mesajlar);
        }

        // GET: Mesajlars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mesajlar == null)
            {
                return NotFound();
            }

            var mesajlar = await _context.Mesajlar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mesajlar == null)
            {
                return NotFound();
            }

            return View(mesajlar);
        }

        // POST: Mesajlars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mesajlar == null)
            {
                return Problem("Entity set 'FitnessDB.Mesajlar'  is null.");
            }
            var mesajlar = await _context.Mesajlar.FindAsync(id);
            if (mesajlar != null)
            {
                _context.Mesajlar.Remove(mesajlar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MesajlarExists(int id)
        {
          return (_context.Mesajlar?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
