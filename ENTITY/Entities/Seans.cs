using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmYonetim.Domain.Entities
{
    public class Seans : BaseObject
    {

        [Required(ErrorMessage ="Seans Adı Girilmelidir")]
        [DisplayName("Seans Adı")]
        public string SeansNo { get; set; }

        [Required(ErrorMessage = "Film Seçilmelidir")]
        public int FilmId { get; set; }
        public Film Film { get; set; }

        [Required(ErrorMessage = "Salon Seçilmelidir")]
        public int SalonId { get; set; }
        public Salon Salon { get; set; }

        [Required(ErrorMessage = "Gösterim Yılı Girilmelidir")]
        public int GosterimYili { get; set; }

        [NotMapped]
        public int BaslangicTarih { get; set; }
        [NotMapped]
        public int BitisTarih { get; set; }

    }
}
