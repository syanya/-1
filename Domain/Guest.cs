using System;
using Domain.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain
{
    [BsonIgnoreExtraElements]
    public class Guest: BasePerson
    {
        public Guest()
        {
            PersonType = PersonType.Guest;
            ActiveTo = DateTime.UtcNow.AddDays(1);
        }

        public string Reason { get; set; }
    }
}
