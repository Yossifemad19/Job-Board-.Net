namespace Api.Middlewares
{
    public class CustomForbiddenMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomForbiddenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode==StatusCodes.Status403Forbidden)
            {
                var user = context.User;
                string message = "Access denied. You do not have permission to access this resource.";
                if (user.Identity.IsAuthenticated)
                {
                    if (user.IsInRole("Company"))
                        message = "Company Access Is Required";
                    else if (user.IsInRole("User"))
                        message = "User Access Is Required";
                }
                else
                {
                    message = "Authentication Is Required To Access This Resources";
                }
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(message);
            }
        }
    }
}
