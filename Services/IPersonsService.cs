using System.Collections.Generic;
using Domain;
using Domain.Enum;

namespace Services
{
    public interface IPersonsService<T> where T: BasePerson
    {
        T GetPerson(long id);

        T GetPerson(string phoneNumber);

        List<T> GetPersons();

        List<T> GetPersons(PersonType type);

        void AddNewPerson(T person);

        bool RemovePerson(long id);

        bool RemovePerson(string phoneNumber);
    }
}
