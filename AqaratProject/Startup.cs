using AqaratProject.Helpers;
using AqaratProject.Hubs;
using AqaratProject.Models;
using AqaratProject.Services;
using BL;
using EmailService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AqaratProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var emailConfig = Configuration
      .GetSection("EmailConfiguration")
      .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();
          
            services.AddScoped<InquiryService, ClsInquiry>();
            services.AddScoped<LogHistoryService, ClsLogHistory>();
            services.AddScoped<OfferService, ClsOffer>();
            services.AddScoped<OfferBookingService, ClsOfferBooking>();
            services.AddScoped<OfferImageService, ClsOfferImage>();
            services.AddScoped<OfferNoteService, ClsOfferNote>();
            services.AddScoped<OfferVideoService, ClsOfferVideo>();
            services.AddScoped<RegionService, ClsRegion>();
            services.AddScoped<RequestService, ClsRequest>();
            services.AddScoped<RequestNoteService, ClsRequestNote>();
            services.AddScoped<SalesRepPointService, ClsSalesRepPoint>();
            services.AddScoped<UnitService, ClsUnit>();
            services.AddScoped<SettingService, ClsSetting>();
            services.AddScoped<RealTimeNotifcationService, ClsRealTimeNotifcation>();
            services.AddScoped<ActiveUsersService, ClsActiveUsers>();
            services.Configure<TwilioSettings>(Configuration.GetSection("Twilio"));
            services.AddTransient<ISMSService, SMSService>();
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder => {
                    builder.AllowAnyOrigin();
                    //URLs are from the front-end (note that they changed
                    //since posting my original question due to scrapping
                    //the original projects and starting over)
                    builder.WithOrigins("https://localhost:44398/", "http://ismguk.com/", "https://eibtek2-001-site1.atempurl.com/", "http://localhost:60097/", "http://localhost:4200/" , "http://habibaahmedm-002-site3.atempurl.com/" , "https://habibaahmedm-002-site3.atempurl.com/")
                                     .AllowAnyHeader()
                                     .AllowAnyMethod()
                                     .AllowCredentials();
                });
            });
            services.AddControllersWithViews();
            services.AddSignalR();
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.MaxAge = TimeSpan.FromHours(3);
                options.Cookie.Name = "SessionNameBlaBlaBla";
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;  
              
               
            });
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.User.AllowedUserNameCharacters = "";
                options.Password.RequireDigit = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
                options.Lockout.MaxFailedAccessAttempts = 5;

                options.User.AllowedUserNameCharacters = string.Empty;

            }).AddErrorDescriber<CustomIdentityErrorDescriber>().AddEntityFrameworkStores<Al3QaratContext>().AddDefaultTokenProviders();    ///.AddDefaultUI();
            services.AddAuthentication()
          .AddGoogle(options =>
          {
              options.ClientId = "975214719409-pp37jcmifi7bg33254ve18ku83telt9r.apps.googleusercontent.com";
              options.ClientSecret = "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";


          });



            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "1387677424973135";
                options.AppSecret = "de6fc7e479121219c97a2e079eee1b3e";
            });
            services.ConfigureApplicationCookie(opt =>
            {
               

                opt.Cookie.MaxAge = TimeSpan.FromHours(3);
                opt.Cookie.Name = "CookieNameBlaBlaBla";
                opt.Cookie.HttpOnly = true;

                //opt.LoginPath = new PathString("/login/login");
                opt.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Home/Accessdenied");
                opt.SlidingExpiration = true;
            });
            services.AddDbContext<Al3QaratContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        


          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseCors(MyAllowSpecificOrigins);
           
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://habibaahmedm-002-site3.atempurl.com"));
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
           
            app.UseSession();
           
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(

              name: "areas",
              pattern: "{area:exists}/{controller=Home}/{action=Index}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


                endpoints.MapHub<UserHub>("/hubs/userCount");

                endpoints.MapHub<DeathlyHallowsHub>("hubs/deathyhallows");


                //endpoints.MapHub<DeathlyHallowsHub>("/hubs/houseGroup");


                endpoints.MapHub<NotificationHub>("/hubs/notification");

                endpoints.MapHub<BasicChatHub>("/hubs/basicchat");

                endpoints.MapHub<ChatHub>("/hubs/chat");

                endpoints.MapHub<OrderHub>("/hubs/order");
            });
          
        }
    }
}
