using System;
using System.Collections.Generic;
using VacationRental.Business.Validators;
using VacationRental.Model.Models;
using VacationRental.Repositories;

namespace VacationRental.Business.Services
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

            var rentalViewModel = _rentalRepository.GetRentalById(model.RentalId);
            try
            {
                foreach (var validator in _bookingValidators)
                {
                    validator.ValidForBooking(model);
                }

                // We should save preparation time to future if we will change Preparation Time in rental model
                model.PreparationTimeInDays = rentalViewModel.PreparationTimeInDays;

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
