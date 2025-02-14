using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SensAPI.Extensions;
using System.Text;

namespace SensAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Registrar Swagger
            builder.Services.AddSwaggerGenWithAuth();

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters 
                    {
                       
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Su32car4ct3rK3yF0rHs256SCu5FunV4uL")),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                }); 

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API IoT v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
          
            //app.UseCors(Opt => {
            //    // limitar
            //    Opt.AllowAnyOrigin();
            //});
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
