using System;
using Moq;
using VacationRental.Business.Validators;
using VacationRental.Model.Models;
using VacationRental.Repositories;
using Xunit;

namespace VacationRental.Business.Tests
{
    public class BookingServiceTests
    {
        private BookingService _targetService;
        private Mock<IRentalRepository> _rentalRepositoryMock = new Mock<IRentalRepository>();
        private Mock<IBookingRepository> _bookingRepositoryMock = new Mock<IBookingRepository>();
        private Mock<IBookingValidator> _bookingValidatorMock = new Mock<IBookingValidator>();

        public BookingServiceTests()
        {
            _targetService = new BookingService(
                _rentalRepositoryMock.Object,
                _bookingRepositoryMock.Object,
                new[] {_bookingValidatorMock.Object});
        }


        [Fact]
        public void AddNewBooking_WhenNoOverlapping_ExpectsAddingNewBooking()
        {
            // Arrange
            int id = 10;
            var bookingViewModel = new BookingViewModel
            {
                Nights = 5,
                RentalId = 1,
                Start = new DateTime(2021, 1,1)
            };
            _bookingValidatorMock.Setup(x=>x.ValidForBooking(bookingViewModel)).Verifiable();
            _bookingRepositoryMock.Setup(x=>x.AddNewBooking(bookingViewModel)).Returns(id).Verifiable();
            _rentalRepositoryMock.Setup(x => x.CheckRentalExists(bookingViewModel.RentalId)).Returns(true).Verifiable();

            // Act
            var resultKey = _targetService.AddNewBooking(bookingViewModel);

            // Assert
            Assert.Equal(id, resultKey.Id);
            _bookingValidatorMock.Verify();
            _bookingRepositoryMock.Verify();
            _rentalRepositoryMock.Verify();
        }
    }
}
