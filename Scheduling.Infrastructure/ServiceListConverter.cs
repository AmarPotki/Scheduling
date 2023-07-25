using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Scheduling.Domain;

namespace Scheduling.Infrastructure;

public class ServiceListConverter : ValueConverter<ServiceList, string>
{
    public ServiceListConverter() : base(
        serviceList => string.Join(",", serviceList.Select(x => x.Id)),
        str => ConvertToServiceList(str)
        //{
        //    if (string.IsNullOrWhiteSpace(str)) return null;

        //    var lst = str.Split(",", StringSplitOptions.None);
        //    var listOfService = new List<Service>();
        //    foreach (var id in lst)
        //    {
        //        var res = Service.Create(long.Parse(id));
        //        //todo: need to be refactored
        //        if (res.IsError) throw new Exception("can not parse service as value object");
        //        listOfService.Add(res.Value);
        //    }

        //    return null;
        //    // return new ServiceList(listOfService);

        //}
    )
    {
    }

    private static ServiceList ConvertToServiceList(string str)
    {
        if (string.IsNullOrWhiteSpace(str)) return null;

        var lst = str.Split(",");
        var listOfService = new List<Service>();
        foreach (var id in lst)
        {
            var res = Service.Create(long.Parse(id));
            //todo: need to be refactored
            if (res.IsError) throw new Exception("can not parse service as value object");
            listOfService.Add(res.Value);
        }

        return new ServiceList(listOfService);
    }
}