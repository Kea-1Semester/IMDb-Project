    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;

namespace integration.tests.DbConnection
{
    public class TestDbContextFactory(IDbContextFactory<ImdbContext> dbContextFactory)
        : IDbContextFactory<ImdbContext>
    {
        private readonly ImdbContext _options = dbContextFactory.CreateDbContext();

        public ImdbContext CreateDbContext()
        {
            return _options;
        }
    }
}
