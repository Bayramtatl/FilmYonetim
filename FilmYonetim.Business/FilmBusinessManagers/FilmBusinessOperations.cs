using FilmYonetim.Dal.Context;
using FilmYonetim.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmYonetim.Business.FilmBusinessManagers
{
    public class FilmBusinessOperations : IFilmBusinessOperations
    {
        private readonly FilmYonetimDataContext _filmyonetimDataContext;

        public FilmBusinessOperations(FilmYonetimDataContext filmyonetimDataContext)
        {
            _filmyonetimDataContext = filmyonetimDataContext;
        }
        public async Task<int> Add(Film model)
        {

            await using var transaction = await _filmyonetimDataContext.Database.BeginTransactionAsync();

            try
            {
                await _filmyonetimDataContext.Filmler.AddAsync(model);

                var result = await _filmyonetimDataContext.SaveChangesAsync();

                if (result > 0)
                {
                    await transaction.CommitAsync();

                    return 1;
                }

                await transaction.RollbackAsync();

                return 0;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();

                return 0;
            }

        }

        public async Task<int> Update(Film model)
        {
            await using var transaction = await _filmyonetimDataContext.Database.BeginTransactionAsync();
            try
            {
                var currentFilm = await _filmyonetimDataContext.Filmler.FirstOrDefaultAsync(t => t.Id == model.Id);

                if (currentFilm == null)
                {
                    return 0;
                }
                currentFilm.FilmAd = model.FilmAd;
                currentFilm.FilmYapimYil = model.FilmYapimYil;
                _filmyonetimDataContext.Update(currentFilm);

                var result = await _filmyonetimDataContext.SaveChangesAsync();

                if (result > 0)
                {
                    await transaction.CommitAsync();

                    return 1;
                }

                await transaction.RollbackAsync();

                return 0;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();

                return 0;
            }
        }

        public async Task<int> Delete(int id)
        {
            await using var transaction = await _filmyonetimDataContext.Database.BeginTransactionAsync();

            var currentFilm = await _filmyonetimDataContext.Filmler.FirstOrDefaultAsync(t => t.Id == id);

            if (currentFilm == null)
            {
                return 0;
            }

            _filmyonetimDataContext.Remove(currentFilm);

            var result = await _filmyonetimDataContext.SaveChangesAsync();

            if (result > 0)
            {
                await transaction.CommitAsync();

                return 1;
            }

            await transaction.RollbackAsync();

            return 0;
        }

        public async Task<List<Film>> GetAll()
        {
            var FilmList = await _filmyonetimDataContext.Filmler.ToListAsync();


            return FilmList;
        }

        public async Task<Film> GetById(int id)
        {
            await using var transaction = await _filmyonetimDataContext.Database.BeginTransactionAsync();

            var currentFilm = await _filmyonetimDataContext.Filmler.FirstOrDefaultAsync(t => t.Id == id);

            return currentFilm;
        }
    }
}