using Microsoft.EntityFrameworkCore;
using ProyectoGrado;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Amazon.S3;
using ProyectoGrado.Services;
using Microsoft.Extensions.Options;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          //builder.WithOrigins("http://localhost:3000");
                          builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost").AllowAnyHeader().AllowAnyMethod();
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddSwaggerGen(opt => {
    opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token})",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    opt.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddBackblazeAgent(options =>
{
    options.KeyId = "f8203b393185";
    options.ApplicationKey = "00503fc9f9875b1b39e988c30a63c8a6708e9f6bfa";
});


if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
{
    builder.Services.AddDbContext<PeliculasContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("PeliculasConnectionProd"));
    });
}
else
{
    builder.Services.AddDbContext<PeliculasContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("PeliculasConnection"));
    });
}

builder.Services.BuildServiceProvider().GetService<PeliculasContext>().Database.Migrate();

builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PeliculasContext>();
    context.Database.Migrate();
    
}


    app.UseSwagger();
    app.UseSwaggerUI();


app.UseCors(MyAllowSpecificOrigins);
//app.UseHttpsRedirection();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllers();

app.Run();
