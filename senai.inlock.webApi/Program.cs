using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Adiciona o servi�o de controllers
builder.Services.AddControllers();

//adiciona servico jwt bearer (forma de autenticacao)
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
});

AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //valida quem est� solicitando
        ValidateIssuer = true,

        //valida quem est� recebendo
        ValidateAudience = true,

        //define se o tempo de expiracao sera validado 
        ValidateLifetime = true,

        //froma de criptografia e valida a chave de autenticacao
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("inlock-chave-autenticacao-webapi-dev")),

        //valida o tempo de expiracao do token
        ClockSkew = TimeSpan.FromMinutes(5),

        //nome do issuer (de onde est� vindo)
        ValidIssuer = "senai.inlock.webApi",

        //nome do audience (para onde est� indo)
        ValidAudience = "senai.inlock.webApi"
    };
});

//Adiciona o servi�o de Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "SENAI API InLock",
        Description = "API para gerenciamento de jogos da Empresa InLock - Projeto de API da 2 sprint de API no SENAI Informatica",
        Contact = new OpenApiContact
        {
            Name = "Marqzzs",
            Url = new Uri("https://github.com/Marqzzs/API-InLock-Senai")
        },

    });
    // Configure o Swagger para usar o arquivo XML gerado com as instru��es anteriores;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    //Usando a autentica�ao no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Value: Bearer TokenJWT ",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

});

var app = builder.Build();

//Come�a a configura��o do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
//Finaliza o processo do Swagger

//Adiciona o mapeamento dos controllers
app.MapControllers();
//adiciona a autentica��o
app.UseAuthentication();
//adiciona a autoriza��o
app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();


