using FilmYonetim.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmYonetim.Dal.Context
{
    public class FilmYonetimDataContext : DbContext
    {
        public FilmYonetimDataContext(DbContextOptions<FilmYonetimDataContext> options) : base(options) {}
        public DbSet<Film> Filmler { get; set; }
        public DbSet<Salon> Salonlar { get; set; }
        public DbSet<Seans> Seanslar { get; set; }
    }
}
