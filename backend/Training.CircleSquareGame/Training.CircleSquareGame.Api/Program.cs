using Training.CircleSquareGame.Api;
using Training.CircleSquareGame.Api.Abstract;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IOXGame, OXGame>();
builder.Services.AddSingleton<IDisplayText, DisplayText>();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost")
                .AllowAnyHeader()
                .WithMethods("GET", "POST")
                .AllowCredentials()
                .SetIsOriginAllowed(s => true);
        });
});

var app = builder.Build();
app.UseCors();
app.MapHub<XOHub>("/xohub");
app.Run("http://localhost:5000");