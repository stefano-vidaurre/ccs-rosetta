using System.Data.Common;
using CCS.Rosetta.Api.Projects;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace CSS.Rosetta.Test;

public class TestWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<DbConnection>(_ => DataBaseConnection.CreateInMemoryConnection());
            services.AddSingleton<IProjectRepository, ProjectRepository>();
        });
    }
}