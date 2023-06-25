using Framework.Domain.Dtos;
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

        [HttpGet("GetAvailabilities")]
        public async Task<IActionResult> GetAvailabilities(DateTime from, DateTime to)
        {
            var all = await _availabilityRepository.GetByDates(DateOnly.FromDateTime(from), DateOnly.FromDateTime(to));
            var result = new Dictionary<DateOnly, List<AvailabilityDto>>();

            var currentDate = DateOnly.FromDateTime(from);
            while (currentDate < DateOnly.FromDateTime(to))
            {
              var availabilities = new List<AvailabilityDto>();
                foreach (var availability in all)
                {
                    if (availability.IsOccurring(currentDate))
                    {
                        availabilities.Add(new AvailabilityDto
                        {
                            Id=availability.Id,
                            Name = availability.Name,
                            Start = availability.TimeRange.StartTime,
                            End = availability.TimeRange.EndTime,
                        });
                    }
                }
                result.Add(currentDate, availabilities);
                currentDate = currentDate.AddDays(1);
            } 




            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> CreateAvailability()
        {
            var days = new HashSet<DayOfWeek>(new[] { DayOfWeek.Monday, DayOfWeek.Friday });
            var dateRange = new DateRange(DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
                DateOnly.FromDateTime(DateTime.Now.AddDays(13)));
            var timeRange = new TimeRange(TimeOnly.FromTimeSpan(new TimeSpan(0, 9, 0, 0)), TimeOnly.FromTimeSpan(new TimeSpan(0, 16, 0, 0)));
            var avail = new Availability(days, dateRange, timeRange, "myAvailability3", null, null, null);
            await _availabilityRepository.AddAvailability(avail, CancellationToken.None);
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

            _availabilityRepository.Update(current, CancellationToken.None);
            await _unitOfWork.SaveChangesAsync(CancellationToken.None);
            return Ok();
        }


    }
}
