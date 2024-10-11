namespace AtHome.Shared.Errors;

public static class ApiErrors
{
    public static Error NotLoaded() => Error.Failure("Api.NotLoaded");
}