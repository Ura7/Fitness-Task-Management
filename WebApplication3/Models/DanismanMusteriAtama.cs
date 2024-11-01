using System.ComponentModel.DataAnnotations.Schema;
using WebApplication3.Areas.Identity.Data;

namespace WebApplication3.Models
{
    public class DanismanMusteriAtama
    {
        public int Id { get; set; }
        [ForeignKey("Danışman")]
        public string DanismanID { get; set; }
        public AppUser Danisman { get; set; }

        [ForeignKey("Musteri")]
        public string MusteriID {  get; set; }
        public AppUser Musteri { get; set; }




   

    }
}
