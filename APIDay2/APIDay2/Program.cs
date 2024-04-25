
using APIDay2.Models;
using APIDay2.Repos;
using APIDay2.Unit_Of_Works;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace APIDay2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string txt = "";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSwaggerGen(
                opt =>
                {

                    opt.SwaggerDoc("v1", new OpenApiInfo()
                    {
                        Version = "v2",
                        Title = "my Web API",
                        Description = "api to manage Departments and students",
                        TermsOfService = new Uri("http://tempuri.org/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "AyaShehata",
                            Email = "ayashehata@email.com"
                        }
                    });

                    opt.IncludeXmlComments("myComments.xml");
                    opt.EnableAnnotations();
                }
                );
            builder.Services.AddDbContext<ITIContext>(
                             options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            /*builder.Services.AddScoped<StudentRepo>();
            builder.Services.AddScoped<DepartmentRepo>();
            builder.Services.AddScoped<InstructorRepo>();*/
            builder.Services.AddScoped<UnitOfWork>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(txt,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            builder.Services.AddAuthentication(option => option.DefaultAuthenticateScheme = "myscheme")
               .AddJwtBearer("myscheme",
               //validate token
               op =>
               {
                   #region secret key
                   string key = "HERE IS THE SECRET KEY FOR THIS App";
                   var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                   #endregion
                   op.TokenValidationParameters = new TokenValidationParameters()
                   {
                       IssuerSigningKey = secertkey,
                       ValidateIssuer = false,
                       ValidateAudience = false

                   };
               });

            //==================new way to autherize=============================//
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Your API", Version = "v3" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                new string[] { }
            }
});
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors(txt);
            app.MapControllers();

            app.Run();
        }
    }
}
