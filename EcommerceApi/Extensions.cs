using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace EcommerceApi
{
    public static class Extensions
    {
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> source , int page =1, int size = 10) where T : class {
         if(page <= 0)
            {
                page = 1;
            }
         if(size <= 0)
            {
                size = 10;
            }
          int total = source.Count();
          var totalPages = (int) Math.Ceiling((decimal)total / size);
            var result = source.Skip((page - 1) * size).Take(size).ToList();
            return result;
        }

        public static void AddCustomJwtAuth(this IServiceCollection services , ConfigurationManager configuration)
        {
            services.AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]))
                    };
                });
        }

        public static void AddSwaggerGenJwtAuth(this IServiceCollection services )
        {
            services.AddSwaggerGen(o=> {
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your Json Web Token .."
                }
                );
                o.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",


                            }, In = ParameterLocation.Header,
                           Name = "Bearer"

                        },
                        new List<string>()
                    }
                });

            });

          
        }
    }
}
