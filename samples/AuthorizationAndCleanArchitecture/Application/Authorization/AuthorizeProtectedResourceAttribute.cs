﻿namespace Api.Application.Authorization;

using Keycloak.AuthServices.Authorization;

/// <summary>
/// Specifies the class this attribute is applied to requires authorization.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class AuthorizeProtectedResourceAttribute : AuthorizeAttribute
{
    private readonly ResourceAuthorizationMode mode;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizeAttribute"/> class.
    /// </summary>
    public AuthorizeProtectedResourceAttribute(
        string resource,
        string scope,
        ResourceAuthorizationMode mode = ResourceAuthorizationMode.Resource)
    {
        this.mode = mode;
        this.Resource = resource;
        this.Scope = scope;
        this.Policy = ProtectedResourcePolicy.From(resource, scope);
    }

    /// <summary>
    /// Gets or sets a comma delimited list of roles that are allowed to access the resource.
    /// </summary>
    public string Resource { get; init; }

    /// <summary>
    /// Gets or sets the policy name that determines access to the resource.
    /// </summary>
    public string Scope { get; init; }

    public string? ResourceId { get; set; }

    public override string? Policy
    {
        get => this.mode == ResourceAuthorizationMode.ResourceFromRequest
            ? ProtectedResourcePolicy.From(this.Resource, this.ResourceId ?? string.Empty, this.Scope)
            : ProtectedResourcePolicy.From(this.Resource, this.Scope);
        set
        {
            // skip
        }
    }
}

public enum ResourceAuthorizationMode
{
    Resource,
    ResourceFromRequest
}
