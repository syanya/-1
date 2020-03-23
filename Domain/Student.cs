using System;
using System.Collections.Generic;
using Domain.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain
{
    [BsonIgnoreExtraElements]
    public class Student : BasePerson
    {
        public Student()
        {
            PersonType = PersonType.Student;
            ActiveTo = DateTime.UtcNow.AddYears(5);
        }

        public List<string> Faculties { get; set; }

        public string Group { get; set; }
    }
}
