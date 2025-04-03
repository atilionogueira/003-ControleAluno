using Ucode.Api.Data;
using Ucode.Api.Handlers;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Students;
using Ucode.Core.Responses;
using Microsoft.EntityFrameworkCore;
using Ucode.Core.Requests;
using Azure.Core;




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

// Mapeamento da rota GESTBY para localizar um estudante
app.MapGet(
    "/v1/Students", // A rota aqui é /v1/Student
    async (IStudentHandler handler) =>
    {
        var request = new GetAllStudentRequest()
                      {                       
                          UserId = "teste@teste.com"
                      };
        return await handler.GetAllAsync(request);
    })
    .WithName("Student: Get All")
    .WithSummary("Retorn All Student")
    .Produces<PagedResponse<List<Student>?>>();

// Mapeamento da rota GESTBY para localizar um estudante
app.MapGet(
    "/v1/Students/{id}", // A rota aqui é /v1/Student
    async (long id, IStudentHandler handler) =>
    {
        var request = new GetStudentByRequest
        {
            Id = id,
            UserId = "teste@teste.com"
        };
        return await handler.GetByIdAsync(request);
    })
    .WithName("Student: Get By Id")
    .WithSummary("Retorn the Student")
    .Produces<Response<Student?>>();

// Mapeamento da rota POST para criação de estudante
app.MapPost(
    "/v1/Students", // A rota aqui é /v1/Student
    async (CreateStudentRequest request, IStudentHandler handler) =>
           await handler.CreateAsync(request))
    .WithName("Student: Create")
    .WithSummary("Create New Student")
    .Produces<Response<Student>>();



// Mapeamento da rota Update para alteração do estudante
app.MapPut(
    "/v1/Students/{id}", // A rota aqui é /v1/Student
    async (long id, UpdateStudentRequest request, IStudentHandler handler) 
    =>
    {
        request.Id = id;
        return await handler.UpdateAsync(request);
    })
    .WithName("Student: Update")
    .WithSummary("Update New Student")
    .Produces<Response<Student?>>();



// Mapeamento da rota Delete para excluir o estudante
app.MapDelete(
    "/v1/Students/{id}", // A rota aqui é /v1/Student
    async (long id,  IStudentHandler handler) =>
    {
        var request = new DeleteStudentRequest
        {
            Id = id,
            UserId = "teste@teste.com"
        };  
        return await handler.DeleteAsync(request);
    })
    .WithName("Student: Delete")
    .WithSummary("Delete New Student")
    .Produces<Response<Student?>>();

app.Run();






