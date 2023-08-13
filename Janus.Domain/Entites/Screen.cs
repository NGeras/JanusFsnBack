namespace Janus.Domain.Entites;

public class Screen : BaseModel
{
    public Guid ScreenAppId { get; set; }
    public string ConnectionId { get; set; }
    public string Location { get; set; }
    public string Category { get; set; }
}