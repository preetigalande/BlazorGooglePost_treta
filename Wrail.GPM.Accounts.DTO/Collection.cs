namespace Wrail.GPM.Accounts.DTO;

public class Collection
{
    public Collection()
    {
        Accounts = new List<Account>();
    }

    public int TotalAcounts { get; set; }
    public List<Account> Accounts { get; set;}
}

public class Account
{
    public string AccountId { get; set; } = null!;
    public string ClientName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int NumberOfLocations { get; set; }
    public int NumberOfGroups { get; set; }
    public int NumberOfPosts { get; set; }
    public bool IsPublishingEnabled { get; set; }
}
