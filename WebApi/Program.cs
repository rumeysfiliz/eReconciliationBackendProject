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

//Burasý baðlantýmýzýn ayarýný yaptýðýmýz yer yani WebApi ye ulaþacak Apileri burada belirliyoruz.
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
    .AddFluentEmail("info@admin.com") //burasý bizim mail adresimiz
    .AddSmtpSender("localhost", 25); //burasý smtp ile baðlantý yeri

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.WithOrigins("https://localhost:7020").AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication(); //giriþ

app.UseAuthorization(); //yetkilendirme

//app.Use(async (context, next) => {
//    try
//    {
//        await next(context);
//    }
//    catch (Exception ex)
//    {// burasý hatalarý daha detaylý görmemizi saðlar. core da yazmýþýszdýr diye baktým ama yazmamýþýz buradan daha detaylý görebiliriz hatayý
//        throw;
//    }
//});

app.MapControllers();

app.Run();
