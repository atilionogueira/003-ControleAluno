var builder = WebApplication.CreateBuilder(args);

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
    public DateTime BirthDate { get; set; }
    public long CourseId { get; set; }
    public string UserId { get; set; } = string.Empty;
}


public class Response
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}

public class Handler
{
    // Faz o processo de criação e persiste no banco (simulado)
    public Response Handle(Request request)
    {
        return new Response
        {
            Id = 2,
            Name = request.Name,
            Email = request.Email,
            BirthDate = request.BirthDate
        };
    }
}
