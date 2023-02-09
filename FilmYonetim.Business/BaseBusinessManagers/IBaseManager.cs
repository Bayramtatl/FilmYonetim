using FilmYonetim.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmYonetim.Business.BaseBusinessManagers
{
    public interface IBaseManager<T>
    {
        public Task<int> Add(T model);
        public Task<int> Update(T model);
        public Task<int> Delete(int id);
        public Task<List<T>> GetAll();
        public Task<T> GetById(int id);
    }
}