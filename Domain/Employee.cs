using System;
using Domain.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain
{
    [BsonIgnoreExtraElements]
    public class Employee : BasePerson
    {
        public Employee()
        {
            PersonType = PersonType.Employee;
            ActiveTo = DateTime.UtcNow.AddYears(1);
        }

        public string Сathedra { get; set; }
    }
}
