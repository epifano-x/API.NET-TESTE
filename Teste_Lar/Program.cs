using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Teste_Lar.Context;
using Teste_Lar.Models.Interface;
using Teste_Lar.Services;

var builder = WebApplication.CreateBuilder(args);

// Adicione esta se��o para configurar a autentica��o JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtConfig:Secret"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false; // Ajuste conforme necess�rio para produ��o
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Registre o ConnectionContext
builder.Services.AddDbContext<ConnectionContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicione os servi�os de reposit�rio
builder.Services.AddTransient<IPessoaRepository, PessoaRepository>();
builder.Services.AddTransient<ITelefoneRepository, TelefoneRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddSingleton<AuthenticationManager>();
// Adicione os controladores
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Pipeline de requisi��es
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Adicione os middlewares de autentica��o e autoriza��o
app.UseAuthentication(); // Middleware de autentica��o
app.UseAuthorization(); // Middleware de autoriza��o

app.MapControllers();

app.Run();
