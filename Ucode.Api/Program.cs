using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ucode.Api.Data;
using Ucode.Core.Models;

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
builder.Services.AddTransient<Handler>();

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
    (Request request, Handler handler) =>
    handler.Handle(request))
    .WithName("Student: Create")
    .WithSummary("Create New Student")
    .Produces<Response>();



app.Run();

public class Request
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;     
    public long CourseId { get; set; }
   
}


public class Response
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
     public long CourseId { get; set; }
}

public class Handler(AppDbContext context)
{
        public Response Handle(Request request)
    {
        var student = new Student
        {
            Name = request.Name,
            Email = request.Email,
            CourseId = request.CourseId
         
        };

        context.Students.Add(student);
        context.SaveChanges();
       
        return new Response
        {
            Id = student.Id,
            Name = request.Name,
            CourseId = request.CourseId
        };
    }
}


