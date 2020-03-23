using Domain.Enum;

namespace Domain
{
    public class GetPersonRequest
    {
        public long Id { get; set; }

        public string PhoneNumber { get; set; }

        public PersonType PersonType { get; set; }
    }
}
