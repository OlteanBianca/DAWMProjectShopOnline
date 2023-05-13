using OnlineShop.Infrastructure.Middlewares;
using OnlineShop.Settings;

var builder = WebApplication.CreateBuilder(args);
Dependencies.Inject(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authorization");
        c.RoutePrefix = string.Empty;
    });
}

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
