using System;
using System.Collections.Generic;

namespace Domain
{
    public class PersonRequest : BasePerson
    {
        public List<string> Faculties { get; set; }

        public string Group { get; set; }

        public string Reason { get; set; }

        public string Сathedra { get; set; }
    }
}
