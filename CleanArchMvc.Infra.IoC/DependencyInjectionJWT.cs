using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.IoC
{
    public static class DependencyInjectionJWT
    {
        public static IServiceCollection AddInfrastructureJWT(  this IServiceCollection services
                                                              , IConfiguration configuration)
        {
            //informar o tipo de autenticação
            //definier o modelo de desafio de autenticação
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            //habilita a autenticação JWT usando o esquema e desafio definidor
            //validar o token
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters { 
                    ValidateIssuer = true,
                    ValidateAudience= true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey= true,
                    //valores válidos
                    ValidIssuer=configuration["Jwt:Issuer"],
                    ValidAudience=configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                    ClockSkew = TimeSpan.Zero //tempo de vida extra do token. 0 faz com que seja exatamente o definido no token.
                };
             });

            return services;
        }
    }
}
