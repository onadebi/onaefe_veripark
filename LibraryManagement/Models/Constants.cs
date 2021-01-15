using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.Models
{
    public static class Constants
    {
        public const string LibrarianRoleName = "librarian";
        public const string SupervisorRoleName = "supervisor";
        public static readonly List<DayOfWeek> Weekends = new DayOfWeek[] { DayOfWeek.Friday, DayOfWeek.Saturday }.ToList();
        public const int FinePerDay = 5;
        public const string Currency = "AED";
    }
}