using Framework.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scheduling.Api.Commands;
using Scheduling.Domain;
using Scheduling.Infrastructure;
using DayOfWeek = Scheduling.Domain.DayOfWeek;

namespace Scheduling.Api.Controllers
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
                            Id = availability.Id,
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
            var dateRange = DateRange.Create(DateOnly.FromDateTime(DateTime.Now.AddDays(3)), DateOnly.FromDateTime(DateTime.Now.AddDays(13)));
            var timeRange = TimeRange.Create(TimeOnly.FromTimeSpan(new TimeSpan(0, 9, 0, 0))
                , TimeOnly.FromTimeSpan(new TimeSpan(0, 16, 0, 0)));
            var avail = Availability.Create("myAvailability3", days, dateRange.Value,
                timeRange.Value, null, null, null).Value;
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
           // current.Clinician = Clinician.Create(1);

            _availabilityRepository.Update(current, CancellationToken.None);
            await _unitOfWork.SaveChangesAsync(CancellationToken.None);
            return Ok();
        }



        [HttpPost("ScenarioOne")]
        public async Task<IActionResult> ScenarioOne()
        {
            //get availabilityDto 

            var days = new HashSet<DayOfWeek>
            {
                DayOfWeek.Monday, DayOfWeek.Friday, DayOfWeek.Thursday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
                DayOfWeek.Saturday, DayOfWeek.Sunday
            };
            var dateRange = DateRange.Create(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(7)));
            var timeRange = TimeRange.Create(TimeOnly.FromTimeSpan(new TimeSpan(0, 9, 0, 0)), TimeOnly.FromTimeSpan(new TimeSpan(0, 16, 0, 0)));
            var services = new ServiceList(new List<Service>{Service.Create(1).Value, Service.Create(2).Value });

            var avail = Availability.Create("a-week-plan", days, dateRange.Value, timeRange.Value, null, null, services);
            await _availabilityRepository.AddAvailability(avail.Value, CancellationToken.None);
            await _unitOfWork.SaveChangesAsync(CancellationToken.None);

            var loadedAvail = await _availabilityRepository.GetById(avail.Value.Id, CancellationToken.None);
            var date = DateOnly.FromDateTime(DateTime.Now.AddDays(3));
            if (loadedAvail.IsOccurring(date))
            {
                loadedAvail.ExcludeDay(date);

                var newAvailability = Availability.Create("somethingnew", new HashSet<DayOfWeek> { date.DayOfWeek },
                     DateRange.Create(date, date).Value, TimeRange.Create(loadedAvail.TimeRange.StartTime, loadedAvail.TimeRange.EndTime).Value
                    , loadedAvail.Clinician, loadedAvail.Location, loadedAvail.Services);

                _availabilityRepository.Update(loadedAvail, CancellationToken.None);
                await _availabilityRepository.AddAvailability(newAvailability.Value, CancellationToken.None);
                await _unitOfWork.SaveChangesAsync(CancellationToken.None);
                return Ok((loadedAvail, newAvailability));

            }

            return Ok();
        }

        [HttpPost("ScenarioTwo")]
        public async Task<IActionResult> ScenarioTwo()
        {
            //get availabilityDto
            var date = DateOnly.FromDateTime(DateTime.Now.AddDays(4));
            var endate = DateOnly.FromDateTime(DateTime.Now.AddDays(16));
            var command = new AvailabilityCommand
            {
                TimeRange =
                 TimeRange.Create(TimeOnly.FromTimeSpan(new TimeSpan(0, 9, 0, 0)),
                TimeOnly.FromTimeSpan(new TimeSpan(0, 16, 0, 0))).Value,
                NewStartDate = date,
                NewEndDate = endate,
                ClinicianId = 1,
                DayOfWeeks = new HashSet<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Friday, DayOfWeek.Thursday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Saturday, DayOfWeek.Sunday },
                Name = "byNewStartDate"
            };

            var days = new HashSet<DayOfWeek>(new[] { DayOfWeek.Monday, DayOfWeek.Friday, DayOfWeek.Thursday, });
            var dateRange = DateRange.Create(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(14)));
            var timeRange = TimeRange.Create(TimeOnly.FromTimeSpan(new TimeSpan(0, 9, 0, 0)), TimeOnly.FromTimeSpan(new TimeSpan(0, 16, 0, 0)));
            var avail = Availability.Create("two-week-plan", days, dateRange.Value, timeRange.Value, null, null, null).Value;
            await _availabilityRepository.AddAvailability(avail, CancellationToken.None);
            await _unitOfWork.SaveChangesAsync(CancellationToken.None);

            var loadedAvail = await _availabilityRepository.GetById(avail.Id, CancellationToken.None);

            if (loadedAvail.IsOccurring(command.NewStartDate))
            {
                //split availability to two
                loadedAvail.Archive(date);
                _availabilityRepository.Update(loadedAvail, CancellationToken.None);

                var newAvailability = Availability.Create(command.Name, command.DayOfWeeks,
                DateRange.Create(command.NewStartDate, command.NewEndDate).Value,
                command.TimeRange, loadedAvail.Clinician, loadedAvail.Location, loadedAvail.Services).Value;

                await _availabilityRepository.AddAvailability(newAvailability, CancellationToken.None);
                await _unitOfWork.SaveChangesAsync(CancellationToken.None);
                return Ok((loadedAvail, newAvailability));

            }

            return Ok();
        }

        [HttpPost("CraeteAvailability")]
        public async Task<IActionResult> CreateAvailability(CreateAvailability command)
        {
            var a = command;
            var b = TimeOnly.Parse("12:02:15");
            return Ok();
        }

    }
}
