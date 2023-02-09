using FilmYonetim.Business.FilmBusinessManagers;
using FilmYonetim.Business.SalonBusinessManagers;
using FilmYonetim.Business.SeansBusinessManagers;
using Microsoft.Extensions.DependencyInjection;

namespace FilmYonetim.Business
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddFilmYonetimManagers(this IServiceCollection service) 
        {
            service.AddTransient<IFilmBusinessOperations, FilmBusinessOperations>();
            service.AddTransient<ISalonBusinessOperations, SalonBusinessOperations>();
            service.AddTransient<ISeansBusinessOperations, SeansBusinessOperations>();

            return service; 
        }
    }
}
