using System;
using System.Collections.Generic;
using System.Linq;
using VacationRental.Business.Validators;
using VacationRental.Model.Models;
using VacationRental.Repositories;

namespace VacationRental.Business
{
    public class BookingService : IBookingService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IEnumerable<IBookingValidator> _bookingValidators;

        public BookingService(
            IRentalRepository rentalRepository,
            IBookingRepository bookingRepository,
            IEnumerable<IBookingValidator> bookingValidators)
        {
            _rentalRepository = rentalRepository ??  throw new ArgumentNullException(nameof(rentalRepository));
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            _bookingValidators = bookingValidators ?? throw new ArgumentNullException(nameof(bookingValidators)); ;
        }

        public BookingViewModel GetBookingById(int bookingId)
        {
            return _bookingRepository.GetBookingById(bookingId);
        }

        public ResourceIdViewModel AddNewBooking(BookingViewModel model)
        {
            if (model.Nights <= 0)
                throw new ApplicationException("Nights must be positive");
            if (!_rentalRepository.CheckRentalExists(model.RentalId))
                throw new ApplicationException($"Rental with id '{model.RentalId}' not found");

            try
            {
                foreach (var validator in _bookingValidators)
                {
                    validator.ValidForBooking(model);
                }

                var id = _bookingRepository.AddNewBooking(model);
                var key = new ResourceIdViewModel { Id = id };
                return key;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to add booking", ex);
            }
        }
    }
}
