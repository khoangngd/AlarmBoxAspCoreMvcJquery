using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ZeroDemo.EntityFrameworkCore
{
    public static class ZeroDemoDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ZeroDemoDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ZeroDemoDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}