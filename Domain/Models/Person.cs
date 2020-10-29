﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;

namespace Domain.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Birthday { get; set; }

        public int TvMazeId { get; set; }
    }
}