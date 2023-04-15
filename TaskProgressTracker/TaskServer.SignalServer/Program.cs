using TaskServer.SignalServer.Hubs;
using TaskServer.SignalServer.HubsControl;
using TaskServer.SignalServer.Interfaces;
using TaskTracker.Infrastructure;
using TaskTracker.Infrastructure.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IInterfaceHubControl, InterfaceHubControl>();
builder.Services.AddSingleton<ITarefaHubControl, TarefaHubControl>();
builder.Services.AddScoped<ITarefaManager, TarefaManager>();
builder.Services.AddDbContext<SQLServerContext>();
builder.Services.AddInfrastructurePersistence(builder.Configuration);
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
