// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using EdFi.Ods.AdminApi.Infrastructure.Security;
using FluentValidation;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Server.AspNetCore;
using Swashbuckle.AspNetCore.Annotations;

namespace EdFi.Ods.AdminApi.Features.Connect;

[AllowAnonymous]
[SwaggerResponse(400, FeatureConstants.BadRequestResponseDescription)]
[SwaggerResponse(500, FeatureConstants.InternalServerErrorResponseDescription)]
public class ConnectController : Controller
{
    private readonly ITokenService _tokenService;
    private readonly IRegisterService _registerService;
    private const string TenantIdentifierRoutePrefix = @"{tenantIdentifier:regex(^[[A-Za-z0-9-]]+$)?}";

    public ConnectController(ITokenService tokenService, IRegisterService registerService)
    {
        _tokenService = tokenService;
        _registerService = registerService;
    }

    //[HttpPost($"Tenant2/{SecurityConstants.RegisterEndpoint}")]
    //[]
    [Route($"{TenantIdentifierRoutePrefix}/{SecurityConstants.RegisterEndpoint}")]
    [HttpPost]
    [Consumes("application/x-www-form-urlencoded"), Produces("application/json")]
    [SwaggerOperation("Registers new client", "Registers new client")]
    [SwaggerResponse(200, "Application registered successfully.")]
    public async Task<IActionResult> Register([FromForm] RegisterService.Request request)
    {
        var message = await _registerService.Handle(request);
        return Ok(new { Title = message, Status = 200 });
    }

    //[HttpPost($"Tenant2/{SecurityConstants.TokenEndpoint}")]
    [Route($"{TenantIdentifierRoutePrefix}/{SecurityConstants.TokenEndpoint}")]
    [HttpPost]
    [Consumes("application/x-www-form-urlencoded"), Produces("application/json")]
    [SwaggerOperation("Retrieves bearer token", "\nTo authenticate Swagger requests, execute using \"Authorize\" above, not \"Try It Out\" here.")]
    [SwaggerResponse(200, "Sign-in successful.")]
    public async Task<ActionResult> Token()
    {
        var a = HttpContext.Features.Get<OpenIddictServerAspNetCoreFeature>()?.Transaction?.Request;
        var request = HttpContext.GetOpenIddictServerRequest() ?? throw new ValidationException("Failed to parse token request");
        var principal = await _tokenService.Handle(request);

        return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }
}
