using ServiceCatalogWebApi.Models;
using static ServiceCatalogWebApi.Stubs.ServiceStub;

namespace ServiceCatalogWebApi.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public IEnumerable<ServiceCategory> GetAll()
    {
        return Categories;
    }

    public bool Exists(string name)
    {
        return Categories.Exists(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }

    public void Add(ServiceCategory category)
    {
        Categories.Add(category);
    }

    public ServiceCategory? FirstOrDefault(int id)
    {
        return Categories.FirstOrDefault(x => x.Id == id);
    }

    public void Remove(ServiceCategory category)
    {
        Categories.Remove(category);
    }
}