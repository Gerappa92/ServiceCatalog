using ServiceCatalogWebApi.Models;

namespace ServiceCatalogWebApi.Stubs;

public static class ServiceStub
{
    private static ServiceCategory Communication = new ServiceCategory(1, nameof(Communication));
    private static ServiceCategory Management = new ServiceCategory(2, nameof(Management));
    private static ServiceCategory Software = new ServiceCategory(3, nameof(Software));
    private static ServiceCategory DevOps = new ServiceCategory(4, nameof(DevOps));

    public static List<ServiceCategory> Categories = new List<ServiceCategory>
    {
        Communication,
        Management,
        Software,
        DevOps
    };

    private static Service Confluence = new Service(1, nameof(Confluence), Communication);
    private static Service ServiceDesk = new Service(4, nameof(ServiceDesk), Communication);
    private static Service Jira = new Service(2, nameof(Jira), Management);
    private static Service Artifactory = new Service(3, nameof(Artifactory), DevOps);
    private static Service Bamboo = new Service(5, nameof(Bamboo), DevOps);
    private static Service Password = new Service(6, nameof(Password), Software);
    private static Service Jenkins = new Service(7, nameof(Jenkins), DevOps);
    private static Service Miro = new Service(8, nameof(Miro), Software);
    private static Service GitLab = new Service(9, nameof(GitLab), DevOps);
    private static Service GitHub = new Service(10, nameof(GitHub), DevOps);
    private static Service Bitbucket = new Service(11, nameof(Bitbucket), DevOps);

    public static List<Service> Services = new List<Service>()
    {
        Confluence,
        ServiceDesk,
        Artifactory,
        Jira,
        Artifactory,
        Bamboo,
        Password,
        Jenkins,
        Miro,
        GitLab,
        Bitbucket
    };
}