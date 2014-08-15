using System;
using System.Collections.Generic;
using System.Linq;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Data.Repositories.Abstract;
using WeeWorld.ADS.Services.Abstract;

namespace WeeWorld.ADS.Services.Concrete
{
    public abstract class BaseService<T> : IBaseService<T> where T : class, IModel
    {
        public readonly IRepository<T> repo;

        public BaseService(IRepository<T> repo)
        {
            this.repo = repo;
        }

        /// <summary>Retrieve a T object using its unique Id</summary>
        public T GetById(int id)
        {
            if (id < 1)
                return null;

            return repo.Single(id);
        }

        /// <summary>Retrieve a collection of all T Objects in the system</summary>
        public IList<T> GetAll()
        {
            var objects = repo.All();
            return objects.ToList();
        }

        /// <summary>Create a new instance of T object</summary>
        public virtual T Create(T obj)
        {
            Validate(obj);

            return repo.Create(obj);
        }

        /// <summary>Save an existing instance of T object</summary>
        public virtual T Update(T obj)
        {
            Validate(obj);

            repo.Update(obj);
            return obj;
        }

        /// <summary>Delete an instance of T object</summary>
        public virtual void Delete(T obj)
        {
            if (obj == null)
                return;

            repo.Delete(obj);
        }

        /// <summary>Delete an instance of an existing T object, using its unique id</summary>
        public void Delete(int id)
        {
            var obj = GetById(id);
            Delete(obj);
        }

        public abstract void Validate(T obj);

    }
}   