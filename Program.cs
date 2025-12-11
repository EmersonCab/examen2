using Examen_Parcial.Services;
using Google.Cloud.Firestore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar servicios
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<LibrosService>();
builder.Services.AddScoped<PrestamoService>();
builder.Services.AddScoped<ReservaService>();
builder.Services.AddScoped<AuthService>();


// IMPORTANTE: reemplaza TU_PROJECT_ID por el ID real de tu proyecto Firebase
FirestoreDb db = FirestoreDb.Create("examen2q4");
builder.Services.AddSingleton(db);

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