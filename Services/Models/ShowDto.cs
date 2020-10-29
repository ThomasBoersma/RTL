﻿using System;
using System.Collections.Generic;

namespace Services.Models
{
    public class ShowDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<PersonDto> Cast { get; set; }
    }
}