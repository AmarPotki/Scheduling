using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scheduling.Domain;
using Scheduling.Infrastructure;
using DayOfWeek = Scheduling.Domain.DayOfWeek;

namespace Scheduling.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AvailabilityController(IAvailabilityRepository availabilityRepository, IUnitOfWork unitOfWork)
        {
            _availabilityRepository = availabilityRepository;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> CreateAvailability()
        {
           


            var days = new HashSet<DayOfWeek>( new[]{ DayOfWeek.Monday, DayOfWeek.Friday });
            var dateRange = new DateRange(DateOnly.FromDateTime(DateTime.Now),
                DateOnly.FromDateTime(DateTime.Now.AddDays(8)));
            var timeRange = new TimeRange(TimeOnly.FromTimeSpan(new TimeSpan(0,9,0,0)), TimeOnly.FromTimeSpan(new TimeSpan(0, 16, 0, 0)));
            var avail = new Availability(days, dateRange, timeRange,"myAvailability",null, null, null);
           await _availabilityRepository.AddAvailability(avail,CancellationToken.None);
           await _unitOfWork.SaveChangesAsync(CancellationToken.None);
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateItem(long id)
        {
            var current = await _availabilityRepository.GetById(id, CancellationToken.None);
            //current.UpdateDateRange(new DateRange(DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            //    DateOnly.FromDateTime(DateTime.Now.AddDays(8))));
            current.ClinicianId = 1;

             _availabilityRepository.Update(current,CancellationToken.None);
            await _unitOfWork.SaveChangesAsync(CancellationToken.None);
            return Ok();
        }
    }
}
