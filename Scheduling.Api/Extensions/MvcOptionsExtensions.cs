using Microsoft.AspNetCore.Mvc;
using Scheduling.Api.Converters;
using System.ComponentModel;
using Controllers = Microsoft.AspNetCore.Mvc;
using MinimalApis = Microsoft.AspNetCore.Http.Json;
namespace Scheduling.Api.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static partial IServiceCollection AddDateOnlyTimeOnlyStringConverters(this IServiceCollection services);


    public static partial IServiceCollection AddDateOnlyTimeOnlyStringConverters(this IServiceCollection services)
    {
        services.Configure<Controllers::JsonOptions>(options => options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter()));
        TypeDescriptor.AddAttributes(typeof(TimeOnly), new TypeConverterAttribute(typeof(TimeOnlyTypeConverter)));
        services.Configure<MinimalApis::JsonOptions>(options => options.SerializerOptions.Converters.Add(new TimeOnlyJsonConverter()));
        return services;
    }

}