namespace WebApplication3.Models
{
    public class AntrenmanProgramları
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string MusteriID { get; set; }
        public string EgzersizAdı {  get; set; }

        public string Hedef { get; set; }

        public int Setsayısı { get; set; }

        public string VideoURL { get; set; }

        public DateTime Baslangic {  get; set; }

        public string ProgramSüresi {  get; set; }

    }
}
