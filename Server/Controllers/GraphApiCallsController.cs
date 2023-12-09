﻿namespace MicrosoftEntraIdMudBlazor.Server.Controllers;

[ValidateAntiForgeryToken]
[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
[AuthorizeForScopes(Scopes = ["User.ReadBasic.All user.read"])]
[ApiController]
[Route("api/[controller]")]
public class GraphApiCallsController : ControllerBase
{
    private readonly MsGraphService _graphApiClientService;

    public GraphApiCallsController(MsGraphService graphApiClientService)
    {
        _graphApiClientService = graphApiClientService;
    }

    [HttpGet]
    public async Task<IEnumerable<string>> Get()
    {
        var userData = await _graphApiClientService.GetGraphApiUser();
        if (userData == null)
            return new List<string> { "no user data" };

        return new List<string> { $"DisplayName: {userData.DisplayName}",
            $"GivenName: {userData.GivenName}", $"AboutMe: {userData.AboutMe}" };
    }
}
