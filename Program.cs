using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using UdemyProject1.DbContexts;
using UdemyProject1.GraphQL.GraphQLAppServices;
using UdemyProject1.Loggers;
using UdemyProject1.Middlewares;
using UdemyProject1.RESTful.RestfulAppServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

// Database
builder.Services.AddDbContext<NZWalksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"))
);
builder.Services.AddDbContext<NZWalksAuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnectionString"))
);

// builder 
builder = LoggerServices.AppBuilder(builder);

builder = RestfulAppServices.AppBuilder(builder);

builder = GraphQLAppServices.AppBuilder(builder);

// App builder
var app = builder.Build();

//if (app.Environment.IsDevelopment()){}
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGraphQL();
//});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});

app.MapControllers();

app.Run();
