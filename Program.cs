using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using UdemyProject1.Data.DbContexts;
using UdemyProject1.GraphQL.GraphQLAppServices;
using UdemyProject1.Helpers;
using UdemyProject1.Loggers;
using UdemyProject1.Middlewares;
using UdemyProject1.RESTful.RestfulAppServices;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddPooledDbContextFactory<NZWalksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"))
);

builder.Services.AddDbContext<NZWalksAuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnectionString"))
);

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

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});

app.MapControllers();

app.MapGraphQL();

app.Run();
