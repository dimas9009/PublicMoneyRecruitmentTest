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
            var id = _rentalRepository.AddNewRental(rental);
            var key = new ResourceIdViewModel { Id = id };
            return key;
        }
    }
}
