using FilmYonetim.Dal.Context;
using FilmYonetim.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmYonetim.Business.SeansBusinessManagers
{
    public class SeansBusinessOperations : ISeansBusinessOperations
    {
        private readonly FilmYonetimDataContext _filmyonetimDataContext;

        public SeansBusinessOperations(FilmYonetimDataContext filmyonetimDataContext)
        {
            _filmyonetimDataContext = filmyonetimDataContext;
        }
        public async Task<int> Add(Seans model)
        {

            await using var transaction = await _filmyonetimDataContext.Database.BeginTransactionAsync();

            try
            {
                await _filmyonetimDataContext.Seanslar.AddAsync(model);

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

        public async Task<int> Update(Seans model)
        {
            await using var transaction = await _filmyonetimDataContext.Database.BeginTransactionAsync();
            try
            {
                var currentSeans = await _filmyonetimDataContext.Seanslar.FirstOrDefaultAsync(t => t.Id == model.Id);

                if (currentSeans == null)
                {
                    return 0;
                }
                currentSeans.SeansNo = model.SeansNo;
                currentSeans.FilmId = model.FilmId;
                currentSeans.SalonId = model.SalonId;
                _filmyonetimDataContext.Update(currentSeans);

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

            var currentSeans = await _filmyonetimDataContext.Seanslar.FirstOrDefaultAsync(t => t.Id == id);

            if (currentSeans == null)
            {
                return 0;
            }

            _filmyonetimDataContext.Remove(currentSeans);

            var result = await _filmyonetimDataContext.SaveChangesAsync();

            if (result > 0)
            {
                await transaction.CommitAsync();

                return 1;
            }

            await transaction.RollbackAsync();

            return 0;
        }

        public async Task<List<Seans>> GetAll()
        {
            var SeansList = await _filmyonetimDataContext.Seanslar.Include(t=>t.Salon).Include(t=>t.Film).ToListAsync();


            return SeansList;
        }

        public async Task<Seans> GetById(int id)
        {
            await using var transaction = await _filmyonetimDataContext.Database.BeginTransactionAsync();

            var currentSeans = await _filmyonetimDataContext.Seanslar.Include(t=> t.Film).Include(t=>t.Salon).FirstOrDefaultAsync(t => t.Id == id);

            return currentSeans;
        }
        public async Task<List<Seans>> GetByFilm(int id)
        {
            await using var transaction = await _filmyonetimDataContext.Database.BeginTransactionAsync();

            var seanslar =  await _filmyonetimDataContext.Seanslar.Include(t => t.Film).Include(t => t.Salon).Where(i=> i.FilmId == id).ToListAsync();

            return seanslar;
        }
        public async Task<List<Seans>> GetBySalon(int id)
        {
            await using var transaction = await _filmyonetimDataContext.Database.BeginTransactionAsync();

            var seanslar = await _filmyonetimDataContext.Seanslar.Include(t => t.Film).Include(t => t.Salon).Where(i => i.SalonId == id).ToListAsync();

            return seanslar;
        }
        public async Task<List<Film>> GetByYillar(int baslangic, int bitis)
        {
            await using var transaction = await _filmyonetimDataContext.Database.BeginTransactionAsync();
            var filmler = await _filmyonetimDataContext.Filmler
                .Where(i => i.FilmYapimYil >= baslangic && i.FilmYapimYil<= bitis).ToListAsync();
            return filmler;
        }
    }
}