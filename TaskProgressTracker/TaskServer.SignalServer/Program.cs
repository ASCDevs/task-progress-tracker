using TaskServer.SignalServer.Hubs;
using TaskServer.SignalServer.HubsControl;
using TaskServer.SignalServer.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IInterfaceHubControl, InterfaceHubControl>();
builder.Services.AddSingleton<ITarefaHubControl, TarefaHubControl>();
builder.Services.AddSingleton<ITarefaManager, TarefaManager>();
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed((host) => true)
                   .AllowCredentials();
        }));

var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.MapHub<TarefaHub>("/tarefas");
app.MapHub<InterfacesHub>("/uitarefas");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapGet("/", () => "Hello World");

app.Run();
