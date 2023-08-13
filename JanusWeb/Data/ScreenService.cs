using Janus.DAL;
using Janus.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace JanusWeb.Data;

public class ScreenService
{
    private readonly JanusDbContext _dbContext;
    private List<Screen> screens = new List<Screen>();

    public ScreenService(JanusDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Screen>> GetScreens()
    {
        // Simulate fetching screens from a database or other data source
        screens = await _dbContext.Screens.ToListAsync();
        return screens;
    }

    public async Task AddScreen(Screen screen)
    {
        // Simulate adding a new screen to the data source
        await _dbContext.AddAsync(screen);
        await _dbContext.SaveChangesAsync();
        screens.Add(screen);
    }

    public async Task<Screen> GetScreen(int screenId)
    {
    var context = new JanusDbContext();
    return await context.Screens.FirstOrDefaultAsync(x => x.Id == screenId);
    }

    public async Task DeleteScreen(Screen screenObj)
    {
        _dbContext.Remove(screenObj);
        await _dbContext.SaveChangesAsync();
    }
}