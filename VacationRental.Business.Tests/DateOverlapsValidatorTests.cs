using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using VacationRental.Business.Validators;
using VacationRental.Model.Models;
using VacationRental.Repositories;
using Xunit;

namespace VacationRental.Business.Tests
{
    public class DateOverlapsValidatorTests
    {
        private DateOverlapsValidator _target;
        private Mock<IBookingRepository> _bookingRepositoryMock = new Mock<IBookingRepository>();
        private Mock<IRentalRepository> _rentalRepositoryMock = new Mock<IRentalRepository>();

        public DateOverlapsValidatorTests()
        {
            _target = new DateOverlapsValidator(_bookingRepositoryMock.Object, _rentalRepositoryMock.Object);
        }

        [Fact]
        public void ValidForBooking_WhenNoOverlapping_ShouldNoThrowException()
        {
            // Arrange
            var countOfUnits = 10;
            var rentalId = 5;
            var bookingViewModel = new BookingViewModel
            {
                Nights = 5,
                RentalId = rentalId,
                Start = new DateTime(2021, 1, 1)
            };
            _rentalRepositoryMock.Setup(x => x.GetCountOfUnit(rentalId)).Returns(countOfUnits).Verifiable();
            _bookingRepositoryMock.Setup(x => x.GetOverlappedBooking(It.IsAny<BookingOverlapsFilter>()))
                .Returns(new List<BookingViewModel>(0)).Verifiable();

            // Act
            _target.ValidForBooking(bookingViewModel);

            // Assert
            _rentalRepositoryMock.Verify();
            _bookingRepositoryMock.Verify();
        }
    }
}
