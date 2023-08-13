namespace Janus.Domain.Entites;

public class AdSlot : BaseModel
{
    public DateTime Date { get; set; }
    public string? Slot1 { get; set; }
    public string? Slot2 { get; set; }
    public string? Slot3 { get; set; }
    public string? Slot4 { get; set; }
    public string? Slot5 { get; set; }
    public string? Slot6 { get; set; }
    public int ScreenId { get; set; }
    public Screen Screen{ get; set; }
}