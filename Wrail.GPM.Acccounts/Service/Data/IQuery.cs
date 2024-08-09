namespace Wrail.GPM.Accounts.Service.Data;

public interface IQuery
{
    Task<int> GetId(string accountId);

    Task<string> GetAccountId(int id);

    Task<List<DTO.Account>> GetAccounts(List<int> publishStatus);

    Task<List<DTO.AccountAttributes>> GetAccountAttributes(List<int> accountIds, List<int> publishStatus);
}