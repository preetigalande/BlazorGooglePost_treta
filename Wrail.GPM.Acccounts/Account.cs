using Wrail.GPM.Return;

namespace Wrail.GPM.Accounts
{
    public class Account : Wrail.GPM.Accounts.Service.IAccount
    {
        public Task<Result> ResumePublishing(string accountId)
        {
            throw new NotImplementedException();
        }

        public Task<Result> SuspendPublishing(string accountId)
        {
            throw new NotImplementedException();
        }
    }
}
