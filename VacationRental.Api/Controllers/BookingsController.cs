using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Business;
using VacationRental.Model.Models;
using BookingBindingModel = VacationRental.Api.Models.BookingBindingModel;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(
            IBookingService bookingService)
        {
            _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public BookingViewModel Get(int bookingId)
        {
            var booking = _bookingService.GetBookingById(bookingId);

            return booking ?? throw new ApplicationException("Booking not found"); ;
        }

        [HttpPost]
        public ResourceIdViewModel Post(BookingBindingModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Parameter model should be defined");
            }

            return _bookingService.AddNewBooking(new BookingViewModel
            {
                Start = model.Start,
                Nights = model.Nights,
                RentalId = model.RentalId
            });
        }
    }
}
