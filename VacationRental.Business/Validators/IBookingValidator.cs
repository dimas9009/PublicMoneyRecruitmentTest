using VacationRental.Model.Models;

namespace VacationRental.Business.Validators
{
    public interface IBookingValidator
    {
        void ValidForBooking(BookingViewModel booking);
    }
}
