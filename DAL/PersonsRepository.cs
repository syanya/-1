using Domain;
using Domain.Enum;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace DAL
{
    public class PersonsRepository<T> : IPersonsRepository<T> where T : BasePerson
    {
        private static readonly object SyncObject = new object();
        private MongoDB.Driver.FilterDefinitionBuilder<T> builder;

        public MongoDbStorage<T> Storage { get; set; }

        private static PersonsRepository<T> instance;

        public static PersonsRepository<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncObject)
                    {
                        if (instance == null)
                        {
                            instance = new PersonsRepository<T>();
                        }
                    }
                }

                return instance;
            }
        }

        private PersonsRepository()
        {
            builder = Builders<T>.Filter;
            Storage = MongoDbStorage<T>.Instance;
        }

        public T GetPerson(long id)
        {
            FilterDefinition<T> filter = builder.Eq(t => t.Id, id);
            var result = Storage.Collection.Find(filter).FirstOrDefault();
            return result;
        }

        public T GetPerson(string phoneNumber)
        {
            FilterDefinition<T> filter = builder.AnyEq(t => t.PhoneNumbers, phoneNumber);
            return Storage.Collection.Find(filter).FirstOrDefault();
        }

        public List<T> GetPersons()
        {
            return Storage.Collection.Find(new BsonDocument()).ToList();
        }

        public List<T> GetPersons(PersonType type)
        {
            FilterDefinition<T> filter = builder.Eq(t => t.PersonType, type);
            return Storage.Collection.Find(filter).ToList<T>();
        }

        public void AddNewPerson(T person)
        {
            person.Id = MonotonicId.Generate();
            Storage.Collection.InsertOne(person);
        }

        public bool RemovePerson(long id)
        {
            try
            {
                MongoDB.Driver.FilterDefinition<T> filter = builder.Eq(t => t.Id, id);
                Storage.Collection.DeleteOne(filter);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemovePerson(string phoneNumber)
        {
            try
            {
                MongoDB.Driver.FilterDefinition<T> filter = builder.AnyEq(t => t.PhoneNumbers, phoneNumber);
                Storage.Collection.DeleteOne(filter);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
