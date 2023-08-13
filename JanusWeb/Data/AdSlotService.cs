using Janus.DAL;
using Janus.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace JanusWeb.Data;

public class AdSlotService
{
    private readonly JanusDbContext _dbContext;

    public AdSlotService(JanusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AdSlot> CreateSlot(int screenId, DateTime toDate)
    {
        var slot = new AdSlot()
        {
            ScreenId = screenId,
            Date = toDate
        };
        var createdSlot = await _dbContext.AddAsync(slot);
        await _dbContext.SaveChangesAsync();
        return createdSlot.Entity;
    }

    public async Task<AdSlot> GetAdSlotForScreen(int screenId)
    {
        return await _dbContext.AdSlots.FirstOrDefaultAsync(x => x.ScreenId == screenId);
    }
    public async Task<string> UploadFile(string filePath)
    {
        // Simulate the file upload logic
        // You can implement the actual file upload logic here
        // and return the uploaded file path
        string uploadedFilePath = "path/to/uploaded/file"; // Replace with actual file path
        return await Task.FromResult(uploadedFilePath);
    }

    public async Task UpdateSlot(AdSlot adSlot)
    {
        _dbContext.Update(adSlot);
        await _dbContext.SaveChangesAsync();
    }
}