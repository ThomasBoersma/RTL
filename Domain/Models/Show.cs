﻿using System.Collections.Generic;

namespace Domain.Models
{
    public class Show
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Person> Cast { get; set; }

        public int TvMazeId { get; set; }
    }
}
