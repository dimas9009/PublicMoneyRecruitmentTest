using System;
using System.Collections.Generic;
using System.Text;
using VacationRental.Model.Models;

namespace VacationRental.Business.Services
{
    public interface IRentalService
    {
        RentalViewModel GetRentalById(int rentalId);
        ResourceIdViewModel AddRental(RentalViewModel rental);
    }
}
