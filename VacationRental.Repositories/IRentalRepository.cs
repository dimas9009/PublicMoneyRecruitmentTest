using System;
using System.Collections.Generic;
using System.Text;
using VacationRental.Model.Models;

namespace VacationRental.Repositories
{
    public interface IRentalRepository
    {
        int GetCountOfUnit(int rentalId);

        bool CheckRentalExists(int modelRentalId);

        int? GetPreparationTime(int modelRentalId);

        RentalViewModel GetRentalById(int rentalId);

        int AddNewRental(RentalViewModel rental);
    }
}
