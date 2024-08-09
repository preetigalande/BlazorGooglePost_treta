using Wrail.GPM.Return;

namespace Wrail.GPM.Accounts.Service
{
    public interface ICollection
    {
        Task<Result<DTO.Collection>> GetCollectionAsync();

        ///// <summary>
        ///// Gets the locations for the account from google for the account, saves the locations to the database and returns the id of the account.
        ///// </summary>
        ///// <param name="accountName"></param>
        ///// <returns></returns>
        //Task<Result<int>> ProcessLocations(string accountName);
    }
}
