using System;
using System.Collections.Generic;
using WeeWorld.ADS.Data.Models;

namespace WeeWorld.ADS.Data.Repositories.Abstract
{
    /// <summary>Generic interface providing all CRUD & Querying actions for implimentations of IModels</summary>
    public interface IRepository<T> where T : IModel
    {
        /// <summary>Get a single T, identified by unique Id</summary>
        T Single(int id);

        /// <summary>Get a single T objects that meets the criteria. Throws exception if none exists</summary>
        T Single(Func<T, bool> where);

        /// <summary>Get a single T objects that meets the criteria. Returns null if none exists</summary>
        T SingleOrDefault(Func<T, bool> where);

        /// <summary>Get a list of all T objects</summary>
        IEnumerable<T> All();

        /// <summary>Get a list of all T objects that meet the where criteria</summary>
        IEnumerable<T> List(Func<T, bool> where);

        /// <summary>Add a new T object to the database</summary>
        T Create(T obj);

        /// <summary>Update an existing T object in the database</summary>
        T Update(T obj);
        
        /// <summary>Remove a T object from the database, identified by its unique id</summary>
        void Delete(int id);

        /// <summary>Remove a T object from the database</summary>
        void Delete(T obj);

        /// <summary>Check if an object exists that meets the supplied criteria</summary>
        bool Exists(Func<T, bool> where);

        /// <summary>Check if an object exists that meets the supplied criteria, 
        /// returns the object if it does exist as an out param</summary>
        bool Exists(Func<T, bool> where, out T obj);

    }
}