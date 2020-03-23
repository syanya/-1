using System;
using System.Collections.Generic;
using DAL;
using Domain;
using Domain.Enum;

namespace Services
{
    public class PersonsService<T> : IPersonsService<T> where T : BasePerson
    {
        private IPersonsRepository<T> Repository { get; set; }

        private static readonly object SyncObject = new object();

        private static PersonsService<T> instance;

        public static PersonsService<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncObject)
                    {
                        if (instance == null)
                        {
                            instance = new PersonsService<T>();
                        }
                    }
                }

                return instance;
            }
        }

        PersonsService()
        {
            Repository = PersonsRepository<T>.Instance;
        }

        public T GetPerson(long id)
        {
            return Repository.GetPerson(id);
        }

        public T GetPerson(string phoneNumber)
        {
            return Repository.GetPerson(phoneNumber);
        }

        public List<T> GetPersons()
        {
            return Repository.GetPersons();
        }

        public List<T> GetPersons(PersonType type)
        {
            return Repository.GetPersons(type);
        }

        public List<T> GetPersonsByType(PersonType type)
        {
            throw new NotImplementedException();
        }

        public void AddNewPerson(T person)
        {
            Repository.AddNewPerson(person);
        }

        public bool RemovePerson(long id)
        {
            return Repository.RemovePerson(id);
        }

        public bool RemovePerson(string phoneNumber)
        {
            return Repository.RemovePerson(phoneNumber);
        }
    }
}
