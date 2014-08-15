
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Data.Repositories.Abstract;
using System.Linq;
using System.Data;

namespace WeeWorld.ADS.Data.Repositories.Concrete
{
    /// <summary>Generic class providing all CRUD & Querying actions for implimentations of IModels</summary>
    public class EFRepository<T> : IRepository<T> where T : class, IModel
    {
        private DatabaseContext databaseContext { get; set; }
        private DbSet<T> dataset { get; set; }

        public EFRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
            this.dataset = databaseContext.Set<T>();
        }

        public T Single(int id)
        {
            return Single(o => o.Id == id);
        }

        public T Single(Func<T, bool> where)
        {
            try
            {
                return dataset.Single(where);
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message == "Sequence contains no elements")
                    throw new ObjectNotFoundException(typeof(T).Name.ToLower() + " not found");
                if (ex.Message == "Sequence contains more than one element")
                    throw new ArgumentException("more than one object was found using this criteria");

                throw;
            }
        }

        public T SingleOrDefault(Func<T, bool> where)
        {
            try
            {
                return dataset.SingleOrDefault(where);
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message == "Sequence contains no elements")
                    throw new ObjectNotFoundException(typeof(T).Name.ToLower() + " not found");
                if (ex.Message == "Sequence contains more than one element")
                    throw new ArgumentException("more than one object was found using this criteria");

                throw;
            }
        }

        public IEnumerable<T> All()
        {
            return dataset.ToList();
        }

        public IEnumerable<T> List(Func<T, bool> where)
        {
            return dataset.Where(where).ToList();
        }

        public T Create(T obj)
        {
            dataset.Add(obj);
            SaveChanges();
            return obj;
        }

        public T Update(T obj)
        {
            // grab the existing entity from the databaseContext, and apply the new object values to that
            var entity = dataset.Find(obj.Id);
            var existingEntity = databaseContext.Entry(entity);

            existingEntity.CurrentValues.SetValues(obj);

            SaveChanges();
            return obj;
        }

        public virtual void Delete(int id)
        {
            var obj = Single(id);
            Delete(obj);
        }

        public virtual void Delete(T obj)
        {
            dataset.Remove(obj);
            SaveChanges();
        }

        public bool Exists(Func<T, bool> where)
        {
            T obj;
            return Exists(where, out obj);
        }

        public bool Exists(Func<T, bool> where, out T obj)
        {
            obj = Single(where);
            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (databaseContext != null)
                {
                    databaseContext.Dispose();
                    databaseContext = null;
                }
            }
        }

        private void SaveChanges()
        {
            databaseContext.SaveChanges();
        }
    }
}