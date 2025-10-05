namespace Project.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var user = context.User?.Identity?.Name ?? "Anonymous";
            var path = context.Request.Path;
            var time = DateTime.UtcNow;

            _logger.LogInformation("Request Log: Path={Path}, User={User}, Time={Time}", path, user, time);

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }

}
