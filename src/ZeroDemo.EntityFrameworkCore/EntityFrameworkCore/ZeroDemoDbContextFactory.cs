using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ZeroDemo.Configuration;
using ZeroDemo.Web;

namespace ZeroDemo.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class ZeroDemoDbContextFactory : IDesignTimeDbContextFactory<ZeroDemoDbContext>
    {
        public ZeroDemoDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ZeroDemoDbContext>();
            var configuration = AppConfigurations.Get(
                WebContentDirectoryFinder.CalculateContentRootFolder(),
                addUserSecrets: true
            );

            ZeroDemoDbContextConfigurer.Configure(builder, configuration.GetConnectionString(ZeroDemoConsts.ConnectionStringName));

            return new ZeroDemoDbContext(builder.Options);
        }
    }
}