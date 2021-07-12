using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Business.Services;
using VacationRental.Model.Models;
using VacationRental.Repositories;
using RentalBindingModel = VacationRental.Api.Models.RentalBindingModel;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public RentalViewModel Get(int rentalId)
        {
            return _rentalService.GetRentalById(rentalId);
        }

        [HttpPost]
        public ResourceIdViewModel Post(RentalBindingModel model)
        {
            var rental = new RentalViewModel
            {
                Units = model.Units,
                PreparationTimeInDays = model.PreparationTimeInDays
            };

            return _rentalService.AddRental(rental);
        }
    }
}
