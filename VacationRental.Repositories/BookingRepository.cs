using System;
using System.Collections.Generic;
using System.Linq;
using VacationRental.Model.Models;

namespace VacationRental.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IDictionary<int, BookingViewModel> _bookingDb;

        public BookingRepository(IDictionary<int, BookingViewModel> bookingDb)
        {
            _bookingDb = bookingDb;
        }

        public List<BookingViewModel> GetOverlappedBooking(BookingOverlapsFilter filter)
        {
            return _bookingDb.Values.Where(
                booking =>
                    (booking.RentalId == filter.RentalId) &&
                    ((booking.Start <= filter.Start.Date &&
                     booking.End > filter.Start.Date)
                    || (booking.Start < filter.End &&
                        booking.End >= filter.End)
                    || (booking.Start > filter.Start &&
                        booking.End < filter.End))
            ).ToList();
        }

        public int AddNewBooking(BookingViewModel model)
        {
            var id = _bookingDb.Keys.Count + 1;
            model.Id = id;
            _bookingDb.Add(id, model);
            return id;
        }

        public BookingViewModel GetBookingById(int bookingId)
        {
            if (!_bookingDb.TryGetValue(bookingId, out var booking))
            {
                throw new ApplicationException($"Unable to get booking with id '{bookingId}'");
            }

            return booking;
        }

        public List<BookingViewModel> GetOverlappedBookingWithExtraDaysCleaning(BookingOverlapsFilter filter, int? PreparationDays)
        {
            return _bookingDb.Values.Where(
                booking =>
                    (booking.RentalId == filter.RentalId) &&
                    (booking.Start <= filter.Start.Date &&
                     GetEndTimeWithExtraCleaning(booking) > filter.Start.Date)
                    || (booking.Start < GetEndTimeWithExtraCleaning(filter.End, PreparationDays) &&
                        GetEndTimeWithExtraCleaning(booking) >= GetEndTimeWithExtraCleaning(filter.End, PreparationDays))
                    || (booking.Start > filter.Start &&
                        GetEndTimeWithExtraCleaning(booking) < GetEndTimeWithExtraCleaning(filter.End, PreparationDays))
            ).ToList();
        }

        private DateTime GetEndTimeWithExtraCleaning(BookingViewModel booking)
        {
            if (booking.ExtraDaysCleaning.HasValue)
            {
                return booking.End.AddDays(booking.ExtraDaysCleaning.Value);
            }

            return booking.End;
        }

        private DateTime GetEndTimeWithExtraCleaning(DateTime endDate, int? extraCleaningDays)
        {
            if (extraCleaningDays.HasValue)
            {
                return endDate.AddDays(extraCleaningDays.Value);
            }

            return endDate;
        }
    }
}
