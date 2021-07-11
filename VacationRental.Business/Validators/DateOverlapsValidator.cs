using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VacationRental.Model.Models;
using VacationRental.Repositories;

namespace VacationRental.Business.Validators
{
    public class DateOverlapsValidator : IBookingValidator
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRentalRepository _rentalRepository;

        public DateOverlapsValidator(
            IBookingRepository bookingRepository,
            IRentalRepository rentalRepository)
        {
            _bookingRepository = bookingRepository;
            _rentalRepository = rentalRepository;
        }

        public void ValidForBooking(BookingViewModel booking)
        {
            var filter = new BookingOverlapsFilter()
            {
                RentalId = booking.RentalId,
                Start = booking.Start,
                End = booking.End
            };

            var overLaps = _bookingRepository.GetOverlappedBooking(filter);

            var countOfUnits = _rentalRepository.GetCountOfUnit(booking.RentalId);

            if (overLaps.Count < countOfUnits)
            {
                // We can accept this booking, because count of overlaps less then count of units
                return;
            }

            var sortingOverLaps = overLaps.OrderBy(l => l.Start).ToList();
            int freeUnits = countOfUnits - 1;
            for (int i = 0; i < sortingOverLaps.Count - 1; i++)
            {
                for (int j = i + 1; j < sortingOverLaps.Count; j++)
                {
                    if (sortingOverLaps[i].End > sortingOverLaps[j].Start)
                    {
                        freeUnits--;
                    }
                }
            }

            if (freeUnits <= 0)
            {
                throw new Exception("Unable to accept booking");
            }
        }
    }
}
