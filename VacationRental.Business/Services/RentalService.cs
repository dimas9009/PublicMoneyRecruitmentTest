using System;
using VacationRental.Model.Models;
using VacationRental.Repositories;

namespace VacationRental.Business.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public RentalViewModel GetRentalById(int rentalId)
        {
            return _rentalRepository.GetRentalById(rentalId);
        }

        public ResourceIdViewModel AddRental(RentalViewModel rental)
        {
            if (rental.Units <= 0)
                throw new ArgumentException("Units should be greater than zero");
            if (rental.PreparationTimeInDays < 0)
                throw new ArgumentException("Rental preparation time should be greater than or equal to zero");

            var id = _rentalRepository.AddNewRental(rental);
            var key = new ResourceIdViewModel { Id = id };
            return key;
        }
    }
}
