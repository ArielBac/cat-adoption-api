using CatAdoptionApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Database Connection
var connectionString = builder.Configuration.GetConnectionString("CatAdoptionDbConnection");
builder.Services.AddDbContext<CatAdoptionContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers().AddNewtonsoftJson(); // Add Newtonsoft

//Possível solução para o erro de looping ao retornar os relacionamentos, porém não recomendável
//builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Cat Adoption API",
        Description = "Uma API em .NET 6 para cadastro, edição, remoção, exibição de um gatinho e listagem de gatinhos cadastrados no sistema, para adoção.",
        Contact = new OpenApiContact
        {
            Name = "Ariel Vieira",
            Email = "arielvieira65@gmail.com"
        },
        License = new OpenApiLicense
        {
            Name = "Licença: GPLv3",
            Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.html")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    Console.WriteLine(xmlFile);
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    Console.WriteLine(xmlPath);
    options.IncludeXmlComments(xmlPath);

    options.EnableAnnotations();
});

// Services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app cors
app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
