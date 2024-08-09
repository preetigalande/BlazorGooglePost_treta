namespace Wrail.GPM.Accounts.Service.Data;

internal interface ICommand
{
    Task  ChangeStatus(string accountId, int publishStatus);
}
