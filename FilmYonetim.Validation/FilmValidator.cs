using FilmYonetim.Domain.Entities;
using FluentValidation;
using FilmYonetim.Dal.Context;

namespace FilmYonetim.Validation
{
    public class FilmValidator : AbstractValidator<Film>
    {
        public FilmValidator(FilmYonetimDataContext dataContext)
        {
            RuleFor(m => m.FilmAd).NotEmpty().WithMessage("Tanım Boş Geçilemez");
            RuleFor(m => m.FilmYapimYil).NotNull().WithMessage("Yapım Yılı Boş Geçilemez");
        }
    }
}

