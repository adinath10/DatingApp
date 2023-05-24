using API.Extensions;
using API.Middleware;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)//configuration is injected
        {
            _config = config;
            //Configuration = configuration;
        }

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices(_config);
            // for injecting the services or class
            services.AddControllers();
            // Cross-origin resource sharing (CORS) is a browser mechanism which enables controlled 
            // access to resources located outside of a given domain
            services.AddCors();
            services.AddIdentityServices(_config);
            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //request goes through the series of middleware
            app.UseMiddleware<ExceptionMiddleware>();
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();//if any error comes then exception page will called
            //     // app.UseSwagger();
            //     // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
            // }

            app.UseHttpsRedirection();// if we go with http then it will redirect to https

            app.UseRouting(); // for navigating to different routes

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

            app.UseAuthentication();

            app.UseAuthorization(); // for authorization

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); //using endpoints we will map the controllers
            });
        }
    }
}
