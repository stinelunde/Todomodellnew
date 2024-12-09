using Serilog;

public class ExcpetionHandlingMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    public ExcpetionHandlingMiddleware(RequestDelegate requestDelegate)
    {
        _requestDelegate = requestDelegate;
    }

    public async Task InvokeAsync(HttpContext context)      //ved feil i applikasjonen fanger vi dette og lar ikke programmet krasje ved å innføre try/catch med feilmelding
    {                                                          //i reponsen
        try
        {
            await _requestDelegate(context);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Unhandled exception");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("En feil oppsto, prøv igjen senere.");
        }
    }
}