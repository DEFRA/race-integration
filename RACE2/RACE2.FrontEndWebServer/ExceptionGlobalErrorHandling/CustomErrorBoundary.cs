using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace RACE2.FrontEndWebServer.ExceptionGlobalErrorHandling
{
    public class CustomErrorBoundary : ErrorBoundary
    {
        protected override Task OnErrorAsync(Exception exception)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == "Development")
            {
                return base.OnErrorAsync(exception); 
            }
            else
            {
                return Task.CompletedTask;
            }
        }
    }
}
