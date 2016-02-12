using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T, U>
        where T : class
        where U : struct
    {
        IEnumerable<T> GetAll();
        T GetById(U id);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
        void Delete(U id);
    }
}
