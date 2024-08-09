using Microsoft.Extensions.Logging;
using Wrail.GPM.Accounts.Data;
using Wrail.GPM.Accounts.Service.Data.DTO;
using Wrail.GPM.Database.SQLServer.Service;
using Wrail.GPM.Return;
using Wrail.GPM.Status;

namespace Wrail.GPM.Accounts;

public class Collection : Wrail.GPM.Accounts.Service.ICollection
{
    private readonly ILogger<Collection> logger;
    private readonly Wrail.GPM.Database.SQLServer.Service.IConnectionString connectionString;
    private readonly Wrail.GPM.Accounts.Service.Data.IQuery query;

    public Collection(ILogger<Collection> logger, ILoggerFactory loggerFactory, IConnectionString connectionString)
    {
        this.logger = logger;
        this.connectionString = connectionString;

        query = new Query(connectionString, loggerFactory.CreateLogger<Wrail.GPM.Accounts.Data.Query>());
    }

    private readonly string errorMessage = "An unexpected error occurred. Please try again.\r\n" + "If the error persists please contact Wrail.";


    public async Task<Result<DTO.Collection>> GetCollectionAsync()
    {
        try
        {
            //get accounts wich are active and suspended
            List<int> publishStatus = new();
            publishStatus.Add((int)PublishStatus.Active);
            publishStatus.Add((int)PublishStatus.Suspended);

            List<Service.Data.DTO.Account> accounts = await query.GetAccounts(publishStatus);

            if (accounts.Count == 0)
            {
                return new Result<DTO.Collection>
                {
                    StatusCode = 404,
                    Message = "No accounts found.",
                    Content = new DTO.Collection
                    {
                        TotalAcounts = 0,
                        Accounts = new List<DTO.Account>()
                    }
                };
            }

            //List<Service.Data.DTO.AccountAttributes> accountAttributes
            //    = await query.GetAccountAttributes(accounts.Select(x => x.Id).ToList(), publishStatus);

            //shape the return object
            DTO.Collection collection = new()
            {
                TotalAcounts = accounts.Count,
                Accounts = GetCollectionAsync(accounts, null)
            };

            return new Result<DTO.Collection>
            {
                StatusCode = 200,
                Message = $"{accounts.Count} accounts found.",
                Content = collection
            };
        }
        catch (Exception e)
        {
            logger.LogError(e,
                "Source: {s}, Operation: {o}, Message: {m}",
                "Wrail.GPM.Accounts.Collection",
                "GetCollectionAsync()",
                e.Message);

            return new Result<DTO.Collection>
            {
                StatusCode = 505,
                Message = errorMessage,
                Content = new DTO.Collection
                {
                    TotalAcounts = 0,
                    Accounts = new List<DTO.Account>()
                }
            };
        }

    }

    private List<DTO.Account> GetCollectionAsync(List<Service.Data.DTO.Account> accounts, List<AccountAttributes> accountAttributes)
    {
        List<DTO.Account> collection = new();

        foreach (Service.Data.DTO.Account account in accounts)
        {
            //var _attributes = accountAttributes.FirstOrDefault(x => x.Id == account.Id);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            collection.Add(new DTO.Account
            {
                AccountId = account.AccountId,
                ClientName = account.ClientName,
                Email = account.Email,
                //NumberOfLocations = _attributes.NumberOfLocations,
                //NumberOfGroups = _attributes.NumberOfGroups,
                //NumberOfPosts = _attributes.NumberOfPosts,
                IsPublishingEnabled = account.Status == (int)PublishStatus.Active
            });
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        return collection;
    }
}