using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ShiftPlanner.Server.Models
{
    public class Shift
    {
        [Key]
        public int Id { get; set; }

        public string ShiftName { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }
    }
}

