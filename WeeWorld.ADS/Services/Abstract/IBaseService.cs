using System;
using System.Collections.Generic;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Models.Validation;

namespace WeeWorld.ADS.Services.Abstract
{
    /// <summary>Base service providing common (CRUD) functionality for an IModel</summary>
    public interface IBaseService<T> where T : IModel
    {

        /// <summary>Get a single T, identified by unique Id</summary>
        T GetById(int id);

        /// <summary>Get a list of all T objects</summary>
        IList<T> GetAll();

        /// <summary>Create a new instance of T object
        /// </summary>
        T Create(T obj);

        /// <summary>Update T object</summary>
        T Update(T obj);

        /// <summary>Delete T object</summary>
        void Delete(T obj);

        /// <summary>Delete T object, identified by its using its unique id</summary>
        void Delete(int id);

    }
}
