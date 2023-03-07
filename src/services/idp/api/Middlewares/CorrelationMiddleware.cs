namespace api.Middlewares
{
    public class CorrelationMiddleware
    {
        private readonly RequestDelegate _next;
        public CorrelationMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.Headers.Add("TrackId", Guid.NewGuid().ToString());

            await this._next.Invoke(context);
        }
    }
}
