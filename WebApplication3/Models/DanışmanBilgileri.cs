using System.ComponentModel.DataAnnotations.Schema;
using WebApplication3.Areas.Identity.Data;

namespace WebApplication3.Models
{
    public class DanışmanBilgileri
    {
        public int Id { get; set; }

        [ForeignKey("Danışman")]
        public string UserId { get; set; }
        public AppUser User { get; set; }


        public string Yas { get; set; }
        public string UzmanlıkAlanı { get; set; }

        public string Deneyim { get; set; }

        public string Egitim { get; set; }
    }
}
