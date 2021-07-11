using System;
using System.Collections.Generic;
using System.Text;

namespace VacationRental.Repositories
{
    public class BookingOverlapsFilter
    {
        public int RentalId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

    }
}
