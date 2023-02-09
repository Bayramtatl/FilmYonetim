using FilmYonetim.Business.BaseBusinessManagers;
using FilmYonetim.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmYonetim.Business.SeansBusinessManagers
{
    public interface ISeansBusinessOperations : IBaseManager<Seans>
    {
        public Task<List<Seans>> GetByFilm(int id);
        public Task<List<Seans>> GetBySalon(int id);
        public Task<List<Film>> GetByYillar(int baslangic, int bitis);
    }
}
