namespace ServiceCatalogWebApi.Models;

public class Service
{
    public Service(int id,
        string name,
        ServiceCategory category)
    {
        this.Id = id;
        this.Name = name;
        this.Category = category;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public ServiceCategory Category { get; set; }
}