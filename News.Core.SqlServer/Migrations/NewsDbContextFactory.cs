using Core.News;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using News.Core.SqlServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.Core.SqlServer.Migrations
{
    public class NewsDbContextFactory : IDesignTimeDbContextFactory<NewsDbContext>
    {
        public NewsDbContext CreateDbContext(string[] args)
        {
            NewsConfiguration config = NewsConfiguration.Load();
            
            var optionsBuilder = new DbContextOptionsBuilder<NewsDbContext>();
            optionsBuilder.UseSqlServer(config.GetDefaultConnection().Value);

            return new NewsDbContext(optionsBuilder.Options);
        }
    }
}
