using SkillsetAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure to listen on all interfaces
//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.ListenAnyIP(5252);
//    serverOptions.ListenAnyIP(7219, configure => configure.UseHttps());
//});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register our services
builder.Services.AddHttpClient();
builder.Services.AddScoped<IExternalApiService, ExternalApiService>();

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
