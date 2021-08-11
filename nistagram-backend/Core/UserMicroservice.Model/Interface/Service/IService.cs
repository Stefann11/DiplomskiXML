using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMicroservice.Core.Interface.Service
{
    public interface IService<T>
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        T Save(T obj);

        T Edit(T obj);

        T Delete(T obj);
    }
}