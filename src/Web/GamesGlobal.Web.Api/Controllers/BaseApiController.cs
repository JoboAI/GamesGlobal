using AutoMapper;
using GamesGlobal.Web.Api.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GamesGlobal.Web.Api.Controllers;

/// <summary>
///     Provides a base class for API controllers to include common services like Mediator, Logger, and Mapper.
/// </summary>
/// <typeparam name="T">The type of the derived controller.</typeparam>
[ApiController]
[Produces("application/json")]
[ValidateApiModelState]
public abstract class BaseApiController<T> : ControllerBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseApiController{T}" /> class with specified services.
    /// </summary>
    /// <param name="mediator">The mediator service for dispatching CQRS operations.</param>
    /// <param name="logger">The logger service for logging.</param>
    /// <param name="mapper">The AutoMapper service for object-to-object mapping.</param>
    /// <exception cref="ArgumentNullException"></exception>
    protected BaseApiController(IMediator mediator, ILogger<T> logger, IMapper mapper)
    {
        Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    ///     Provides mediator instance to derived controllers for handling CQRS operations.
    /// </summary>
    protected IMediator Mediator { get; }

    /// <summary>
    ///     Provides logger instance to derived controllers for logging capabilities.
    /// </summary>
    protected ILogger<T> Logger { get; }

    /// <summary>
    ///     Provides AutoMapper instance to derived controllers for object-to-object mapping.
    /// </summary>
    protected IMapper Mapper { get; }
}