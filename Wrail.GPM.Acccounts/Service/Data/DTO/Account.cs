namespace Wrail.GPM.Accounts.Service.Data.DTO;

public class Account
{
    public int Id { get; set; }
    public string AccountId { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Status { get; set; }
}

public class AccountAttributes
{
    public int Id { get; set; }
    public int NumberOfLocations { get; set; }
    public int NumberOfGroups { get; set; }
    public int NumberOfPosts { get; set; }
}
