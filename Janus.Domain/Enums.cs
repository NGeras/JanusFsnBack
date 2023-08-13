namespace Janus.Domain;

public class Enums
{
    public enum HubMessageType
    {
        TriggerVideoDownload,
    }

    public enum HubMethodNames
    {
        TriggerDownload,
        ReceiveMessage,
        RegisterScreen,
        SendScreenStatus
    }
}