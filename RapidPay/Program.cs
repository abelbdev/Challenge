using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using RapidPay.Persistence;
using RapidPay.Persistence.Infrastructure.Data;
using RapidPay.Services;
using RapidPay.Services.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncBaseRepository<>));
builder.Services.AddDbContext<RapidPayDbContext>(op =>
{
    op.UseInMemoryDatabase("RapidPayDb");
});

builder.Services.AddSingleton<IPaymentFeeService, PaymentFeeService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
