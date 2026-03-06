using api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Configura o swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injeçao de dependencias dos services
builder.Services.AddScoped<TurmaService>();
builder.Services.AddScoped<AlunoService>();

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

