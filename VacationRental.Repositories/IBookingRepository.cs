using System;
using System.Collections.Generic;
using System.Text;
using VacationRental.Model.Models;

namespace VacationRental.Repositories
{
    public interface IBookingRepository
    {
        List<BookingViewModel> GetOverlappedBooking(BookingOverlapsFilter filter);
        int AddNewBooking(BookingViewModel model);
        BookingViewModel GetBookingById(int bookingId);
        List<BookingViewModel> GetOverlappedBookingWithExtraDaysCleaning(BookingOverlapsFilter filter, int? PreparationDays);
    }
}
