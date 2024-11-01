namespace WebApplication3.Models
{
    public class Mesajlar
    {
        public int Id { get; set; }

        public string AlıcıID { get; set; }
        public string AlıcıName { get; set; }

        public string GönderenID { get; set; }
        public string GönderenName { get; set; }

        public string Mesajİcerigi {  get; set; }

    }
}
