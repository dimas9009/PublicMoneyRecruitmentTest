using VacationRental.Model.Models;

namespace VacationRental.Business.Services
{
    public interface IBookingService
    {
        BookingViewModel GetBookingById(int bookingId);

        ResourceIdViewModel AddNewBooking(BookingViewModel model);
    }
}
