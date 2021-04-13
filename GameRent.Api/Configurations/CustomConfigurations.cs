using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using static GameRent.Application.Shared.AppSettings;

namespace GameRent.Api.Configurations
{
    public static class CustomConfigurations
    {
        public static IServiceCollection AddCustomConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-br");

            var assembly = AppDomain.CurrentDomain.Load("GameRent.Application");

            services.AddMediatR(assembly);
            services.Configure<TokenSettings>(configuration.GetSection("TokenSettings"));

            return services;
        }
    }
}