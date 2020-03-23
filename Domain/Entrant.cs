using System;
using System.Collections.Generic;
using Domain.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain
{
    [BsonIgnoreExtraElements]
    public class Entrant : BasePerson
    {
        public Entrant()
        {
            PersonType =  PersonType.Entrant;
            ActiveTo = DateTime.UtcNow.AddMonths(1);
        }

        public List<string> Faculties { get; set; }
    }
}
