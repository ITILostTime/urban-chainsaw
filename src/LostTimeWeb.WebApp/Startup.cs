﻿using System.Security.Claims;
using System.Text;
using LostTimeDB;
using LostTimeWeb.WebApp.Authentication;
using LostTimeWeb.WebApp.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace LostTimeWeb.WebApp
{
    public class Startup
    {
        public Startup( IHostingEnvironment env )
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath( env.ContentRootPath )
                .AddJsonFile( "appsettings.json", optional: true, reloadOnChange: true )
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices( IServiceCollection services )
        {
            services.AddOptions();

            string secretKey = Configuration[ "JwtBearer:SigningKey" ];
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey( Encoding.ASCII.GetBytes( secretKey ) );

            services.Configure<TokenProviderOptions>( o =>
            {
                o.Audience = Configuration[ "JwtBearer:Audience" ];
                o.Issuer = Configuration[ "JwtBearer:Issuer" ];
                o.SigningCredentials = new SigningCredentials( signingKey, SecurityAlgorithms.HmacSha256 );
            } );

            services.AddMvc();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Permission", policy =>
                          policy.RequireClaim(ClaimTypes.Role, "ADMIN"));
            });

            services.AddSingleton( _ => new UserAccountGateway( Configuration[ "ConnectionStrings:LostTimeDB" ] ) );
            services.AddSingleton( _ => new NewsGateway( Configuration[ "ConnectionStrings:LostTimeDB" ] ) );
            services.AddSingleton( _ => new QuestGateway( Configuration[ "ConnectionStrings:LostTimeDB" ] ) );
            //services.AddSingleton( _ => new QuestPatternGateway( Configuration[ "ConnectionStrings:LostTimeDB" ] ) );
            services.AddSingleton<PasswordHasher>();
            services.AddSingleton<UserService>();
            services.AddSingleton<TokenService>();
            services.AddSingleton<NewsService>();
            services.AddSingleton<QuestService>();
            //services.AddSingleton<QuestPatternService>();
            services.AddSingleton<UserProfileService>();
        }

        public void Configure( IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory )
        {
            loggerFactory.AddConsole();

            if( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }

            string secretKey = Configuration[ "JwtBearer:SigningKey" ];
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey( Encoding.ASCII.GetBytes( secretKey ) );

            app.UseJwtBearerAuthentication( new JwtBearerOptions
            {
                AuthenticationScheme = JwtBearerAuthentication.AuthenticationScheme,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,

                    ValidateIssuer = true,
                    ValidIssuer = Configuration[ "JwtBearer:Issuer" ],

                    ValidateAudience = true,
                    ValidAudience = Configuration[ "JwtBearer:Audience" ],

                    NameClaimType = ClaimTypes.Email,
                    AuthenticationType = JwtBearerAuthentication.AuthenticationType
                }
            } );

            app.UseCookieAuthentication( new CookieAuthenticationOptions
            {
                AuthenticationScheme = CookieAuthentication.AuthenticationScheme
            } );
            
           /* ExternalAuthenticationEvents googleAuthenticationEvents = new ExternalAuthenticationEvents(
                new GoogleExternalAuthenticationManager( app.ApplicationServices.GetRequiredService<UserService>() ) );
                
            app.UseGoogleAuthentication( c =>
            {
                c.SignInScheme = CookieAuthentication.AuthenticationScheme;
                c.ClientId = Configuration[ "Authentication:Google:ClientId" ];
                c.ClientSecret = Configuration[ "Authentication:Google:ClientSecret" ];
                c.Events = new OAuthEvents
                {
                    OnCreatingTicket = googleAuthenticationEvents.OnCreatingTicket
                };
                c.AccessType = "offline";
            } );*/
         
            app.UseMvc( routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" } );

                routes.MapRoute(
                    name: "spa-fallback",
                    template: "app/{*anything}",
                    defaults: new { controller = "Home", action = "Index" } );
            } );

            app.UseStaticFiles();
        }
    }
}
