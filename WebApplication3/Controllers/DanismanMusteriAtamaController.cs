using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebApplication3.Areas.Identity.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Authorize(Roles ="Admin")]
    public class DanismanMusteriAtamaController : Controller
    {
        private readonly FitnessDB _coontext;

        public DanismanMusteriAtamaController(FitnessDB context)
        {
        _coontext = context;
        }


        public IActionResult Index()
        {
            var danisman = _coontext.Users.Where(u => u.Rol == "Danışman");
            var musteri = _coontext.Users.Where(u => u.Rol == "Müşteri");

            
            ViewBag.danisman = danisman;
            ViewBag.musteri = musteri;


            return View();
        }



        [HttpPost]
        public IActionResult Atama(string danismanID,  string musteriID, string musteriAdi)
        {
            var atama = new DanismanMusteriAtama
            {
                DanismanID = danismanID,
                MusteriID = musteriID,
                
            };

            _coontext.DanismanMusteriAtamas.Add( atama );
            _coontext.SaveChanges();

            return RedirectToAction("Index");


        }

    }
}
