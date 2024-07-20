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
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString"))
);
builder.Services.AddDbContext<NZWalksAuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksAuthConnectionString"))
);

// builder 

builder = LoggerServices.AppBuilder(builder);

builder = RestfulAppServices.AppBuilder(builder);

builder = GraphQLAppServices.AppBuilder(builder);

// App 
var app = builder.Build();

// Configure the HTTP request pipeline.
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
