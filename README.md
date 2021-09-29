# CleanArch.Utilities

[![Build Status](https://guhke.visualstudio.com/CleanArch.Utilities/_apis/build/status/poneymusical.CleanArch.Utilities%20master%20build?branchName=master)](https://guhke.visualstudio.com/CleanArch.Utilities/_build/latest?definitionId=12&branchName=master)
![](https://img.shields.io/nuget/v/CleanArch.Utilities.Core?label=nuget.org)

## What it is

This library is built on top of [MediatR](https://github.com/jbogard/MediatR) and [FluentValidation](https://github.com/FluentValidation/FluentValidation) with two goals in mind:

- Automate requests validation (requests as in MediatR requests), so that the validation (if defined) is always executed before the request reaches the handler. If the validation fails, the handler won't be reached at all.
- Define a standard response data structure, with a status and an optional payload.

Two smaller utility libraries come with the main `Core` one :

- `DependencyInjection`: extension methods on `IServiceCollection` to allow you to easily register your handlers and validators.
- `AspNetCore`: helpers to ease the translation from a handler response to a HTTP response.

## How it works

### Responses

The library defines two types of responses :

- `ServiceResponse` carries a status, validation errors list, plain errors list and an Exception. It doesn't carry any payload and is suited for operations that don't return a proper result and where you only need to know if the operation went well or not.
- `ServiceResponse<TPayload>` is a generic type that carries all the properties of `ServiceResponse` AND a payload, which can be of any type you want (value or reference). It's suited for operations that do return a result.

```csharp
public class ServiceResponse
{
    public ServiceResponseStatus Status { get; set; }
    public IEnumerable<ValidationFailure> ValidationErrors { get; set; } = new List<ValidationFailure>();
    public IEnumerable<string> Errors { get; set; } = new List<string>();
    public Exception Exception { get; set; }

    //...
}

public class ServiceResponse<TPayload> : ServiceResponse
{
    public TPayload Payload { get; set; }

    //...
}
```

`ServiceResponseStatus` is an enum defining the possible outcomes of an operation. It's a rough mapping of what can happen in a resource-oriented REST API:

```csharp
public enum ServiceResponseStatus
{
    Ok,
    NotFound,
    BadRequest,
    Conflict,
    Forbidden,
    Unauthorized,
    UnknownError
}
```

`ValidationFailure` is a type defined in `FluentValidation`: it collects errors from the validation stage.

### Requests

A request is a `class` or a `record` that implements either `IServiceRequest` or `IServiceRequest<TResponsePayload>`:

- `IServiceRequest` defines that the response from your handler won't carry any payload.
- `IServiceRequest<TResponsePayload>` defines that the response from your handler will carry a payload, along with the type of the payload

```csharp
public class ResourceExistsRequest : IServiceRequest
{
    public string ResourceId {get; set;}
}

// or

public record RevertStringRequest(string Value) : IServiceRequest<RevertStringResponse>;
public record RevertStringResponse(string Value);
```

### Handlers

The handler is the class that will perform the operation: it takes a request in input and outputs a response which is either a `ServiceResponse` or `ServiceResponse<TPayload>` object.

```csharp
public class ResourceExistsHandler : IServiceRequestHandler<ResourceExistsRequest>
{
    private IQueryable<Guid> _collection; //This might be a DbContext

    public Task<ServiceResponse> Handle(ResourceExistsRequest request, CancellationToken cancellationToken)
    {
        var exists = _collection.Any(i => i == request.ResourceId);
        return Task.FromResult(exists ? ServiceResponseFactory.Ok() : ServiceResponseFactory.NotFound());
    }
}
```

```csharp
public class RevertStringHandler : IServiceRequestHandler<RevertStringRequest, RevertStringResponse>
{
    public Task<ServiceResponse<RevertStringResponse>> Handle(RevertStringRequest request, CancellationToken cancellationToken)
    {
        var revertedString = new string(request.Value.Reverse().ToArray());
        return Task.FromResult(ServiceResponseFactory.Ok<RevertStringResponse>(new RevertStringResponse(revertedString)));
    }
}
```

**Note:** `ServiceResponseFactory` is a collection of static methods that can help build a `ServiceResponse` or `ServiceResponse<TPayload>` object.

### Validators

Optionally, you can define a validator. These are plain FluentValidation validators:

```csharp
public class RevertStringValidator : AbstractValidator<RevertStringRequest>
{
    //Validation logic (see FluentValidation documentation)
}
```

### DI wiring - handlers and validators registration

In order to use your handlers and validators, you have to register them through your dependency injection container. At the moment, extension methods are only implemented for `Microsoft.Extensions.DependencyInjection`.

To enable your handlers and validators, simply call `IServiceCollection.AddServices(assemblyContainingYourRequestsAndHandlers)` in your `ConfigureServices` phase. It registers all your handlers, all your validators and enable the automatic validation behavior.

## ASP.net core helpers

Some extension methods are implemented in the `CleanArch.Utilities.AspNetCore` library:

- `ServiceResponseStatus.ToHttpStatusCode()` : translates the ServiceResponseStatus to a relevant HTTP status code.
- `ControllerBase.FromServiceResponse()` : creates a `ActionResult` object from a `ServiceResponse` or `ServiceResponse<TPayload>` object.

## Advanced - custom behaviors

`MediatR` offers a behavior pipeline system that allows you to perform certain operations before/after the handling process ([documentation](https://github.com/jbogard/MediatR/wiki/Behaviors)). It's somewhat comparable to the middleware pipeline in ASP.net core.

This library defines a `IServicePipelineBehavior` interfaces that allow integration with `IServiceRequest` and `ServiceResponse`. A validation behavior has been implemented and is activated when you use the `AddServices()` method.

You also have the possibility to define your own implementations of `IServicePipelineBehavior` and register them through the DI, with the overload of `AddServices()`. In this case, the validation behavior won't be automatically added, so you have to register it yourself if needed.

Two `IServicePipelineBehavior` interfaces are defined:

- `IServicePipelineBehavior<TRequest>` which works with `IServiceRequest`
- `IServicePipelineBehavior<TRequest, TResponsePayload>` which works with `IServiceRequest<TResponsePayload>`

A behavior can fall into two categories:

- The behavior is a closed type and implements `IServicePipelineBehavior<TRequest>` or `IServicePipelineBehavior<TRequest, TResponsePayload>` for a limited list of types:

```csharp
public class MyBehavior : IServicePipelineBehavior<MyRequest>, IServicePipelineBehavior<MyOtherRequest>
```

In that case, the behavior will be registered only for the types it implements.

- The behavior is a generic type:

```csharp
public class MyBehavior<TRequest, TResponsePayload> : IServicePipelineBehavior<TRequest, TResponsePayload>
```

In that case, the behavior will be registered **for all** requests (if it implements `IServicePipelineBehavior<TRequest>`) or request/response combinations (if it implements `IServicePipelineBehavior<TRequest, TResponsePayload>`).
