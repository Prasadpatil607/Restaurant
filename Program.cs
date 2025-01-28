

using RestaurantDemo.Handlers;
using RestaurantDemo.Infrastructure;
using RestaurantDemo.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();

builder.Services.AddSingleton<DbConnection>(); // Register DbConnection as a singleton

builder.Services.AddScoped<INavItemsHandler, NavItemsHandler>();
builder.Services.AddScoped<ISectionsHandler, SectionsHandler>();
builder.Services.AddScoped<IMenuHandler, MenuHandler>();
builder.Services.AddScoped<IContactUsHandler, ContactUsHandler>();
builder.Services.AddScoped<IFeedbackHandler, FeedbackHandler>();
builder.Services.AddScoped<IDetailsHandler, DetailsHandler>();
builder.Services.AddScoped<IServiceHoursHandler, ServiceHoursHandler>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", builder =>
        builder.WithOrigins("http://localhost:3000") // Vue app's default port
               .AllowAnyMethod()
               .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowVueApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
