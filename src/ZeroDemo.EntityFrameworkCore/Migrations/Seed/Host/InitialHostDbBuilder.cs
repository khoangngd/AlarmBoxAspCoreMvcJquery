using ZeroDemo.EntityFrameworkCore;

namespace ZeroDemo.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly ZeroDemoDbContext _context;

        public InitialHostDbBuilder(ZeroDemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
