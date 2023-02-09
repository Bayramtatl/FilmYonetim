using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FilmYonetim.Domain.Entities
{
    public class Salon : BaseObject
    {
        [Required(ErrorMessage ="Boş olamaz")]
        [DisplayName("Salon Adi")]
        [StringLength(100, ErrorMessage = "Salon Adi 100 karakterden fazla olamaz")]
        public string SalonAd { get; set; }

        public ICollection<Seans> Seans { get; set; }
    }
}
