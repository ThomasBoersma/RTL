using System.Collections.Generic;

namespace Services.Models
{
    public class ShowDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<PersonDto> Cast { get; set; }
    }
}
