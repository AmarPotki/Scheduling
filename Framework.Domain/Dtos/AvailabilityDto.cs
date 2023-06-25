namespace Framework.Domain.Dtos;

public class AvailabilityDto
{
    public string Name { get; set; }
    public TimeOnly Start { get; set; }
    public TimeOnly End { get; set; }
    public long Id { get; set; }
}