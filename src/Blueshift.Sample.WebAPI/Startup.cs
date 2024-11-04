using System;
using System.IO;
using System.Reflection;
using Blueshift.Sample.WebAPI.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Blueshift.Sample.WebAPI;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Blueshift interMEDIA API",
                Description = "A RESTful WebAPI for the Blueshift interMEDIA Content Management System.",
                //TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Blueshift Software, LLC",
                    Email = "sales@blueshift-software.com",
                    Url = new Uri("https://github.com/BlueshiftSoftware/Blueshift.Sample"),
                },
                License = new OpenApiLicense
                {
                    Name = "Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International",
                    Url = new Uri("https://github.com/BlueshiftSoftware/Blueshift.Sample/blob/f161f3fbc97f2bf93f924305cf193e7edd739e9f/LICENSE"),
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        services
            .AddAutoMapper(typeof(Startup))
            .AddSampleCore()
            .AddSqlServerRepositories("name=ConnectionStrings:SampleSqlServerDb");
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app
                .UseDeveloperExceptionPage()
                .UseSwagger();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseMiddleware<LoanPolicyMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
