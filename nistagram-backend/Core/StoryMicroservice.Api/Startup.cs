using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using StoryMicroservice.DataAccess.Implementation;
using StoryMicroservice.Core.Interface.Repository;
using StoryMicroservice.Core.Services;
using StoryMicroservice.DataAccess.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using System.Reflection;
using StoryMicroservice.Api.Consumers;

namespace StoryMicroservice.Api
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StoryMicroservice.Api", Version = "v1" });
            });

            var bus = RabbitHutch.CreateBus(Configuration["RabbitMQ:ConnectionString"]);
            services.AddSingleton<IBus>(bus);
            services.AddSingleton<MessageDispatcher>();

            services.AddSingleton<AutoSubscriber>(_ =>
            {
                return new AutoSubscriber(_.GetRequiredService<IBus>(), Assembly.GetExecutingAssembly().GetName().Name)
                {
                    AutoSubscriberMessageDispatcher = _.GetRequiredService<MessageDispatcher>()
                };
            });

            services.AddScoped<UserService>();
            services.AddScoped<StoryService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStoryRepository, StoryRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IHighlightRepositry, HighlightRepository>();
            services.AddScoped<RegisteredUserFactory>();
            services.AddScoped<LocationFactory>();
            services.AddScoped<HashTagFactory>();
            services.AddScoped<StoryFactory>();
            services.AddScoped<HighlightFactory>();
            services.Configure<StoryDatabaseSettings>(
            Configuration.GetSection(nameof(StoryDatabaseSettings)));

            services.AddScoped<UnsuccessfulPostUserRegistrationEventConsumer>();
            services.AddScoped<UnsuccessfulPostUserEditEventConsumer>();
            services.AddScoped<CampaignUserRegisteredEventConsumer>();
            services.AddScoped<CampaignUserEditedEventConsumer>();
            services.AddScoped<CampaignUserBlockedEventConsumer>();
            services.AddScoped<CampaignUserFollowedEventConsumer>();

            services.AddSingleton<IStoryDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<StoryDatabaseSettings>>().Value);

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = Configuration["Jwt:Issuer"],
                  ValidAudience = Configuration["Jwt:Issuer"],
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
              };
          });

            services.AddControllersWithViews()
             .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddAuthorization();
            services.AddHostedService<Worker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("MyPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StoryMicroservice.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}