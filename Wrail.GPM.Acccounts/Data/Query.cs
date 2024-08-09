using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
using Wrail.GPM.Accounts.Service.Data.DTO;
using Wrail.GPM.Database.SQLServer.Service;
using Wrail.GPM.Status;

namespace Wrail.GPM.Accounts.Data;

public class Query : Wrail.GPM.Accounts.Service.Data.IQuery
{
    private readonly ILogger<Query> logger;
    private readonly IConnectionString connectionString;

    public Query(IConnectionString connectionString, ILogger<Query> logger)
    {
        this.logger = logger;
        this.connectionString = connectionString;
    }

    public Task<int> GetId(string accountId)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetAccountId(int Id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Service.Data.DTO.Account>> GetAccounts(List<int> publishStatus)
    {
        List<Service.Data.DTO.Account> accounts = new();

        using SqlConnection connection = new(connectionString.ConnectionString);
        using SqlCommand command = connection.CreateCommand();

        try
        {
            command.CommandType = CommandType.StoredProcedure;

            command.CommandText = "dbo.GetAccounts";

            command.Parameters.Clear();

            var _status = publishStatus.ToStatusDataTable();
            command.Parameters.Add(new SqlParameter("@status", SqlDbType.Structured)
            {
                TypeName = "dbo.Status",
                Value = _status
            });

            await connection.OpenAsync();

            SqlDataReader dr = await command.ExecuteReaderAsync();

            while (dr.Read())
            {
                Service.Data.DTO.Account account = new();

                account.Id = Convert.ToInt32(dr["Id"]);

#pragma warning disable CS8601 // Possible null reference assignment.
                account.AccountId = dr["AccountId"].ToString();
                account.ClientName = dr["ClientName"].ToString();
                account.Email = dr["Email"].ToString();
#pragma warning restore CS8601 // Possible null reference assignment.

                accounts.Add(account);
            }

            await dr.CloseAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e,
                "Source: {s}, Operation: {o}, Message: {m}, Status: {status}",
                "Wrail.GPM.Accounts.Data.Query",
                "GetAccounts(List<int> publishStatus)",
                e.Message,
                JsonConvert.SerializeObject(publishStatus));

            throw;
        }

        return accounts;
    }

    public async Task<List<AccountAttributes>> GetAccountAttributes(List<int> accountIds, List<int> publishStatus)
    {
        List<AccountAttributes> accountAttributes = new();

        using SqlConnection connection = new(connectionString.ConnectionString);
        using SqlCommand command = connection.CreateCommand();

        try
        {
            command.CommandType = CommandType.StoredProcedure;
            await connection.OpenAsync();

            foreach (int accountId in accountIds)
            {
                AccountAttributes attributes = new();

                attributes.Id = accountId;
                attributes.NumberOfLocations =  GetLocationsCount(accountId, publishStatus, command);
                attributes.NumberOfGroups = GetGroupsCount(accountId, publishStatus, command);
                attributes.NumberOfPosts = GetPostsCount(accountId, command);

                accountAttributes.Add(attributes);
                
            }
        }
        catch (Exception e)
        {
            logger.LogError(e,
                "Source: {s}, Operation: {o}, Message: {m}, AccountIds: {aids}, Status: {status}",
                "Wrail.GPM.Accounts.Data.Query",
                " GetAccountAttributes(List<int> accountIds, int status)",
                e.Message,
                JsonConvert.SerializeObject(accountIds),
                JsonConvert.SerializeObject(publishStatus));

            throw;
        }

        return accountAttributes;
    }

    private int GetLocationsCount(int accountId, List<int> publishStatus, SqlCommand command)
    {
        command.CommandText = ""; //stored procedure name for location count for the account

        command.Parameters.Clear();

        command.Parameters.Add("@accountId", SqlDbType.Int).Value = accountId;

        var _status = publishStatus.ToStatusDataTable();
        command.Parameters.Add(new SqlParameter("@status", SqlDbType.Structured)
        {
            TypeName = "dbo.Status",
            Value = _status
        });

        return Convert.ToInt32(command.ExecuteScalar());

    }

    private int GetGroupsCount(int accountId, List<int> publishStatus, SqlCommand command)
    {
        command.CommandText = ""; //stored procedure name for group count for the account

        command.Parameters.Clear();

        command.Parameters.Add("@accountId", SqlDbType.Int).Value = accountId;

        var _status = publishStatus.ToStatusDataTable();
        command.Parameters.Add(new SqlParameter("@status", SqlDbType.Structured)
        {
            TypeName = "dbo.Status",
            Value = _status
        });

        return Convert.ToInt32(command.ExecuteScalar());
    }

    private int GetPostsCount(int accountId, SqlCommand command)
    {
        command.CommandText = ""; //stored procedure name for post count for the account

        command.Parameters.Clear();

        command.Parameters.Add("@accountId", SqlDbType.Int).Value = accountId;
        //command.Parameters.Add("@status", SqlDbType.Int).Value = status;

        return Convert.ToInt32(command.ExecuteScalar());
    }
}