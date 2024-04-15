using GamesGlobal.IntegrationTest.Fixtures;
using GamesGlobal.Web.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GamesGlobal.IntegrationTest.Shared;

[Collection("GamesGlobal Api Test Collection")]
public abstract class ApiTestBase
{
    private readonly GamesGlobalWebApplicationFactory<Program> _factory;

    protected HttpClient Client;


    protected ApiTestBase(GamesGlobalWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        Setup();
    }

    private void Setup()
    {
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        Client = client;
    }
}