namespace ArchitectureShowcase.UserMgmt.Models;

public class B2CResponse
{
    public B2CResponse(string version, int status, string userMessage)
    {
        Version = version;
        Status = status;
        UserMessage = userMessage;
    }

    public string Version { get; set; }
    public int Status { get; set; }
    public string UserMessage { get; set; }
}
