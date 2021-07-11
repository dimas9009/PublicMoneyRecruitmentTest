using System;
using System.Collections.Generic;
using VacationRental.Model.Models;

namespace VacationRental.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly IDictionary<int, RentalViewModel> _rentalDb;

        public RentalRepository(IDictionary<int, RentalViewModel> rentalDd)
        {
            _rentalDb = rentalDd;
        }

        public int GetCountOfUnit(int rentalId)
        {
            if (!_rentalDb.TryGetValue(rentalId, out var rentalModel))
            {
                throw new ArgumentException($"Unable to get rental with id '{rentalId}'");
            }

            return rentalModel.Units;
        }

        public RentalViewModel GetRentalById(int modelRentalId)
        {
            if (!_rentalDb.TryGetValue(modelRentalId, out var rentalModel))
            {
                throw new ArgumentException($"Unable to get rental with id '{modelRentalId}'");
            }

            return rentalModel;
        }

        public int AddNewRental(RentalViewModel rental)
        {
            var id = _rentalDb.Keys.Count + 1;
            rental.Id = id;
            _rentalDb.Add(id, rental);
            return id;
        }

        public int? GetPreparationTime(int modelRentalId)
        {
            if (!_rentalDb.TryGetValue(modelRentalId, out var rentalModel))
            {
                throw new ArgumentException($"Unable to get rental with id '{modelRentalId}'");
            }

            return rentalModel.PreparationTimeInDays;
        }

        public bool CheckRentalExists(int modelRentalId)
        {
            return _rentalDb.ContainsKey(modelRentalId);
        }
    }
}
