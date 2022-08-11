using Microsoft.AspNetCore.Mvc;
using ServiceCatalogWebApi.Models;
using static ServiceCatalogWebApi.Stubs.ServiceStub;

namespace ServiceCatalogWebApi.Endpoints;

public static class CategoryEndpoints
{
    private const string EndpointTag = "Category";

    public static void RegisterCategoryEndpoints(this WebApplication app)
    {
        app.MapGet("/api/categories", List)
            .WithTags(EndpointTag);

        app.MapGet("/api/categories/{categoryId:int}", Get)
            .WithTags(EndpointTag);

        app.MapPost("/api/categories", Add)
            .WithTags(EndpointTag);

        app.MapPut("/api/categories/{categoryId:int}", Update)
            .WithTags(EndpointTag);

        app.MapDelete("/api/categories/{categoryId:int}", Delete)
            .WithTags(EndpointTag);
    }

    private static IResult List()
    {
        return Results.Ok(new { categories = Categories });
    }

    private static IResult Get([FromRoute] int categoryId)
    {
        var categoryFind = Categories.FirstOrDefault(x => x.Id == categoryId);
        return categoryFind == null ? Results.NotFound() : Results.Ok(new { category = categoryFind });
    }

    private static IResult Add([FromBody] AddCategoryRequest body)
    {
        if (string.IsNullOrWhiteSpace(body.Name)
            || Categories.Exists(x => x.Name.Equals(body.Name, StringComparison.InvariantCultureIgnoreCase)))
        {
            return Results.BadRequest();
        }

        var newCategory = new ServiceCategory(Categories.Max(x => x.Id) + 1, body.Name);
        Categories.Add(newCategory);
        return Results.Created($"/api/categories/{newCategory.Id}", body);
    }

    private static IResult Update(
        [FromRoute] int categoryId,
        [FromBody] AddCategoryRequest body)
    {
        var category = Categories.FirstOrDefault(x => x.Id == categoryId);
        if (category == null)
        {
            return Results.NotFound();
        }
        if (string.IsNullOrWhiteSpace(body.Name))
        {
            return Results.BadRequest();
        }

        category.Name = body.Name;
        return Results.NoContent();
    }

    private static IResult Delete(
        [FromRoute] int categoryId)
    {
        var category = Categories.FirstOrDefault(x => x.Id == categoryId);
        if (category == null)
        {
            return Results.NotFound();
        }

        Categories.Remove(category);
        return Results.NoContent();
    }
}

internal record AddCategoryRequest(string Name);