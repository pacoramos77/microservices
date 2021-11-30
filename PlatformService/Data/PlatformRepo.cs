using PlatformService.Models;

namespace PlatformService.Data
{
  public class PlatformRepo : IPlatformRepo
  {
    private AppDbContext _context;

    public PlatformRepo(AppDbContext context)
    {
      _context = context;
    }

    public void CreatePlatform(Platform platform)
    {
      if (platform == null) ArgumentNullException.ThrowIfNull(platform);

      _context.Platforms.Add(platform);
    }

    public IEnumerable<Platform> GetAllPlatforms() => _context.Platforms.ToList();

    public Platform GetPlatformById(int id) =>
      _context.Platforms.FirstOrDefault(p => p.Id == id)
        ?? throw new KeyNotFoundException();

    public bool SaveChanges() => (_context.SaveChanges() >= 0);
  }
}