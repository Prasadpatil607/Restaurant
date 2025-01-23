

using RestaurantDemo.DatabaseConnection;
using RestaurantDemo.Handlers;
using RestaurantDemo.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<INavItemsRepository, NavItemsRepository>();
builder.Services.AddScoped<ISectionRepository, SectionsRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IContactUsRepository , ContactUsRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IDetailsRepository, DetailsRepository>();
builder.Services.AddScoped<IServiceHourRepository,ServiceHoursRepository>();
builder.Services.AddSingleton<DbConnection>(); // Register DbConnection as a singleton

builder.Services.AddScoped<INavItemsHandler, NavItemsHandler>();
builder.Services.AddScoped<ISectionsHandler, SectionsHandler>();
builder.Services.AddScoped<IMenuHandler, MenuHandler>();
builder.Services.AddScoped<IContactUsHandler, ContactUsHandler>();
builder.Services.AddScoped<IFeedbackHandler, FeedbackHandler>();
builder.Services.AddScoped<IDetailsHandler, DetailsHandler>();
builder.Services.AddScoped<IServiceHoursHandler, ServiceHoursHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
