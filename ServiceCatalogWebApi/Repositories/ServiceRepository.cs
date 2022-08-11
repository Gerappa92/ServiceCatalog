using ServiceCatalogWebApi.Models;
using static ServiceCatalogWebApi.Stubs.ServiceStub;

namespace ServiceCatalogWebApi.Repositories;

public class ServiceRepository : IServiceRepository
{
    public IEnumerable<Service> GetAll()
    {
        return Services;
    }

    public void Add(Service service)
    {
        Services.Add(service);
    }

    public Service? FirstOrDefault(int id)
    {
        return Services.FirstOrDefault(x => x.Id == id);
    }

    public void Remove(Service service)
    {
        Services.Remove(service);
    }
}