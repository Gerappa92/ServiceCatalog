using ServiceCatalogWebApi.Models;

namespace ServiceCatalogWebApi.Repositories;

public interface IServiceRepository
{
    IEnumerable<Service> GetAll();
    void Add(Service service);
    Service? FirstOrDefault(int id);
    void Remove(Service service);
}