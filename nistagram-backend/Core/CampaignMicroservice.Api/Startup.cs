using CampaignMicroservice.Api.Consumers;
using CampaignMicroservice.Api.Factories;
using CampaignMicroservice.Core.Interface;
using CampaignMicroservice.Core.Services;
using CampaignMicroservice.DataAccess.Implementation;
using CampaignMicroservice.DataAccessImplementation;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace CampaignMicroservice.Api
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CampaignMicroservice.Api", Version = "v1" });
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

            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<UserService>();
            services.AddScoped<CampaignService>();
            services.AddScoped<AdService>();
            services.AddScoped<ExposureDateService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICampaignRequestRepository, CampaignRequestRepository>();
            services.AddScoped<IAdRepository, AdRepository>();
            services.AddScoped<IExposureDateRepository, ExposureDateRepository>();
            services.AddScoped<ISeenByRepository, SeenByRepository>();
            services.AddScoped<UserFollowedEventConsumer>();
            services.AddScoped<UnsuccessfulStoryFollowEventConsumer>();
            services.AddScoped<UserRegisteredEventConsumer>();
            services.AddScoped<UnsuccessfulStoryUserRegistrationEventConsumer>();
            services.AddScoped<UnsuccessfulStoryUserBlockEventConsumer>();
            services.AddScoped<UserEditedEventConsumer>();
            services.AddScoped<UserMutedEventConsumer>();
            services.AddScoped<AgentEditedEventConsumer>();
            services.AddScoped<VerifiedUserEditedEventConsumer>();
            services.AddScoped<UserBlockedEventConsumer>();
            services.AddScoped<UnsuccessfulStoryUserEditEventConsumer>();

            services.AddScoped<CampaignRequestFactory>();
            services.AddScoped<VerifiedUserFactory>();
            services.AddScoped<ExposureDateFactory>();
            services.AddScoped<CampaignFactory>();
            services.AddScoped<AdFactory>();
            services.AddScoped<TargetAudienceFactory>();
            services.AddScoped<CampaignWithAgentFactory>();
            services.AddScoped<AgentFactory>();
            services.AddScoped<DTOAdFactory>();

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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CampaignMicroservice.Api v1"));
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