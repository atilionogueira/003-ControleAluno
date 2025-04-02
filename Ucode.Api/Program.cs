using Ucode.Api.Data;
using Ucode.Api.Handlers;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Students;
using Ucode.Core.Responses;
using Microsoft.EntityFrameworkCore;




var builder = WebApplication.CreateBuilder(args);

// Configuraçao para acessar o Banco

var cnnStr = builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(cnnStr);
});


// Configurações para o Swagger

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});


// Registra o handler como um serviço transiente
builder.Services.AddTransient<IStudentHandler,StudentHandler>();

var app = builder.Build();

// Configura o uso do Swagger e a interface do Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student API V1");
    c.RoutePrefix = string.Empty;
});

// Mapeamento da rota POST para criação de estudante
app.MapPost(
    "/v1/Student", // A rota aqui é /v1/Student
    (CreateStudentRequest request, IStudentHandler handler) =>
    handler.CreateAsync(request))
    .WithName("Student: Create")
    .WithSummary("Create New Student")
    .Produces<Response<Student>>();



app.Run();






