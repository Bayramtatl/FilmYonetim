using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FilmYonetim.Domain.Entities
{
    public class Film : BaseObject
    {
        [Required(ErrorMessage ="Boş olamaz")]
        [DisplayName("Film Adi")]
        [StringLength(100,ErrorMessage = "Film Adi 100 karakterden fazla olamaz")]  
        public string FilmAd { get; set; }

        [Required]
        [DisplayName("Film Yapım Yılı")]
        [Range(0.0,2023.0,ErrorMessage = "Film yapim yılı 1800-2023 arasında olmalıdır")]
        public int FilmYapimYil { get; set; }

        public ICollection<Seans> Seans { get; set; }
    }
}
