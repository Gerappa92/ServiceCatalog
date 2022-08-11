using Microsoft.AspNetCore.Mvc;
using ServiceCatalogWebApi.Models;
using static ServiceCatalogWebApi.Stubs.ServiceStub;

namespace ServiceCatalogWebApi.Endpoints;

public static class ServiceEndpoints
{
    private const string EndpointTag = "Service";

    public static void RegisterServiceEndpoints(this WebApplication app)
    {
        app.MapGet("/api/categories/{categoryId:int}/services", List)
            .WithTags(EndpointTag);

        app.MapGet("/api/services/{serviceId:int}", Get)
            .WithTags(EndpointTag);

        app.MapPost("/api/services", Add)
            .WithTags(EndpointTag);

        app.MapPut("/api/services/{serviceId:int}", Update)
            .WithTags(EndpointTag);

        app.MapDelete("/api/services/{serviceId:int}", Delete)
            .WithTags(EndpointTag);
    }

    private static IResult List([FromRoute] int categoryId, [FromQuery] int? take,
        [FromQuery] int? skip)
    {
        var services = Services.Where(x => x.Category.Id == categoryId).Skip(skip.GetValueOrDefault(0)).Take(take.GetValueOrDefault(10));
        return Results.Ok(new { services = services, total = Services.Count });
    }

    private static IResult Get([FromRoute] int serviceId)
    {
        var service = Services.FirstOrDefault(x => x.Id == serviceId);
        return service == null ? Results.NotFound() : Results.Ok(new { service = service });
    }

    private static IResult Add([FromBody] AddServiceRequest body)
    {
        if (string.IsNullOrWhiteSpace(body.Name)
            || !Categories.Exists(x => x.Id == body.CategoryId))
        {
            return Results.BadRequest();
        }

        var newService = new Service(Services.Max(x => x.Id) + 1, body.Name, Categories.First(x => x.Id == body.CategoryId));
        Services.Add(newService);
        return Results.Created($"/api/services/{newService.Id}", body);
    }

    private static IResult Update(
        [FromRoute] int serviceId,
        [FromBody] AddServiceRequest body)
    {
        var service = Services.FirstOrDefault(x => x.Id == serviceId);
        if (service == null)
        {
            return Results.NotFound();
        }
        var category = Categories.FirstOrDefault(x => x.Id == body.CategoryId);
        if (string.IsNullOrWhiteSpace(body.Name)
            || category == null)
        {
            return Results.BadRequest();
        }

        service.Name = body.Name;
        service.Category = category;
        return Results.NoContent();
    }

    private static IResult Delete(
        [FromRoute] int serviceId)
    {
        var service = Services.FirstOrDefault(x => x.Id == serviceId);
        if (service == null)
        {
            return Results.NotFound();
        }

        Services.Remove(service);
        return Results.NoContent();
    }
}

internal record AddServiceRequest(string Name, int CategoryId);