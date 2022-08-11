using ServiceCatalogWebApi.Models;

namespace ServiceCatalogWebApi.Repositories;

public interface ICategoryRepository
{
    IEnumerable<ServiceCategory> GetAll();
    bool Exists(string name);
    void Add(ServiceCategory category);
    ServiceCategory? FirstOrDefault(int id);
    void Remove(ServiceCategory category);
}