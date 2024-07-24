using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using NZWalks.Data.DbContexts;
using NZWalks.GraphQL.GraphQLAppServices;
using NZWalks.Helpers;
using NZWalks.Loggers;
using NZWalks.Middlewares;
using NZWalks.RESTful.RestfulAppServices;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddPooledDbContextFactory<NZWalksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")).LogTo(Console.WriteLine)
);

//builder.Services.AddDbContext<NZWalksAuthDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnectionString"))
//);

builder.Services.AddHttpContextAccessor();

// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

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

app.UseWebSockets();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});

app.MapControllers();

app.MapGraphQL();

app.Run();
