using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));

// Add services to the container.

IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers();

//Buras� ba�lant�m�z�n ayar�n� yapt���m�z yer yani WebApi ye ula�acak Apileri burada belirliyoruz.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("https://localhost:7020"));
});
var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
    };
});

builder.Services.AddDependencyResolvers(new ICoreModule[]{
    new CoreModule(),
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddFluentEmail("info@admin.com") //buras� bizim mail adresimiz
    .AddSmtpSender("localhost", 25); //buras� smtp ile ba�lant� yeri

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.WithOrigins("https://localhost:7020").AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication(); //giri�

app.UseAuthorization(); //yetkilendirme

//app.Use(async (context, next) => {
//    try
//    {
//        await next(context);
//    }
//    catch (Exception ex)
//    {// buras� hatalar� daha detayl� g�rmemizi sa�lar. core da yazm���szd�r diye bakt�m ama yazmam���z buradan daha detayl� g�rebiliriz hatay�
//        throw;
//    }
//});

app.MapControllers();

app.Run();
