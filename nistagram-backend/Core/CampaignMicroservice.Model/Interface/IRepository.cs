using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignMicroservice.Core.Interface
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        Maybe<T> GetById(Guid id);

        T Save(T obj);

        T Edit(T obj);

        void Delete(Guid id);
    }
}