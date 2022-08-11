namespace ServiceCatalogWebApi.Models;

public class ServiceCategory
{
    public ServiceCategory(int id, string name)
    {
        this.Id = id;
        this.Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}