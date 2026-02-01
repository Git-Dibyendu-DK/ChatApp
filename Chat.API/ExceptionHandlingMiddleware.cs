namespace Chat.API
{
    public class ExceptionHandlingMiddleware(RequestDelegate request)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await request(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new
                {
                    message = ex.Message
                });
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new
                {
                    message = "Something Went Wrong"
                });
            }
        }
    }
}
