using CSharpFunctionalExtensions;
using NotificationMicroservice.Core.Interface.Repository;
using NotificationMicroservice.DataAccess.NotificationMicroserviceDbContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotificationMicroservice.DataAccess.Implementation
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext context;

        public Repository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public Maybe<T> GetById(Guid id)
        {
            T item = context.Set<T>().Find(id);
            return item ?? Maybe<T>.None;
        }

        public T Save(T obj)
        {
            context.Set<T>().Add(obj);
            context.SaveChanges();
            return obj;
        }

        public T Edit(T obj)
        {
            context.Set<T>().Update(obj);
            context.SaveChanges();
            return obj;
        }

        public void Delete(T obj)
        {
            context.Set<T>().Remove(obj);
            context.SaveChanges();
        }
    }
}