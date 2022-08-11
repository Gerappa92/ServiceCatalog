using Microsoft.AspNetCore.Mvc;
using ServiceCatalogWebApi.Models;
using ServiceCatalogWebApi.Repositories;

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

    private static IResult List([FromServices] ICategoryRepository repository)
    {
        return Results.Ok(new { categories = repository.GetAll() });
    }

    private static IResult Get(
        [FromRoute] int categoryId,
        [FromServices] ICategoryRepository repository)
    {
        var categoryFind = repository.FirstOrDefault(categoryId);
        return categoryFind == null ? Results.NotFound() : Results.Ok(new { category = categoryFind });
    }

    private static IResult Add(
        [FromBody] AddCategoryRequest body,
        [FromServices] ICategoryRepository repository)
    {
        if (string.IsNullOrWhiteSpace(body.Name)
            || repository.Exists(body.Name))
        {
            return Results.BadRequest();
        }

        var newCategory = new ServiceCategory(repository.GetAll().Max(x => x.Id) + 1, body.Name);
        repository.Add(newCategory);
        return Results.Created($"/api/categories/{newCategory.Id}", body);
    }

    private static IResult Update(
        [FromRoute] int categoryId,
        [FromBody] AddCategoryRequest body,
        [FromServices] ICategoryRepository repository)
    {
        var category = repository.FirstOrDefault(categoryId);
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
        [FromRoute] int categoryId,
        [FromServices] ICategoryRepository repository)
    {
        var category = repository.FirstOrDefault(categoryId);
        if (category == null)
        {
            return Results.NotFound();
        }

        repository.Remove(category);
        return Results.NoContent();
    }
}

internal record AddCategoryRequest(string Name);