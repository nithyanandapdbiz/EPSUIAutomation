using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Automation
{
    internal class InMemoryDatabase
    {
        /// <summary>
        /// This dictionary is used to manage the data store.
        /// </summary>
        private readonly Dictionary<Type, Dictionary<Guid, BaseDataTransferObject>> _database =
            new Dictionary<Type, Dictionary<Guid, BaseDataTransferObject>>();

        internal Dictionary<Type, Dictionary<Guid, BaseDataTransferObject>> Database { get { return _database; } }

        /// <summary>
        /// This method is used to insert an object
        /// </summary>
        /// <typeparam name="T">This is the type of object being inserted</typeparam>
        /// <param name="value">This is the value of the object.</param>
        public void Insert<T>(T value) where T : BaseDataTransferObject
        {
            //if the storage list doesnt exists
            if (_database.Keys.Contains(typeof(T)) == false)
            {
                var dictionaryOfEntities = new Dictionary<Guid, BaseDataTransferObject>();
                _database.Add(typeof(T), dictionaryOfEntities);
            }
            // set entity creation date
            value.CreationDate = DateTime.Now;
            //finally insert the value
            _database[typeof(T)].Add(value.GuidId, value);
        }

        /// <summary>
        /// This method is used to select multiple results
        /// </summary>
        /// <typeparam name="T">Type of obect expected back</typeparam>
        /// <param name="predicate">The where condition</param>
        /// <returns>A List of T</returns>
        public List<T> SelectMany<T>(Func<T, bool> predicate) where T : BaseDataTransferObject
        {
            if (_database.ContainsKey(typeof(T)) == false) throw new KeyNotFoundException("The object type has not been initiated");
            return _database[typeof(T)].Values.Select(x => (T)x).Where(predicate).ToList();
        }

        /// <summary>
        /// This method is used to return a single value
        /// </summary>
        /// <typeparam name="T">This is the type of the object</typeparam>
        /// <param name="predicate">This is the where condition</param>
        /// <returns>The single object</returns>
        public T SelectTopOne<T>(Func<T, bool> predicate) where T : BaseDataTransferObject
        {
            return SelectMany(predicate).First();
        }

        /// <summary>
        /// This method updates the entities
        /// </summary>
        /// <typeparam name="T">This is the type parameter</typeparam>
        /// <param name="value">This is the value</param>
        public void Update<T>(T value) where T : BaseDataTransferObject
        {
            if (_database.ContainsKey(typeof(T)) == false) throw new KeyNotFoundException("The object type has not been initiated");
            //if (!_database[typeof(T)].Keys.Contains(value.GuidId)) throw new KeyNotFoundException("The Object doesnt exist in data store");
            // set entity creation date
            value.CreationDate = DateTime.Now;
            _database[typeof(T)][value.GuidId] = value;
        }

        /// <summary>
        /// This method is used to delete a key form the data store
        /// </summary>
        /// <typeparam name="T">This is the type object</typeparam>
        /// <param name="value">This is the value</param>
        public void Delete<T>(T value) where T : BaseDataTransferObject
        {
            if (_database.ContainsKey(typeof(T)) == false) throw new KeyNotFoundException("The object type has not been initiated");
            if (_database[typeof(Task)].Keys.Contains(value.GuidId)) throw new KeyNotFoundException("The Object doesnt exist in data store");
            _database[typeof(Task)].Remove(value.GuidId);
        }
    }
}