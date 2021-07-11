﻿using System;
using System.Collections.Generic;

namespace VacationRental.Model.Models
{
    public class CalendarDateViewModel
    {
        public DateTime Date { get; set; }
        public List<CalendarBookingViewModel> Bookings { get; set; }
    }
}
