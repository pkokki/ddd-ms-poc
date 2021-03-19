using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Application;
using Accounts.Domain;
using Accounts.Infrastructure.Repositories;
using Accounts.Infrastructure.TransactionExecution;
using Accounts.Infrastructure.TransactionPermissions;
using ExtAccountService.Client;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MobileGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<SubscriberPermissionsAdapter>();
            services.AddScoped(typeof(ITransactionPermissionService), typeof(TransactionPermissionService));
            services.AddScoped(typeof(ITransactionExecutionService), typeof(TransactionExecutionService));
            services.AddScoped(typeof(IAccountRepository), typeof(AccountServiceRepository));
            services.AddScoped(typeof(IExtAccountClientService), typeof(ExtAccountClientService));
            services.AddScoped(typeof(IExtAccountClientService), typeof(ExtAccountClientService));
            services.AddMediatR(typeof(AccountService));
            services.AddScoped(typeof(IAccountService), typeof(AccountService));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
