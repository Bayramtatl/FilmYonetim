using FilmYonetim.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
namespace FilmYonetim.Validation
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddFilmYonetimValidation(this IServiceCollection service)
        {
            service.AddTransient<IValidator<Film>, FilmValidator>();

            return service;
        }
    }
}
