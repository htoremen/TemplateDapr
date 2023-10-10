using Infrastructure.Persistence;
using Migration.App.Seed;

namespace Migration.App
{
    public interface IMigrationService
    {
        void Migrate();
    }

    public class MigrationService : IMigrationService
    {
        private readonly ApplicationDbContext _context;

        public MigrationService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void Migrate()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            ParamaterSeed.SeedFormParametersAsync(_context).Wait();
        }
    }
}