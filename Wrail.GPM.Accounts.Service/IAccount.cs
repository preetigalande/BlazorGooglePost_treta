using Wrail.GPM.Return;

namespace Wrail.GPM.Accounts.Service;

public interface IAccount
{
    Task<Result> SuspendPublishing(string accountId);

    Task<Result> ResumePublishing(string accountId);
}