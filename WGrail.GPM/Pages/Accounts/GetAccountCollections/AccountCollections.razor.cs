namespace Wrail.GPM.FE.Blazor.Pages.Accounts.GetAccounCollections;

public class Accounts
{
    public bool HasError { get; private set; }
    public bool IsInfo { get; private set; }
    public string Message { get; private set; } = String.Empty;

    public void SetErrorMessage(string message)
    {
        this.HasError = true;
        this.IsInfo = false;
        this.Message = message.Replace("\r\n", "<br />");
    }

    public void SetInfoMessage(string message)
    {
        this.HasError = false;
        this.IsInfo = true;
        this.Message = message.Replace("\r\n", "<br />");
    }

    public Wrail.GPM.Accounts.DTO.Collection Collection { get; set; } = null!;
}