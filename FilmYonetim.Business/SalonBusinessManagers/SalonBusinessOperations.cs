using FilmYonetim.Business.SalonBusinessManagers;
using FilmYonetim.Dal.Context;
using FilmYonetim.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmYonetim.Business.FilmBusinessManagers
{
    public class SalonBusinessOperations : ISalonBusinessOperations
    {
        private readonly FilmYonetimDataContext _filmyonetimDataContext;

        public SalonBusinessOperations(FilmYonetimDataContext filmyonetimDataContext)
        {
            _filmyonetimDataContext = filmyonetimDataContext;
        }
        public async Task<int> Add(Salon model)
        {

            await using var transaction = await _filmyonetimDataContext.Database.BeginTransactionAsync();

            try
            {
                await _filmyonetimDataContext.Salonlar.AddAsync(model);

                var result = await _filmyonetimDataContext.SaveChangesAsync();
                if (model.SalonAd == null)
                {
                    await transaction.RollbackAsync();
                    return 0;
                }
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

        public async Task<int> Update(Salon model)
        {
            await using var transaction = await _filmyonetimDataContext.Database.BeginTransactionAsync();
            try
            {
                var currentSalon = await _filmyonetimDataContext.Salonlar.FirstOrDefaultAsync(t => t.Id == model.Id);

                if (currentSalon == null || model.SalonAd == null)
                {
                    return 0;
                }
                currentSalon.SalonAd = model.SalonAd;
                _filmyonetimDataContext.Update(currentSalon);

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

            var currentSalon = await _filmyonetimDataContext.Salonlar.FirstOrDefaultAsync(t => t.Id == id);

            if (currentSalon == null)
            {
                return 0;
            }

            _filmyonetimDataContext.Remove(currentSalon);

            var result = await _filmyonetimDataContext.SaveChangesAsync();

            if (result > 0)
            {
                await transaction.CommitAsync();

                return 1;
            }

            await transaction.RollbackAsync();

            return 0;
        }

        public async Task<List<Salon>> GetAll()
        {
            var SalonList = await _filmyonetimDataContext.Salonlar.ToListAsync();


            return SalonList;
        }

        public async Task<Salon> GetById(int id)
        {
            await using var transaction = await _filmyonetimDataContext.Database.BeginTransactionAsync();

            var currentSalon = await _filmyonetimDataContext.Salonlar.FirstOrDefaultAsync(t => t.Id == id);

            return currentSalon;
        }
    }
}