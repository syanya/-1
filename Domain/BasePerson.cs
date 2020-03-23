using System;
using System.Collections.Generic;
using Domain.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain
{
    [BsonIgnoreExtraElements]
    public class BasePerson
    {
        public long Id { get; set; }

        public PersonType PersonType { get; set; }

        public DateTime ActiveTo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<string> PhoneNumbers { get; set; }
    }
}
