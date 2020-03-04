using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

using AspNetCore.Identity.Mongo;

using AutoMapper;

using Blog.DataAccess.DataBaseSettings;
using Blog.DataAccess.DbContext;
using Blog.DataAccess.Entities.Identity;
using Blog.Services.Articles;
using Blog.Services.Comments;
using Blog.Services.Users;
using Blog.WebApi.MiddleWare;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Blog.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                                  });
            });

            services.Configure<BlogDatabaseSettings>(Configuration.GetSection(nameof(BlogDatabaseSettings)));
            services.AddSingleton<IBlogDatabaseSettings>(sp => sp.GetRequiredService<IOptions<BlogDatabaseSettings>>().Value);
            services.AddSingleton<IMongoDbContext, BlogMongoDbContext>();
            services.AddAutoMapper(GetAutoMapperProfilesFromAllAssemblies().ToArray());

            services.AddControllers()
                    .AddFluentValidation(x =>
                    {
                        x.RegisterValidatorsFromAssemblyContaining<UpdateUserRequest>();
                        x.RegisterValidatorsFromAssemblyContaining<UpdateArticleRequest>();
                        x.RegisterValidatorsFromAssemblyContaining<UpdateCommentRequest>();
                    });

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog API", Version = "v1" });
            });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICommentsService, CommentsService>();

            services.AddIdentityMongoDbProvider<ApplicationUser, ApplicationRole>(identityOptions =>
                                                                                  {
                                                                                      identityOptions.Password.RequiredLength = 6;
                                                                                      identityOptions.Password.RequireLowercase = false;
                                                                                      identityOptions.Password.RequireUppercase = false;
                                                                                      identityOptions.Password.RequireNonAlphanumeric = false;
                                                                                      identityOptions.Password.RequireDigit = false;
                                                                                  },
                                                                                  mongoIdentityOptions =>
                                                                                  {
                                                                                      mongoIdentityOptions.ConnectionString =
                                                                                          Configuration.GetSection("BlogDatabaseSettings")
                                                                                                       .GetSection("ConnectionString")
                                                                                                       .Value;
                                                                                  });


            services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(cfg =>
                    {
                        cfg.RequireHttpsMetadata = false;
                        cfg.SaveToken = true;
                        cfg.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = false,
                                ValidateAudience = false,
                                ValidateLifetime = false,
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                            };
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();

            app.UseCors("CorsPolicy");

            app.UseMiddleware<ExceptionHandlerMiddleWare>(); // custom middleWare exception handler class

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static IEnumerable<Type> GetAutoMapperProfilesFromAllAssemblies()
        {
            return from assembly in AppDomain.CurrentDomain.GetAssemblies()
                   from aType in assembly.GetTypes()
                   where aType.IsClass && !aType.IsAbstract && aType.IsSubclassOf(typeof(Profile))
                   select aType;
        }
    }
}