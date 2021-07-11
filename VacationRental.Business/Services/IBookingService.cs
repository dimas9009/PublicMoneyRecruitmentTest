using VacationRental.Model.Models;

namespace VacationRental.Business
{
    public interface IBookingService
    {
        BookingViewModel GetBookingById(int bookingId);

        ResourceIdViewModel AddNewBooking(BookingViewModel model);
    }
}
