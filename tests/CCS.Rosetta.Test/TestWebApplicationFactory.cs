using System.Data.Common;
using CCS.Rosetta.Api.Projects;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CSS.Rosetta.Test;

// ReSharper disable once ClassNeverInstantiated.Global
public class TestWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbConnection));
            services.AddSingleton<DbConnection>(_ => DataBaseConnection.CreateInMemoryConnection());
        });
    }
}