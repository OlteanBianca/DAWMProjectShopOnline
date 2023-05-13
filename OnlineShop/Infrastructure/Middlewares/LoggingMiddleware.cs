using System.Diagnostics;

namespace OnlineShop.Infrastructure.Middlewares
{
    public class LoggingMiddleware
    {
        #region Private Fields
        private readonly RequestDelegate next;
        private Stopwatch Stopwatch { get; set; }
        #endregion

        #region Constructors
        public LoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        #endregion

        #region Public Methods
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine(context.Request.Path);

            Stopwatch = Stopwatch.StartNew();
            await this.next(context);
            Stopwatch.Stop();

            Console.WriteLine(context.Request.Path + " - " + Stopwatch.ElapsedMilliseconds + " ms");
        }
        #endregion
    }
}
